using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New POI", menuName = "Assets/POI")]
public class PointOfInterest : ScriptableObject
{

    [SerializeField]
    private int _id;
    public int Id { get => _id;}

    [SerializeField]
    private string _title;
    public string Title { get => _title;}

    [SerializeField]
    private List<Thread> _threads;
    public List<Thread> Threads { get => _threads; }

    /*public void AddNewThread(Comment firstComment)
    {
        Thread newThread = ScriptableObject.CreateInstance("Thread") as Thread;
        newThread.Init(firstComment);
        _threads.Add(newThread);
    }*/
}

