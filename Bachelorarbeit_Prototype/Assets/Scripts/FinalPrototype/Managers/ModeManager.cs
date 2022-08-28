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
    private MiniatureModeController _miniatureMode; /** Miniature Mode Controller*/

    private ARSubmodeController _arSubmode; /** AR Submode Controller*/

    [SerializeField]
    private Camera _arCamera; /** AR Camera used for AR mode*/
    public Camera ARCamera { get => _arCamera; } /** AR Camera used for AR mode*/

    private PhysicsRaycaster _arPhysicsRaycaster; /** Physics Raycaster attached to the AR Camera*/

    private PhysicsRaycaster _miniaturePhysicsRaycaster; /** Physics Raycaster attached to the Miniature Camera*/

    private Mode _standbyARMode; /** AR mode which will be switched to when returning from Miniature mode*/
    private Mode _currentMode; /** Currently active mode */

    private bool _miniatureInstructionWasShown; /** Value indicating if the instruction screen for the Miniature was already shown*/

    private SessionManager _sessionManager; /** Session Manager */


    private bool _poiIsSelected; /** Value indicating if a POI is currently selected */
    public bool PoiIsSelected /** Value indicating if a POI is currently selected */
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


    void Start()
    {
        _sessionManager = SessionManager.Instance;
        _poiIsSelected = false;
        _standbyARMode = Mode.AR;
        _currentMode = Mode.AR;
        _arSubmode = GetComponent<ARSubmodeController>();
        _arPhysicsRaycaster = _arCamera.GetComponent<PhysicsRaycaster>();

        if (_miniatureMode != null)
        {
            _miniatureMode.Setup(_arCamera);
            _miniaturePhysicsRaycaster = _miniatureMode.Camera.GetComponent<PhysicsRaycaster>();
        }
    }

    /** Setup the modes after AR Session was started
     */
    public void SetupSession()
    {
        _miniatureMode = FindObjectOfType<MiniatureModeController>();
        if (_miniatureMode != null)
        {
            _miniatureMode.Setup(_arCamera);
            _miniaturePhysicsRaycaster = _miniatureMode.Camera.GetComponent<PhysicsRaycaster>();
        }
    }

    /** Switch between AR and Miniature Mode
     */
    public void SwitchMode()
    {
        if (_currentMode == Mode.Miniature)
        {
            StartCoroutine(SwitchToARMode());
        }
        else
        {
            _standbyARMode = _currentMode;
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


    /** Switch to AR Mode
     */
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

    /** Switch to Camera Submode (within AR Mode)
     */
    public void SwitchToCameraSubmode()
    {
        _currentMode = Mode.ARCamera;
        _standbyARMode = Mode.ARCamera;
        _arSubmode.Show(_currentMode);
    }

    /** Switch to Picture Submode (within AR Mode)
     */
    public void SwitchToPictureSubmode()
    {
        _currentMode = Mode.ARPicture;
        _standbyARMode = Mode.ARPicture;
        _arSubmode.Show(_currentMode);
    }

    /** Checks if the current mode is an AR submode
     *  @return true if the current mode is an AR submode
     */
    private bool CurrentModeIsARSubmode()
    {
        return (_currentMode == Mode.ARCamera || _currentMode == Mode.ARPicture);
    }

    /** Activates the Physics Raycaster of one mode and deactivates the other
     * @param currentMode defines the mode for which the Physics Raycaster should be activated
     */
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
