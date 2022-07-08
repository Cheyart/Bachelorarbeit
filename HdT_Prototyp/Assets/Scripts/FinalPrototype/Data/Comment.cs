using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**@class Comment This class defines a single comment posted by a user*/
[CreateAssetMenu(fileName = "New Comment", menuName = "Assets/Comment")]
public class Comment : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    private int _id;  /** comment id. has to be unique*/

    [SerializeField]
    private User _poster; /** user who posted the comment */

    [SerializeField]
    private string _message; /** comment content*/

    [SerializeField]
    private int _replyTo; /** id of comment of which this comment is a reply to */

    private DateTime _date; /** date on which the comment was posted*/

    [SerializeField]
    [Range(2021, 2022)]
    private int _year = 2021; /**Year in which the comment was posted. (range is just for the sake of this prototype; year, month, day saved separately for the sake of serialization) */

    [SerializeField]
    [Range(1, 12)]
    private int _month; /**Month in which the comment was posted. (year, month, day saved separately for the sake of serialization)*/

    [SerializeField]
    [Range(1, 31)]
    private int _day = 1; /**Day on which the comment was posted. (year, month, day saved separately for the sake of serialization*/


    //SetComment()


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
