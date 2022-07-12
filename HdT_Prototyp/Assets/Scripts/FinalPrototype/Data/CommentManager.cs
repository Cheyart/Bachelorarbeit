using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentManager : MonoBehaviour
{
    
    //private CommentsDB _commentsDB;
   // private ThreadsDB _threadsDB;

    private void Awake()
    {
    }

    public void AddNewComment(PointOfInterest poi, User poster, string message)
    {
        //add backend
        Comment newComment = InstantiateNewComment(poster, message, -1);

        Thread newThread = ScriptableObject.CreateInstance("Thread") as Thread;
        newThread.Init(newComment);
        SessionManager.Instance.ThreadsDB.AddThread(newThread);

        poi.AddNewThread(newThread);
    }

    public void AddReply(User poster, string message, Comment commentToReplyTo)
    {
        Comment newComment = InstantiateNewComment(poster, message, commentToReplyTo.Id);

        Thread thread = SessionManager.Instance.ThreadsDB.GetThreadById(commentToReplyTo.BelongsToThread);
        if(thread != null)
        {
            thread.AddComment(newComment);
        }
    }

    private Comment InstantiateNewComment(User poster, string message, int replyTo)
    {
        Comment newComment = ScriptableObject.CreateInstance("Comment") as Comment;
        newComment.Init(poster, message, replyTo);
        SessionManager.Instance.CommentsDB.AddComment(newComment);
        return newComment;
    }
}
