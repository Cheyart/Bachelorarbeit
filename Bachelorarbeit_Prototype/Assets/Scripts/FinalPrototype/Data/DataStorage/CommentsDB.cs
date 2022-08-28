using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**@class CommentsDB This class represents the comments DB, containing all the comments poses by users*/

[CreateAssetMenu(fileName = "New CommentsDB", menuName = "Assets/CommentsDB")]
public class CommentsDB : ScriptableObject
{
    [SerializeField]
    private List<Comment> _commentsDB; /** List which simulated the database*/

    /** Cleans up the database (deletes null entries)
     */
    public void Setup()
    {
        _commentsDB.RemoveAll(c => c == null);
    }

    /** Returns a comment by Id. If no comment is found, null will be returned
     * @param id Id of the Comment which will be returned
     * @return Comment with the given id
     */
    public Comment GetCommentById(int id)
    {
        //return _commentsDB.First(c => c.Id == id);
        foreach (Comment comment in _commentsDB)
        {
            if (comment.Id == id)
            {
                return comment;
            }
        }
        return null;
    }

    /** Returns the User who posted a specific comment
     * @param id Id of the comment for which the poster will be returned
     * @return User User who posted the comment
     */
    public User GetPosterOfComment(int id)
    {
        Comment comment = GetCommentById(id);

        if (comment != null)
        {
            return comment.Poster;
        }
        else
        {
            return null;
        }
    }

    /** Adds a new comment to the database
     * @param newComment new comment to be addded to the database
     */
    public void AddComment(Comment newComment)
    {
        _commentsDB.Add(newComment);
    }
}
