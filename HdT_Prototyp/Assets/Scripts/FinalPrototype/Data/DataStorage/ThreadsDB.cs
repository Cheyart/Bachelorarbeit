using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/** @class ThreadsDB This class represents the Thread database, consisting of all the Threads in the system*/
[CreateAssetMenu(fileName = "New ThreadsDB", menuName = "Assets/ThreadsDB")]
public class ThreadsDB : ScriptableObject
{
    [SerializeField]
    private List<Thread> _threadsDB;

    public void Setup()
    {
        _threadsDB.RemoveAll(t => t == null);

        foreach (Thread thread in _threadsDB)
        {
            thread.Comments.RemoveAll(c => c == null);

            foreach (Comment comment in thread.Comments)
            {
                comment.BelongsToThread = thread.Id;
            }
        }
    }

    public void AddThread(Thread newThread)
    {
        _threadsDB.Add(newThread);
    }

    public Thread GetThreadById(int id)
    {
        foreach (Thread thread in _threadsDB)
        {
            if (thread.Id == id)
            {
                return thread;
            }
        }
        return null;
    }
}
