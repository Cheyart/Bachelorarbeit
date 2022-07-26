using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;



//TO DO: implement ModeManager?
public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    [SerializeField]
    private User _loggedInUser;
    public User LoggedInUser { get => _loggedInUser; }


    public PointOfInterest ActivePOI { get; set; }

    public OffScreenPointer _offscreenPointer;


    [SerializeField]
    private MainGUIController _GUIController;
    public MainGUIController GUIController { get => _GUIController; }

    [SerializeField]
    private POISelectionManager _poiSelectionManager;

    [SerializeField]
    private ModeManager _modeManager;

    [SerializeField]
    private UserInstructionController _instructionController;

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

        Debug.Log("AWAKE SessionManager");
        _instructionController.ShowInstruction(Instructions.scanQRCode);
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
                _instructionController.HideInstruction(Instructions.scanQRCode);
                _sessionInProgress = true;
                _GUIController.ShowMainGUI();
                _poiSelectionManager.SetupPOIs();
                _modeManager.Setup();
            }

            //_trackedImage = newImage;
            //_transitionController.ActivateNewSession(newImage);
            //_imageDetected++;
        }
    }

   
    public void SetTarget(GameObject go)
    {
        _offscreenPointer.Target = go;
    }
    /*void OnGUI()
    {
      
        
            GUI.Label(new Rect(200, 450, 400, 100), " Session in progress = " + _sessionInProgress);

        
       



    }*/


}
