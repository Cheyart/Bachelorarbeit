using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    [SerializeField]
    private User _loggedInUser;
    public User LoggedInUser { get => _loggedInUser; }


    public PointOfInterest ActivePOI { get; set; }

    [SerializeField]
    private PointOfInterestDB _poiDB;
    public PointOfInterestDB POI_DB { get => _poiDB;  }

    [SerializeField]
    private CommentsDB _commentsDB;
    public CommentsDB CommentsDB { get => _commentsDB; }

    [SerializeField]
    private ThreadsDB _threadsDB;
    public ThreadsDB ThreadsDB { get => _threadsDB; }

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
}
