using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class PointOfInterest This class defines a Point of Interest (POI) and its contents*/
[CreateAssetMenu(fileName = "New POI", menuName = "Assets/POI")]
public class PointOfInterest : ScriptableObject
{

    [SerializeField]
    private int _id; /** unique ID*/
    public int Id { get => _id; } /** unique ID*/

    [SerializeField]
    private string _title; /** Title of the POI */
    public string Title { get => _title; } /** Title of the POI */

    [SerializeField]
    private List<Thread> _threads; /** Comment Threads associated with the POI */
    public List<Thread> Threads { get => _threads; }  /** Comment Threads associated with the POI */

    [SerializeField]
    private Sprite _picture; /** Picture showing the POI */
    public Sprite Picture { get => _picture; } /** Picture showing the POI */

    private int _nrOfComments; /** Total Nr of comments saved for this POI */
    public int NrOfComments { get => _nrOfComments; } /** Total Nr of comments saved for this POI */


    private void Awake()
    {
        SetNrOfComments();
    }

    /** Adds a new comment thread to this POI
     * @param newThread Thread to be added
     */
    public void AddNewThread(Thread newThread)
    {
        _threads.Add(newThread);
        _nrOfComments += newThread.Comments.Count;
    }

    /** Determines total number of comments saved for this POI
     */
    public void SetNrOfComments()
    {

        _nrOfComments = 0;

        foreach (Thread thread in _threads)
        {
            _nrOfComments += thread.Comments.Count;
        }
    }
}

