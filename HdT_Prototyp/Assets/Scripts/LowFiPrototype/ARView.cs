using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace LowFiPrototype
{
    public class ARView : MonoBehaviour
    {
        [SerializeField]
        private ViewTransitionController _transitionController;

        public Camera ArCamera { get => _arCamera; set => _arCamera = value; }
        [SerializeField]
        private Camera _arCamera;

        public ARTrackedImageManager TrackedImageManager { get => _trackedImageManager; set => _trackedImageManager = value; }
        [SerializeField]
        private ARTrackedImageManager _trackedImageManager;

        public ARSessionOrigin ARSessionOrigin { get => _arSessionOrigin; set => _arSessionOrigin = value; }
        [SerializeField]
        private ARSessionOrigin _arSessionOrigin;



        void OnEnable() => _trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

        void OnDisable() => _trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;

        int _imageDetected;

        public ARTrackedImage TrackedImage { get => _trackedImage; set => _trackedImage = value; }
        private ARTrackedImage _trackedImage;



        void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
            foreach (var newImage in eventArgs.added)
            {
                _trackedImage = newImage;
                _transitionController.ActivateNewSession(newImage);
                _imageDetected++;
            }
        }

    }
}