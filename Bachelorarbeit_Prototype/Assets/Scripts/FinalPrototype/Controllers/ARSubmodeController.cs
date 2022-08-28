using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** @class ARSubmodeController controls the setup and display of the AR submodes (Camera and Picture mode).
 */
public class ARSubmodeController : MonoBehaviour
{
    private SessionManager _sessionManager; /** Session Manager*/

    [SerializeField]
    private POIImage _poiImage; /** POI Image which is displayed in the Picture Sub mode*/

    [SerializeField]
    private OffScreenPointer _poiOffScreenPointer; /** Off-Screen pointer for the currently selected POI */

    [SerializeField]
    private Button _switchToPictureButton; /** Switch to Picture mode button */

    [SerializeField]
    private Button _switchToCameraButton; /** Switch to Camera mode button */

    private int _currentPOIId; /** Id of the currently selected POI */

    private bool _instructionWasShown; /** value indicating if the user instruction for the AR submodes was already displayed */


    void Start()
    {
        _sessionManager = SessionManager.Instance;

        _poiImage.SetToTransparent();
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);
        _poiOffScreenPointer.IsEnabled = false;

    }

    /** Shows one of the AR submodes
     * @param mode Submode which will be shown
     */
    public void Show(Mode mode)
    {
        if (mode == Mode.ARPicture)
        {

            if (_currentPOIId != _sessionManager.ActivePOI.Id)
            {
                SetupPictureContent();
            }
            StartCoroutine(ShowPictureMode());
        }
        else if (mode == Mode.ARCamera)
        {
            StartCoroutine(ShowCameraMode());
        }
    }

    /** Hides the current mode
     */
    public void Hide()
    {
        StartCoroutine(_poiImage.FadeOut());
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);
        _poiOffScreenPointer.IsEnabled = false;
    }

    /** Shows the Picture Submode
     */
    private IEnumerator ShowPictureMode()
    {
        _poiOffScreenPointer.IsEnabled = false;
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);

        yield return StartCoroutine(_poiImage.FadeIn());

        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(true);

        if (!_instructionWasShown)
        {
            SessionManager.Instance.InstructionController.ShowInstruction(Instructions.switchView, 1f, true);
            _instructionWasShown = true;
        }
    }

    /** Shows the Camera submode
     */
    private IEnumerator ShowCameraMode()
    {

        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);

        yield return StartCoroutine(_poiImage.FadeOut());

        _switchToPictureButton.gameObject.SetActive(true);
        _switchToCameraButton.gameObject.SetActive(false);
        _poiOffScreenPointer.IsEnabled = true;
    }

    /** Sets up the content for the picture submode
     */
    private void SetupPictureContent()
    {
        _currentPOIId = _sessionManager.ActivePOI.Id;
        _poiImage.SetImage(_sessionManager.ActivePOI.Picture);

    }


}
