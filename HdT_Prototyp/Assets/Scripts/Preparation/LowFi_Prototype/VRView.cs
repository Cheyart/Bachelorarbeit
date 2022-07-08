using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Preparation
{
    public class VRView : MonoBehaviour
{
    public GameObject WIMScene { get => _WIMScene; set => _WIMScene = value; }
    [SerializeField]
    private GameObject _WIMScene;

    [SerializeField]
    private PlayerPosition _playerPosition;

    public bool SceneIsSetup { get => _sceneIsSetup; set => _sceneIsSetup = value; }
    private bool _sceneIsSetup;

    private Vector3 _scenePosition = new Vector3();
    private Quaternion _sceneRotation = new Quaternion();

    private int _setupTest;
    private ARTrackedImage _trackedImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DELETE?
        //_WIMScene.transform.position = _scenePosition;
        //_WIMScene.transform.rotation = _sceneRotation;
        _WIMScene.transform.position = _trackedImage.transform.position;
        _WIMScene.transform.rotation = _trackedImage.transform.rotation;
    }

    public void Setup(Vector3 position, Quaternion rotation, Camera arCamera, ARTrackedImage trackedImage)
    {
        _setupTest++;
        _trackedImage = trackedImage;

        /*_WIMScene.transform.position = position;
        _WIMScene.transform.rotation = rotation;

        _scenePosition = position;
        _sceneRotation = rotation;*/

        _WIMScene.transform.position = _trackedImage.transform.position;
        _WIMScene.transform.rotation = _trackedImage.transform.rotation;

        _scenePosition = trackedImage.transform.position;
        _sceneRotation = trackedImage.transform.rotation;

        if (_playerPosition != null)
        {
            _playerPosition.ARCamera = arCamera;
            _playerPosition.IsActive = true;
        }
        _sceneIsSetup = true;
    }

    public void Show()
    {
        //_WIMScene.gameObject.SetActive(true);
        //_playerPosition.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /*void OnGUI()
    {
        
        GUI.Label(new Rect(200, 500, 400, 100), " WIM position = " + _WIMScene.transform.position);
        GUI.Label(new Rect(200, 550, 400, 100), " WIM rotation = " + _WIMScene.transform.rotation);
        //GUI.Label(new Rect(200, 600, 400, 100), " scene position = " + _scenePosition);
        //GUI.Label(new Rect(200, 650, 400, 100), " scene rotation = " + _sceneRotation);
        GUI.Label(new Rect(200, 600, 400, 100), " tracked image position (in VR)= " + _trackedImage.transform.position);
        GUI.Label(new Rect(200, 650, 400, 100), " tracked image rotation (in VR)= " + _trackedImage.transform.rotation);
        GUI.Label(new Rect(200, 700, 400, 100), " in VR view setupTest = " + _setupTest);*/


        /*GUI.Label(new Rect(200, 250, 400, 100), " Automatic Transition enabled (in transition controller) = " + _automaticTransitionEnabled);
        GUI.Label(new Rect(200, 300, 400, 100), " View Transition Switch To VR = " + _insideSwitchToVR);
        GUI.Label(new Rect(200, 350, 400, 100), " View Transition Switch To AR = " + _insideSwitchToAR);
        GUI.Label(new Rect(200, 450, 400, 100), " VR Scene is set up = " + _vrView.SceneIsSetup);*/



    //}


}
}