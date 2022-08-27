//source: https://docs.unity3d.com/ScriptReference/Gyroscope.html


using System.Collections;
using UnityEngine;

namespace LowFiPrototype
{
    public class GyroscopeHandler : MonoBehaviour
    {

        [SerializeField]
        private ViewTransitionController _transitionController;

        public bool IsEnabled { get => _isEnabled; set => _isEnabled = value; }
        [SerializeField]
        private bool _isEnabled = false;

        private bool _gyroEnabled;
        private Gyroscope _gyro;

        private const float MIN_VALUE = 50f;

        private const float MAX_VALUE = 100f;

        private int _insideSwitchUIMode;
        private int _insideSwitchToVR;
        private int _insideSwitchToAR;

        private float _phonePitch;



        void Start()
        {

            _gyroEnabled = EnableGyro();
        }


        private void Update()
        {
            if (_isEnabled && _gyroEnabled)
            {
               
                _phonePitch = GetPhonePitch();
                if (_transitionController.CurrentView == Views.arView && _phonePitch > MIN_VALUE && _phonePitch < MAX_VALUE)
                {
                    _insideSwitchToVR++;
                    StartCoroutine("switchUIMode", Views.vrView);
                }
                else if (_transitionController.CurrentView == Views.vrView && !(_phonePitch > MIN_VALUE && _phonePitch < MAX_VALUE))
                {
                    _insideSwitchToAR++;
                    StartCoroutine("switchUIMode", Views.arView);

                }
            }
        }

        private bool EnableGyro()
        {
            if (SystemInfo.supportsGyroscope)
            {
                _gyro = Input.gyro;
                _gyro.enabled = true;

                _phonePitch = GetPhonePitch();


                return true;
            }
            return false;
        }

        float GetPhonePitch()
        {
            Quaternion referenceRotation = Quaternion.identity;
            Quaternion deviceRotation = GetDeviceRotation();
            Quaternion eliminationOfYZ = Quaternion.Inverse(
            Quaternion.FromToRotation(referenceRotation * Vector3.right,
                                    deviceRotation * Vector3.right));
            Quaternion rotationX = eliminationOfYZ * deviceRotation;
            return rotationX.eulerAngles.x;
        }


        Quaternion GetDeviceRotation()
        {
            if (_gyro != null)
            {
                return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * _gyro.attitude * new Quaternion(0, 0, 1, 0);
            }
            return new Quaternion();
        }



        private IEnumerator switchUIMode(Views newView)
        {
            _insideSwitchUIMode++;
            yield return new WaitForSeconds(1.5f);

            if (newView == Views.vrView && _transitionController.CurrentView == Views.arView && _phonePitch > MIN_VALUE && _phonePitch < MAX_VALUE)
            {
                _transitionController.SwitchToVR();
            }
            else if (newView == Views.arView && _transitionController.CurrentView == Views.vrView && !(_phonePitch > MIN_VALUE && _phonePitch < MAX_VALUE))
            {
                _transitionController.SwitchToAR();
            }

        }

    }
}