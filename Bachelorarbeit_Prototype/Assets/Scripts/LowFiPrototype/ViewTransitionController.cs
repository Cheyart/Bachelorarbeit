using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


namespace LowFiPrototype
{
    public enum Views
    {
        arView, vrView
    }

    [RequireComponent(typeof(GyroscopeHandler))]
    public class ViewTransitionController : MonoBehaviour
    {

        public Views CurrentView { get => _currentView; set => _currentView = value; }
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


            }
        }
        private bool _sessionInProgess;



        // Start is called before the first frame update
        void Start()
        {
            _automaticTransitionEnabled = true;
            _gyroHandler = GetComponent<GyroscopeHandler>();
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


        public void SwitchToVR()
        {
            if (_sessionInProgess)
            {


                _insideSwitchToVR++;
                _vrView.Show();
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
            if (!_automaticTransitionEnabled)
            {
                _overlayGUI.SwitchToArGUI();
            }
            _currentView = Views.arView;

        }

        public void EnableAutomaticTransition(bool value)
        {
            _automaticTransitionEnabled = value;

            if (_automaticTransitionEnabled)
            {
                _gyroHandler.IsEnabled = true;
                _overlayGUI.HideBothViews();

            }
            else
            {
                _gyroHandler.IsEnabled = false;
                if (_currentView == Views.arView)
                {
                    _overlayGUI.SwitchToArGUI();
                }
                else if (_currentView == Views.vrView)
                {
                    _overlayGUI.SwitchToVrGUI();

                }
            }
        }

    }
}