using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ThreadsDB", menuName = "Assets/ThreadsDB")]
public class ThreadsDB : ScriptableObject
{
    [SerializeField]
    private List<Thread> _threadsDB;

    public void Setup()
    {
        _threadsDB.RemoveAll(t => t == null);

    }

    public void AddThread(Thread newThread)
    {
        _threadsDB.Add(newThread);

    }

   
}
