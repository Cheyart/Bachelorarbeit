using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;



public enum Views
{
    arView, vrView
}

[RequireComponent (typeof (GyroscopeHandler))]
public class ViewTransitionController : MonoBehaviour
{

    public Views CurrentView { get => _currentView; set => _currentView = value;}
    [SerializeField]
    private Views _currentView = Views.arView;

    [SerializeField]
    private ARView _arView;

    [SerializeField]
    private VRView _vrView;

    [SerializeField]
    private OverlayGUI _overlayGUI;

    private GyroscopeHandler _gyroHandler;

    private int _insideSwitchToVR;
    private int _insideSwitchToAR;
    private int _setupTest;
    private ARTrackedImage _trackedImage;

    private bool _automaticTransitionEnabled;

    public bool SessionInProgress
    {
        get { return _sessionInProgess; }
        set
        {
            _sessionInProgess = value;
           /* if (_sessionInProgess)
            {
                _overlayGUI.Show();
                _gyroHandler.IsEnabled = true;

                if (!_vrView.SceneIsSetup)
                {
                    _setupTest+=5;

                    if (_arView.TrackedImage.transform.position != null && _arView.TrackedImage.transform.rotation != null)
                    {
                        _setupTest++;
                        _vrView.Setup(_arView.TrackedImage.transform.position, _arView.TrackedImage.transform.rotation, _arView.ArCamera);

                    }
                    else
                    {
                        return;
                    }
                }
            }*/

        }
    }
    private bool _sessionInProgess;

   

    // Start is called before the first frame update
    void Start()
    {
        _automaticTransitionEnabled = true;
        _gyroHandler = GetComponent<GyroscopeHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void ActivateNewSession(ARTrackedImage trackedImage)
    {
        _sessionInProgess = true;
        _trackedImage = trackedImage;
      
            _overlayGUI.Show();
            _gyroHandler.IsEnabled = true;

            if (trackedImage != null && trackedImage != null)
            {
                _vrView.Setup(trackedImage.transform.position, trackedImage.transform.rotation, _arView.ArCamera, trackedImage);
                _setupTest++;

        }
    }

    private void setUpWIM()
    {

    }

    public void SwitchToVR()
    {
        if (_sessionInProgess)
        {
            /*if (!_vrView.SceneIsSetup)
            {
                //_setupTest++;

                if (_arView.TrackedImage.transform.position != null && _arView.TrackedImage.transform.rotation != null)
                {
                    //_setupTest++;
                    _vrView.Setup(_arView.TrackedImage.transform.position, _arView.TrackedImage.transform.rotation, _arView.ArCamera);
                  
                }
                else
                {
                    return;
                }
            }*/

            _insideSwitchToVR++;
            _vrView.Show();
            //_arView.gameObject.SetActive(false);
            if (!_automaticTransitionEnabled)
            {
                _overlayGUI.SwitchToVrGUI();
            }

            _currentView = Views.vrView;
        }
    }

    public void SwitchToAR()
    {
        _insideSwitchToAR++;
        _vrView.Hide();
        //_arView.gameObject.SetActive(true);
        if (!_automaticTransitionEnabled)
        {
            _overlayGUI.SwitchToArGUI();
        }
        _currentView = Views.arView;

    }

    public void EnableAutomaticTransition(bool value)
    {
        _automaticTransitionEnabled = value;
       // Debug.Log("Enable Automatic Transition = " + _automaticTransitionEnabled);

        if (_automaticTransitionEnabled)
        {
            _gyroHandler.IsEnabled = true;
            _overlayGUI.HideBothViews();

        }
        else
        {
            _gyroHandler.IsEnabled = false;
            if(_currentView == Views.arView)
            {
                _overlayGUI.SwitchToArGUI();
            } else if (_currentView == Views.vrView)
            {
                _overlayGUI.SwitchToVrGUI();

            }
        }
    }

   

    

  // void OnGUI()
    //{
        //GUI.Label(new Rect(200, 250, 400, 100), " Setup = " + _setupTest);
        /*GUI.Label(new Rect(200, 200, 400, 100), " saved tracked image position = " + _trackedImage.transform.position);
        GUI.Label(new Rect(200, 250, 400, 100), " saved tracked image rotation = " + _trackedImage.transform.rotation);
        GUI.Label(new Rect(200, 300, 400, 100), " Tracked image position (AR View) = " + _arView.TrackedImage.transform.position);
        GUI.Label(new Rect(200, 350, 400, 100), " Tracked Image rotation = " + _arView.TrackedImage.transform.rotation);
        GUI.Label(new Rect(200, 400, 400, 100), " ARCamera position = " + _arView.ArCamera.transform.position);*/


        /*GUI.Label(new Rect(200, 250, 400, 100), " Automatic Transition enabled (in transition controller) = " + _automaticTransitionEnabled);
        GUI.Label(new Rect(200, 300, 400, 100), " View Transition Switch To VR = " + _insideSwitchToVR);
        GUI.Label(new Rect(200, 350, 400, 100), " View Transition Switch To AR = " + _insideSwitchToAR);
        GUI.Label(new Rect(200, 450, 400, 100), " VR Scene is set up = " + _vrView.SceneIsSetup);*/



    //}
}
