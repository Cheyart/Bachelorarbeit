using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ARSubmodeController : MonoBehaviour
{

    [SerializeField]
    private POIImage _poiImage;

    [SerializeField]
    private OffScreenPointer _poiOffScreenPointer;

    [SerializeField]
    private Button _switchToPictureButton;

    [SerializeField]
    private Button _switchToCameraButton;

    private SessionManager _sessionManager;

    private int _currentPOIId;

    private int counter;


    // Start is called before the first frame update
    void Start()
    {
        //_contentPanel.gameObject.SetActive(true);
        // _currentMode = Mode.ARCamera;
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
                counter += 3;
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
        _currentPOIId = _sessionManager.ActivePOI.Id;
        _poiImage.SetImage(_sessionManager.ActivePOI.Picture);
        
    }

    void OnGUI()
    {

        GUI.Label(new Rect(200, 300, 400, 100), " Current POI Id = " + _currentPOIId);
        GUI.Label(new Rect(200, 350, 400, 100), " counter = " + counter);

    }



}
