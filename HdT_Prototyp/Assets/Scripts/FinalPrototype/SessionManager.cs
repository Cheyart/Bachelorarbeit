using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    [SerializeField]
    private User _loggedInUser;
    public User LoggedInUser { get => _loggedInUser; }


    public PointOfInterest ActivePOI { get; set; }


    [SerializeField]
    private MainGUIController _GUIController;

    [SerializeField]
    private ARTrackedImageManager _trackedImageManager;

    [SerializeField]
    private PointOfInterestDB _poiDB;
    public PointOfInterestDB POI_DB { get => _poiDB;  }

    [SerializeField]
    private CommentsDB _commentsDB;
    public CommentsDB CommentsDB { get => _commentsDB; }

    [SerializeField]
    private ThreadsDB _threadsDB;
    public ThreadsDB ThreadsDB { get => _threadsDB; }

    private bool _sessionInProgress;

    void OnEnable() => _trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

    void OnDisable() => _trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        POI_DB.Setup();
        CommentsDB.Setup();
        ThreadsDB.Setup();
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            //activate Session
            if (!_sessionInProgress)
            {
                _sessionInProgress = true;
                _GUIController.ShowMainGUI();
            }

            //_trackedImage = newImage;
            //_transitionController.ActivateNewSession(newImage);
            //_imageDetected++;
        }
    }
}
