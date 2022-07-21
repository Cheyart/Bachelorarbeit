using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//name: AR GUI Controller
public class PictureMode : MonoBehaviour
{
    [SerializeField]
    private GameObject _contentPanel;

    [SerializeField]
    private Image _poiPicture;

    [SerializeField]
    private Button _switchToPictureButton;

    [SerializeField]
    private Button _switchToCameraButton;

    private int _currentPOIId;

    // Start is called before the first frame update
    void Start()
    {
        _contentPanel.gameObject.SetActive(false);
    }

    public void Show(Mode mode)
    {
        if(mode == Mode.ARPicture)
        {
            if (_currentPOIId != SessionManager.Instance.ActivePOI.Id)
            {
                Setup();
            }
            _poiPicture.gameObject.SetActive(true);
            _switchToPictureButton.gameObject.SetActive(false);
            _switchToCameraButton.gameObject.SetActive(true);
        } else if (mode == Mode.ARCamera)
        {
            _poiPicture.gameObject.SetActive(false);
            _switchToPictureButton.gameObject.SetActive(true);
            _switchToCameraButton.gameObject.SetActive(false);
        }

        _contentPanel.gameObject.SetActive(true);
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
        _contentPanel.gameObject.SetActive(false);
    }

    private void Setup()
    {
        _poiPicture.sprite = SessionManager.Instance.ActivePOI.Picture;
    }
}
