using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/** @enum Mode defines the possible modes the system can be in
 */
public enum Mode
{
    AR, ARCamera, ARPicture, Miniature
}


/** @class ModeManager is responsible for coordinating the transitions between the different modes
 */
[RequireComponent(typeof(ARSubmodeController))]
public class ModeManager : MonoBehaviour
{

    [SerializeField]
    private MiniatureModeController _miniatureMode;

    private ARSubmodeController _arSubmode;

    [SerializeField]
    private Camera _arCamera;
    public Camera ARCamera { get => _arCamera; }

    private PhysicsRaycaster _arPhysicsRaycaster;

    private PhysicsRaycaster _miniaturePhysicsRaycaster;

    private Mode _standbyARMode; //AR mode which will be switched to when returning from Miniature mode
    private Mode _currentMode;

    private bool _miniatureInstructionWasShown;

    private SessionManager _sessionManager;


    private bool _poiIsSelected;
    public bool PoiIsSelected
    {
        get
        {
            return _poiIsSelected;
        }
        set
        {
            _poiIsSelected = value;

            if (_poiIsSelected)
            {

                if (_currentMode == Mode.AR)
                {
                    _currentMode = Mode.ARPicture;
                    _standbyARMode = Mode.ARPicture;
                    _arSubmode.Show(_currentMode);

                }
                else if (_currentMode == Mode.Miniature)
                {
                    _standbyARMode = Mode.ARPicture;
                }

            }
            else if (!_poiIsSelected)
            {
                _standbyARMode = Mode.AR;
                _arSubmode.Hide();

                if (CurrentModeIsARSubmode())
                {
                    _currentMode = Mode.AR;
                }

            }
        }
    }

    private string _debugText = "";

    void Start()
    {
        _sessionManager = SessionManager.Instance;
        _poiIsSelected = false;
        _standbyARMode = Mode.AR;
        _currentMode = Mode.AR;
        //_currentMode = Mode.Miniature;
        _arSubmode = GetComponent<ARSubmodeController>();
        _arPhysicsRaycaster = _arCamera.GetComponent<PhysicsRaycaster>();

        if (_miniatureMode != null)
        {
            _miniatureMode.Setup(_arCamera);
            _miniaturePhysicsRaycaster = _miniatureMode.Camera.GetComponent<PhysicsRaycaster>();


        }
    }


    public void SetupSession()
    {
        _miniatureMode = FindObjectOfType<MiniatureModeController>();
        if (_miniatureMode != null)
        {
            _miniatureMode.Setup(_arCamera);
            _miniaturePhysicsRaycaster = _miniatureMode.Camera.GetComponent<PhysicsRaycaster>();
        }


    }


    public void SwitchMode()
    {
        if (_currentMode == Mode.Miniature)
        {
            StartCoroutine(SwitchToARMode());
        }
        else
        {
            _standbyARMode = _currentMode;
            // _modelOffscreenPointer.IsEnabled = false;

            _miniatureMode.Show();
            if (!_miniatureInstructionWasShown)
            {
                if (_sessionManager.POIMenuManager.State == POIMenuState.big)
                {
                    _sessionManager.POIMenuManager.ContractMenu();
                }
                _sessionManager.InstructionController.ShowInstruction(Instructions.rotateModel, 2f, true);
                _miniatureInstructionWasShown = true;
            }

            if (CurrentModeIsARSubmode())
            {
                _arSubmode.Hide();
            }
            _currentMode = Mode.Miniature;

        }

        SwitchPhysicsRaycaster(_currentMode);
    }



    private IEnumerator SwitchToARMode()
    {
        _currentMode = _standbyARMode;

        if (_currentMode == Mode.ARPicture)
        {
            _arSubmode.Show(_currentMode);
        }


        yield return StartCoroutine(_miniatureMode.Hide());

        if (_currentMode == Mode.ARCamera)
        {
            _arSubmode.Show(_currentMode);
        }

    }

    public void SwitchToCameraSubmode()
    {
        _currentMode = Mode.ARCamera;
        _standbyARMode = Mode.ARCamera;
        _arSubmode.Show(_currentMode);
    }

    public void SwitchToPictureSubmode()
    {
        _currentMode = Mode.ARPicture;
        _standbyARMode = Mode.ARPicture;
        _arSubmode.Show(_currentMode);
    }

    private bool CurrentModeIsARSubmode()
    {
        return (_currentMode == Mode.ARCamera || _currentMode == Mode.ARPicture);
    }

    private void SwitchPhysicsRaycaster(Mode currentMode)
    {
        if (currentMode == Mode.Miniature)
        {
            _arPhysicsRaycaster.enabled = false;
            _miniaturePhysicsRaycaster.enabled = true;
        }
        else
        {
            _arPhysicsRaycaster.enabled = true;
            _miniaturePhysicsRaycaster.enabled = false;
        }
    }

}
