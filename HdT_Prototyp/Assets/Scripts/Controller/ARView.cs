using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARView : MonoBehaviour
{
    [SerializeField]
    private ViewTransitionController _transitionController;

    public Camera ArCamera { get => _arCamera; set => _arCamera = value; }
    [SerializeField]
    private Camera _arCamera;

    public ARTrackedImageManager TrackedImageManager{ get => _trackedImageManager; set => _trackedImageManager = value; }
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


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            _trackedImage = newImage;
            //_transitionController.SessionInProgress = true
            _transitionController.ActivateNewSession(newImage);
            _imageDetected++;
        }
    }

   /* void OnGUI()
    {

            GUI.Label(new Rect(200, 800, 400, 100), " Image Detected = " + _imageDetected);
            //GUI.Label(new Rect(200, 850, 400, 100), " _trackedImagePosition = " + _trackedImagePosition);
            GUI.Label(new Rect(200, 850, 400, 100), " _trackedImagePosition 2 = " + _trackedImage.transform.position);

            GUI.Label(new Rect(200, 900, 400, 100), " _trackedImageRotation 2 = " + _trackedImage.transform.rotation);

    }*/
}
