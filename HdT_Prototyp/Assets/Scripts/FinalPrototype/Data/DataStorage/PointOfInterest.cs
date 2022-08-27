using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class PointOfInterest This class defines a Point of Interest (POI) and its contents*/
[CreateAssetMenu(fileName = "New POI", menuName = "Assets/POI")]
public class PointOfInterest : ScriptableObject
{

    [SerializeField]
    private int _id;
    public int Id { get => _id; }

    [SerializeField]
    private string _title;
    public string Title { get => _title; }

    [SerializeField]
    private List<Thread> _threads;
    public List<Thread> Threads { get => _threads; }

    [SerializeField]
    private Sprite _picture;
    public Sprite Picture { get => _picture; }

    private int _nrOfComments;
    public int NrOfComments { get => _nrOfComments; }


    private void Awake()
    {
        SetNrOfComments();
    }

    public void AddNewThread(Thread newThread)
    {
        _threads.Add(newThread);
        _nrOfComments += newThread.Comments.Count;
    }


    public void SetNrOfComments()
    {

        _nrOfComments = 0;

        foreach (Thread thread in _threads)
        {
            _nrOfComments += thread.Comments.Count;
        }
    }
}

