using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace LowFiPrototype
{
    public class PlayerPosition : MonoBehaviour
    {

        public Camera ARCamera { get => _arCamera; set => _arCamera = value; }
        private Camera _arCamera;

        public bool IsActive { get => _isActive; set => _isActive = value; }
        private bool _isActive = false;


        // Start is called before the first frame update
        void Start()
        {
            _isActive = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isActive && _arCamera != null)
            {
                transform.position = _arCamera.transform.position;
            }
        }

    }
}