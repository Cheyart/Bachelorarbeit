using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace LowFiPrototype
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

        // Update is called once per frame
        void Update()
        {
            _WIMScene.transform.position = _trackedImage.transform.position;
            _WIMScene.transform.rotation = _trackedImage.transform.rotation;
        }

        public void Setup(Vector3 position, Quaternion rotation, Camera arCamera, ARTrackedImage trackedImage)
        {
            _setupTest++;
            _trackedImage = trackedImage;

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
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }



    }
}