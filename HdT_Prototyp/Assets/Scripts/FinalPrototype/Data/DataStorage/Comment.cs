using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**@class Comment This class defines a single comment posted by a user*/
[CreateAssetMenu(fileName = "New Comment", menuName = "Assets/Comment")]
public class Comment : ScriptableObject, ISerializationCallbackReceiver
{
    private static int IdCounter = 100;

    [SerializeField]
    private int _id;  /** comment id. has to be unique*/
    public int Id { get => _id; }

    [SerializeField]
    private User _poster; /** user who posted the comment */
    public User Poster { get => _poster; }

    [SerializeField]
    private string _message; /** comment content*/
    public string Message { get => _message; }

    [SerializeField]
    private int _replyTo; /** id of comment to which this comment is a reply. Is -1 if this comment is not a reply to any comment */
    public int ReplyTo { get => _replyTo; }

    [SerializeField]
    private int _belongsToThread;
    public int BelongsToThread { get => _belongsToThread; set => _belongsToThread = value; }

    private DateTime _date; /** date on which the comment was posted*/
    public DateTime Date { get => _date; }

    [SerializeField]
    [Range(2021, 2022)]
    private int _year = 2021; /**Year in which the comment was posted. (range is just for the sake of this prototype; year, month, day saved separately for the sake of serialization) */

    [SerializeField]
    [Range(1, 12)]
    private int _month; /**Month in which the comment was posted. (year, month, day saved separately for the sake of serialization)*/

    [SerializeField]
    [Range(1, 31)]
    private int _day = 1; /**Day on which the comment was posted. (year, month, day saved separately for the sake of serialization*/

    /** Initialized a new Comment
     * @param poster User who posted the comment
     * @param message Message which will be saved
     * @param replyTo Comment to which this comment is a reply to (-1 if it is not a reply to any comment)
     */
    public void Init(User poster, string message, int replyTo)
    {
   
        _id = IdCounter++;

        _poster = poster;
        _message = message;
        _replyTo = replyTo;
        _date = DateTime.Today;
    }

    /**
     * transforms the single date attribute into separate attributes (year, month, day) 
     */
    public void OnBeforeSerialize()
    {
        _year = _date.Year;
        _month = _date.Month;
        _day = _date.Day;
    }

    /**
     * transforms the separate date attributes (year, month, day) into a single date attribute 
     */
    public void OnAfterDeserialize()
    {
        _date = new DateTime(_year, _month, _day);
    }

}
