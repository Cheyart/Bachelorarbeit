using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    ARCamera, ARPicture, Miniature
}

// TO DO: inmplement animated transition between miniature and ar mode
public class ModeManager : MonoBehaviour
{
    private Mode _standbyARMode; //AR mode which will be switched to when returning from Miniature mode
    private Mode _currentMode;

    [SerializeField]
    private OffScreenPointer _offScreenPointer;

    [SerializeField]
    private GameObject _vrMode;

    [SerializeField]
    private MiniatureMode _miniatureMode;

    [SerializeField]
    private PictureMode _pictureMode;

    [SerializeField]
    private Camera _arCamera;

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
                if (_currentMode == Mode.ARCamera)
                {
                    _offScreenPointer.IsEnabled = false;
                    _currentMode = Mode.ARPicture;
                    _standbyARMode = Mode.ARPicture;
                    _pictureMode.Show(_currentMode);
                } else if(_currentMode == Mode.Miniature)
                {
                    _standbyARMode = Mode.ARPicture;
                }
                   
            } else if (!_poiIsSelected) 
            {
                if(_currentMode == Mode.ARPicture)
                {
                    _standbyARMode = Mode.ARCamera;
                    _currentMode = Mode.ARCamera;
                    _pictureMode.Hide();
                } else if (_currentMode == Mode.Miniature)
                {
                    _standbyARMode = Mode.ARCamera;
                }
               
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _miniatureMode.Show();
        _poiIsSelected = false;
        _standbyARMode = Mode.ARCamera;
        _currentMode = Mode.ARCamera;
        _offScreenPointer.IsEnabled = true;
        //_vrMode.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup()
    {
        _miniatureMode = FindObjectOfType<MiniatureMode>();
        if(_miniatureMode != null)
        {
            _miniatureMode.Setup(_arCamera);
        }
    }

    public void SwitchMode()
    {
        if(_currentMode == Mode.ARCamera || _currentMode == Mode.ARPicture)
        {
            _offScreenPointer.IsEnabled = false;
            _standbyARMode = _currentMode;

            //_vrMode.SetActive(true);
            _miniatureMode.Show();
            
          
            _pictureMode.Hide();
          

            _currentMode = Mode.Miniature;


        }
        else
        {
            
            _currentMode = _standbyARMode;
            //_standbyARMode = _currentMode;

            if(_poiIsSelected)
            {
                _pictureMode.Show(_currentMode);
            }

            //_vrMode.SetActive(false);
            _miniatureMode.Hide();


            if (_currentMode == Mode.ARCamera)
            {
                _offScreenPointer.IsEnabled = true;
            }
        }
    }

    public void SwitchToCameraMode()
    {
        _currentMode = Mode.ARCamera;
        _standbyARMode = Mode.ARCamera;
        _pictureMode.Show(_currentMode);
    }

    public void SwitchToPictureMode()
    {
        _currentMode = Mode.ARPicture;
        _standbyARMode = Mode.ARPicture;
        _pictureMode.Show(_currentMode);
    }

    ////Picture Mode

    /*void OnGUI()
    {

        

        GUI.Label(new Rect(200, 350, 400, 100), " Current Mode: " + _currentMode);
        GUI.Label(new Rect(200, 400, 400, 100), " AR standby mode: " + _standbyARMode);
        GUI.Label(new Rect(200, 450, 400, 100), " POI is selected: " + _poiIsSelected);
  

    }*/


}
