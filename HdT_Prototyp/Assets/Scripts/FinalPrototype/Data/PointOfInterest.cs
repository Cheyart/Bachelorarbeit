using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int _nrOfComments;
    public int NrOfComments { get => _nrOfComments; }


    private void Awake()
    {
        //Debug.Log("Inside POI Scriptable Object Awake");
        SetNrOfComments();
    }

    public void AddNewThread(Thread newThread)
    {
        _threads.Add(newThread);
        _nrOfComments += newThread.NrOfComments;

        //Debug.Log("Thread added to POI: ");

    }


    public void SetNrOfComments()
    {

        _nrOfComments = 0;

        foreach(Thread thread in _threads)
        {
            _nrOfComments += thread.Comments.Count;
        }
    }
}

