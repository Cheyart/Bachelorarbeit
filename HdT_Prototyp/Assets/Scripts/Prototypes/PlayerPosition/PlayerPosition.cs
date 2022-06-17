using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

//To D0: use Coroutine to manage position (de/activate Coroutine in IsActive property)
public class PlayerPosition : MonoBehaviour
{
    [SerializeField]
    private Camera _arCamera;

    [SerializeField]
    private ARTrackedImageManager _TrackedImageManager;

    [SerializeField]
    private ARSessionOrigin _arSessionOrigin;

    //[SerializeField]
    //private GameObject _rotationObject;

    //[SerializeField]
    //private GameObject _parentObject;

    [SerializeField]
    private GameObject _arScene;

    private bool _isActive;

    private Vector3 _cameraPosAtStart;
    private Quaternion _cameraRotAtStart;

    private ARTrackedImage _trackedImage;


    //forTest only
    private GUIStyle guiStyle = new GUIStyle();

    void OnEnable() => _TrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

    void OnDisable() => _TrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;

    // Start is called before the first frame update
    void Start()
    {
        _isActive = false;
        guiStyle.fontSize = 80; //change the font size
    }

    // Update is called once per frame
    void Update()
    {
        if(_isActive && _arCamera != null)
        {

            //TO DO: try without adjusting to camera position


            transform.position = _arCamera.transform.position;
            //transform.rotation = _arCamera.transform.rotation;
            transform.localRotation = _trackedImage.transform.rotation;
            //_parentObject.transform.localRotation = _rotationObject.transform.localRotation;

            //put outside of update loop 
            _arScene.transform.position = _trackedImage.transform.position;
            _arScene.transform.rotation = _trackedImage.transform.rotation;
        }
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            _cameraPosAtStart = _arCamera.transform.position;
            _cameraRotAtStart = _arCamera.transform.rotation;

            _trackedImage = newImage;

            //not working (?) -> coordinates of trackedImage are 0
            _arScene.transform.position = _trackedImage.transform.position;
            _arScene.transform.rotation = _trackedImage.transform.rotation;
            //_parentObject.transform.position = _rotationObject.transform.position;
            //_parentObject.transform.rotation = _rotationObject.transform.rotation;
           
            gameObject.SetActive(true);
            _isActive = true;

        }

        /*foreach (var updatedImage in eventArgs.updated)
        {
        }*/

        foreach (var removedImage in eventArgs.removed)
        {
            gameObject.SetActive(false);
            _isActive = false;
        }
    }

   

    void OnGUI()
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
            GUI.Label(new Rect(200, 400, 400, 100), " AR Camera position at Scan" + _cameraPosAtStart);
            GUI.Label(new Rect(200, 450, 400, 100), " AR Camera rotation at Scan" + _cameraRotAtStart);

            GUI.Label(new Rect(200, 550, 400, 100), " Tracked Image Position" + _trackedImage.transform.position);
            GUI.Label(new Rect(200, 600, 400, 100), " Tracked Image Local Position" + _trackedImage.transform.localPosition);
            GUI.Label(new Rect(200, 650, 400, 100), " Tracked Image Rotation" + _trackedImage.transform.rotation);

            GUI.Label(new Rect(200, 750, 400, 100), " WIM Position" + _arScene.transform.position);
            GUI.Label(new Rect(200, 800, 400, 100), " WIM Local Position" + _arScene.transform.localPosition);
            GUI.Label(new Rect(200, 850, 400, 100), " WIM Rotation" + _arScene.transform.rotation);


            GUI.Label(new Rect(200, 950, 400, 100), " Player Position" + transform.position);
            GUI.Label(new Rect(200, 1000, 400, 100), " Player Local Pos" + transform.localPosition);
        }
    }
}
