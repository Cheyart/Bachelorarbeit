using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

/** @class SessionManager holds references to all the managers and databases. Is responsible for setting up the session.
 */
public class SessionManager : MonoBehaviour
{


    //------------------ GLOBAL VARIABLES----------------------------
    public static SessionManager Instance { get; private set; } /** Instance of this manager*/

    [SerializeField]
    private User _loggedInUser; /** currently looged in user */
    public User LoggedInUser { get => _loggedInUser; }  /** currently looged in user */

    public PointOfInterest ActivePOI { get; set; }  /** currently active POI */


    //------------------ MANAGERS----------------------------

    public MainGUIController GUIController { get; private set; } /** Main GUI Controller */
    public POISelectionManager POISelectionManager { get; private set; } /** POI Selection Manager*/
    public ModeManager ModeManager { get; private set; } /** Mode Manager */
    public POIMenuManager POIMenuManager { get; private set; } /** POI Menu Manager */
    public CommentManager CommentManager { get; private set; } /** Comment Manager */
    public UserInstructionController InstructionController { get; private set; } /** User Instruction Controller */

    [SerializeField]
    private ARTrackedImageManager _trackedImageManager; /** AR Tracked Image Manager*/


    //------------------ DATABASES----------------------------
    [SerializeField]
    private PointOfInterestDB _poiDB; /** POI database*/
    public PointOfInterestDB POI_DB { get => _poiDB; } /** POI database*/

    [SerializeField]
    private CommentsDB _commentsDB; /** Comments database */
    public CommentsDB CommentsDB { get => _commentsDB; } /** Comments database */

    [SerializeField]
    private ThreadsDB _threadsDB; /** Threads database */
    public ThreadsDB ThreadsDB { get => _threadsDB; } /** Threads database */
    //---------------------------------------------------------

    [SerializeField]
    private bool _activateSessionAtStart; /** Activates Session right when starting the app. This doesn't require the scanning of an Image Target to activate the session. It is used for testing in the Editor*/
    private bool _sessionInProgress; /** Value which indicates if there is an AR session in progress*/


    void OnEnable() => _trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;

    void OnDisable() => _trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        SetManagers();
        SetupDatabases();

        if (_activateSessionAtStart)
        {
            ActivateSession();
        }
        else
        {
            InstructionController.ShowInstruction(Instructions.scanQRCode, 0, false);
        }
    }

    /** Behaviour executed once an image target is detected
     */
    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            //activate Session
            if (!_sessionInProgress)
            {
                ActivateSession();
            }
        }
    }

    /** Gets references for all the managers
     */
    public void SetManagers()
    {
        GUIController = GetComponentInChildren<MainGUIController>();
        POISelectionManager = GetComponentInChildren<POISelectionManager>();
        ModeManager = GetComponentInChildren<ModeManager>();
        POIMenuManager = GetComponentInChildren<POIMenuManager>();
        CommentManager = GetComponentInChildren<CommentManager>();
       InstructionController = GetComponentInChildren<UserInstructionController>();
    }

    /** Sets up the databases for the session. This is necessary to delete the void ScriptableObject instances from the previous session. 
     */
    public void SetupDatabases()
    {
        POI_DB.Setup();
        CommentsDB.Setup();
        ThreadsDB.Setup();
    }

    /** Activates the Session. 
     */
    private void ActivateSession()
    {
        _sessionInProgress = true;
        InstructionController.HideInstructionScreen();
        InstructionController.ShowInstruction(Instructions.switchMode, 7f, true);
        GUIController.ShowMainGUI(); 
        POISelectionManager.SetupPOIs();
        ModeManager.SetupSession();
    }




}
