using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ARSubmodeController : MonoBehaviour
{
    private SessionManager _sessionManager;

    [SerializeField]
    private POIImage _poiImage;

    [SerializeField]
    private OffScreenPointer _poiOffScreenPointer;

    [SerializeField]
    private Button _switchToPictureButton;

    [SerializeField]
    private Button _switchToCameraButton;

    private int _currentPOIId;

    private int counter;

    private bool _instructionWasShown;


    // Start is called before the first frame update
    void Start()
    {
        _sessionManager = SessionManager.Instance;

        _poiImage.SetToTransparent();
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);
        _poiOffScreenPointer.IsEnabled = false;

    }

    public void Show(Mode mode)
    {
        if (mode == Mode.ARPicture)
        {

            if (_currentPOIId != _sessionManager.ActivePOI.Id)
            {
                SetupContent();
            }
            StartCoroutine(ShowPictureMode());
        }
        else if (mode == Mode.ARCamera)
        {
            StartCoroutine(ShowCameraMode());
        }
    }


    public void Hide()
    {
        StartCoroutine(_poiImage.FadeOut());
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);
        _poiOffScreenPointer.IsEnabled = false;
    }

    private IEnumerator ShowPictureMode()
    {
        counter++;
        _poiOffScreenPointer.IsEnabled = false;
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);

        yield return StartCoroutine(_poiImage.FadeIn());

        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(true);

        if (!_instructionWasShown)
        {
            SessionManager.Instance.InstructionController.ShowInstruction(Instructions.switchView);
            _instructionWasShown = true;
        }
    }

    private IEnumerator ShowCameraMode()
    {

        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);

        yield return StartCoroutine(_poiImage.FadeOut());
       
        _switchToPictureButton.gameObject.SetActive(true);
        _switchToCameraButton.gameObject.SetActive(false);
        _poiOffScreenPointer.IsEnabled = true;
    }

    private void SetupContent()
    {
        Debug.Log("Inside Setup content");
        _currentPOIId = _sessionManager.ActivePOI.Id;
        _poiImage.SetImage(_sessionManager.ActivePOI.Picture);
        
    }


}
