using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Preparation
{
    //To D0: use Coroutine to manage position (de/activate Coroutine in IsActive property)
    public class PlayerPosition : MonoBehaviour
{

    public Camera ARCamera { get => _arCamera; set => _arCamera = value; }
    private Camera _arCamera;

    public bool IsActive { get => _isActive; set => _isActive = value; } 
    private bool _isActive = false;

    //forTest only
    private GUIStyle guiStyle = new GUIStyle();

    

    // Start is called before the first frame update
    void Start()
    {
        _isActive = true;
        guiStyle.fontSize = 80; //change the font size
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive && _arCamera != null)
        {
            transform.position = _arCamera.transform.position;
            //transform.localRotation = _trackedImage.transform.rotation;
        }
    }

  

   

    /*void OnGUI()
    {

        //Output the rotation rate, attitude and the enabled state of the gyroscope as a Label
        //GUI.Label(new Rect(500, 300, 200, 40), "Gyro rotation rate " + _gyro.rotationRate);
        if (_arCamera != null)
        {

            GUI.Label(new Rect(200, 250, 400, 100), " AR Camera position" + _arCamera.transform.position);
            GUI.Label(new Rect(200, 300, 400, 100), " AR Camera rotation" + _arCamera.transform.rotation);

        }
       
        if (_isActive)
        {
            GUI.Label(new Rect(200, 950, 400, 100), " Player Position" + transform.position);
            GUI.Label(new Rect(200, 1000, 400, 100), " Player Local Pos" + transform.localPosition);
        }
    }*/
}
}