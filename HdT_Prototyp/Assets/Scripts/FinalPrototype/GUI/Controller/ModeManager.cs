using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Mode
{
    AR, ARCamera, ARPicture, Miniature
}

// TO DO: revise attributes (make more compact)
[RequireComponent(typeof(ARSubmodeController))]
public class ModeManager : MonoBehaviour
{
   

    [SerializeField]
    private Camera _arCamera;
    //[SerializeField]
    private PhysicsRaycaster _arPhysicsRaycaster;

    //[SerializeField]
    private PhysicsRaycaster _miniaturePhysicsRaycaster;

    [SerializeField]
    private MiniatureMode _miniatureMode;

    private ARSubmodeController _arSubmode;

    //[SerializeField]
    //private OffScreenPointer _offScreenPointer;

    private Mode _standbyARMode; //AR mode which will be switched to when returning from Miniature mode
    private Mode _currentMode;

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

            if(_poiIsSelected)
            {
                if (_currentMode == Mode.AR)
                {
                    //_offScreenPointer.IsEnabled = false;
                    _currentMode = Mode.ARPicture;
                    _standbyARMode = Mode.ARPicture;
                    _arSubmode.Show(_currentMode);
                } else if(_currentMode == Mode.Miniature)
                {
                    _standbyARMode = Mode.ARPicture;
                }
                   
            } else if (!_poiIsSelected) 
            {
                _standbyARMode = Mode.AR;
                _arSubmode.Hide();

                if(CurrentModeIsARSubmode())
                {
                    _currentMode = Mode.AR;
                }

                /* if (_currentMode == Mode.ARPicture || )
                 {
                     _standbyARMode = Mode.ARCamera;
                     _currentMode = Mode.ARCamera;
                     _arMode.Hide();
                 } else if (_currentMode == Mode.Miniature)
                 {
                 }*/

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // _standbyARMode = Mode.ARCamera;
        // _currentMode = Mode.ARCamera;

        _poiIsSelected = false;
        _standbyARMode = Mode.AR;
        _currentMode = Mode.AR;
        //_offScreenPointer.IsEnabled = false;
        _arSubmode = GetComponent<ARSubmodeController>();
        _arPhysicsRaycaster = _arCamera.GetComponent<PhysicsRaycaster>();
        

        if (_miniatureMode != null)
        {
            _miniatureMode.Setup(_arCamera);
            _miniaturePhysicsRaycaster = _miniatureMode.Camera.GetComponent<PhysicsRaycaster>();
        }
    }

   
    public void SetupMiniatureMode()
    {
        _miniatureMode = FindObjectOfType<MiniatureMode>();
        if(_miniatureMode != null)
        {
            _miniatureMode.Setup(_arCamera);
            _miniaturePhysicsRaycaster = _miniatureMode.Camera.GetComponent<PhysicsRaycaster>();
        }
    }

    public void SwitchMode()
    {
        //Switch from Miniature mode
        if(_currentMode == Mode.Miniature)
        {
            StartCoroutine(SwitchToARMode());
        }
        //switch from AR Mode
        else
        {
            _standbyARMode = _currentMode;
            _miniatureMode.Show();

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



    ////Picture Mode

    /* void OnGUI()
      {



          GUI.Label(new Rect(200, 400, 400, 100), " Current Mode: " + _currentMode);
          GUI.Label(new Rect(200, 450, 400, 100), " AR standby mode: " + _standbyARMode);
          GUI.Label(new Rect(200, 500, 400, 100), " POI is selected: " + _poiIsSelected);
          GUI.Label(new Rect(200, 550, 400, 100), " AR physics raycaster: " + _arPhysicsRaycaster.enabled);
          GUI.Label(new Rect(200, 550, 400, 100), " miniature physics raycaster: " + _miniaturePhysicsRaycaster.enabled);




      }*/


}
