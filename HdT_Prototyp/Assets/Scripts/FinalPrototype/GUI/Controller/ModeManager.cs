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
    private Mode _previousMode;
    private Mode _currentMode;

    [SerializeField]
    private GameObject _vrMode;

    // Start is called before the first frame update
    void Start()
    {
        _previousMode = Mode.ARCamera;
        _currentMode = Mode.ARCamera;
        _vrMode.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMode()
    {
        if(_currentMode == Mode.ARCamera || _currentMode == Mode.ARPicture)
        {
            _previousMode = _currentMode;
            _currentMode = Mode.Miniature;
            _vrMode.SetActive(true);
        } else
        {
            _currentMode = _previousMode;
            _previousMode = Mode.Miniature;
            _vrMode.SetActive(false);
        }
    }
}
