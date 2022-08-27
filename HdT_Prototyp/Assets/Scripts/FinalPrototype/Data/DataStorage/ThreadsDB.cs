using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/** @class ThreadsDB This class represents the Thread database, consisting of all the Threads in the system*/
[CreateAssetMenu(fileName = "New ThreadsDB", menuName = "Assets/ThreadsDB")]
public class ThreadsDB : ScriptableObject
{
    [SerializeField]
    private List<Thread> _threadsDB; /** List simulating the thread database */

    /** Sets up the database (removes void entries and assigns Ids)
     */
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

    /** Add a new thread to the database
     * @param newThread Thread to be added
     */
     
    public void AddThread(Thread newThread)
    {
        _threadsDB.Add(newThread);
    }

    /** Return a thread by Id. Returns null if no match is found.
     * @param if Id of the thread which will be returned.
     * @return Thread which will be returned
     */
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
