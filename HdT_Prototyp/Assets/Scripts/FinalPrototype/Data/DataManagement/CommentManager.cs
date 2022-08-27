using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class CommentManager manages the comments.
 */
public class CommentManager : MonoBehaviour
{

    /** Adds a new Comment to a specified POI and to the Comments database
     * @param poi POI which the comment will be added to
     * @param poster User who posted the comment
     * @param message Message which will be saved
     */
    public void AddNewComment(PointOfInterest poi, User poster, string message)
    {

        Comment newComment = InstantiateNewComment(poster, message, -1);

        Thread newThread = ScriptableObject.CreateInstance("Thread") as Thread;
        newThread.Init(newComment);
        SessionManager.Instance.ThreadsDB.AddThread(newThread);

        poi.AddNewThread(newThread);
    }

    /** Adds a new reply comment to a thread and to the Comments database
    * @param poster User who posted the comment
    * @param message Message which will be saved
    * @param commentToReplyTo Comment to which the reply will be saved
    */
    public void AddReply(User poster, string message, Comment commentToReplyTo)
    {

        Comment newComment = InstantiateNewComment(poster, message, commentToReplyTo.Id);

        Thread thread = SessionManager.Instance.ThreadsDB.GetThreadById(commentToReplyTo.BelongsToThread);
        if (thread != null)
        {
            thread.AddComment(newComment);
        }
    }

    /** Instantiates a new Comment ScriptableObject
     * @param poster User who posted the comment
     * @param message Message which will be saved
     * @param replyTo ID of the comment which the new comment is a reply to (-1 if it is not a reply to any comment)
     */
    private Comment InstantiateNewComment(User poster, string message, int replyTo)
    {
        Comment newComment = ScriptableObject.CreateInstance("Comment") as Comment;
        newComment.Init(poster, message, replyTo);
        SessionManager.Instance.CommentsDB.AddComment(newComment);
        return newComment;
    }
}
