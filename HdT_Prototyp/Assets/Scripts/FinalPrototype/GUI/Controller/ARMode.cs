using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//name: AR GUI Controller
public class ARMode : MonoBehaviour
{
   
    [SerializeField]
    private POIImage _poiImage;

    [SerializeField]
    private Button _switchToPictureButton;

    [SerializeField]
    private Button _switchToCameraButton;

    private Mode _currentMode;

    private int _currentPOIId;

    // Start is called before the first frame update
    void Start()
    {
        //_contentPanel.gameObject.SetActive(true);
        _currentMode = Mode.ARCamera;
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);
    }

    /*public void ShowTest()
    {
        Show(Mode.ARPicture);
    }*/

    public void Show(Mode mode)
    {
        if(mode == Mode.ARPicture)
        {
            if (_currentPOIId != SessionManager.Instance.ActivePOI.Id)
            {
                Setup();
            }
            _currentMode = Mode.ARPicture;
            StartCoroutine(ShowPictureMode());

           
        } else if (mode == Mode.ARCamera)
        {
            //_poiPicture.gameObject.SetActive(false);
            StartCoroutine(ShowCameraMode());
           
        }

        //_contentPanel.gameObject.SetActive(true);
    }
    

    /*public void ShowPictureMode()
    {
        if(_currentPOIId != SessionManager.Instance.ActivePOI.Id)
        {
            Setup();
        }
        _poiPicture.gameObject.SetActive(true);
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(true);
        _contentPanel.gameObject.SetActive(true);
    }

    public void ShowCameraMode()
    {
        _poiPicture.gameObject.SetActive(false);
        _switchToPictureButton.gameObject.SetActive(true);
        _switchToCameraButton.gameObject.SetActive(false);
        _contentPanel.gameObject.SetActive(true);

    }*/

    public void Hide()
    {
        
        _currentMode = Mode.ARCamera;
        StartCoroutine(_poiImage.Hide());
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);
        //_contentPanel.gameObject.SetActive(false);
    }

    private IEnumerator ShowPictureMode()
    {
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(false);

        yield return StartCoroutine(_poiImage.Show());
       // yield return new WaitForSeconds(0.5f);
        _switchToPictureButton.gameObject.SetActive(false);
        _switchToCameraButton.gameObject.SetActive(true);
    }

    private IEnumerator ShowCameraMode()
    {
        //if(_currentMode == Mode.ARPicture)
        //{
            _switchToPictureButton.gameObject.SetActive(false);
            _switchToCameraButton.gameObject.SetActive(false);
            yield return StartCoroutine(_poiImage.Hide());
            //yield return new WaitForSeconds(0.5f);
           
        //}
        _switchToPictureButton.gameObject.SetActive(true);
        _switchToCameraButton.gameObject.SetActive(false);
    }

    private void Setup()
    {
        _poiImage.SetImage(SessionManager.Instance.ActivePOI.Picture);
    }

    void OnGUI()
    {



        //
        GUI.Label(new Rect(200, 300, 400, 100), " current POI " + _currentPOIId);

        GUI.Label(new Rect(200, 350, 400, 100), " current POI 2 " + SessionManager.Instance.ActivePOI.Id);
     



    }
}
