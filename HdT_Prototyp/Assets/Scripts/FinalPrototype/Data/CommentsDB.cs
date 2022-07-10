using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CommentsDB", menuName = "Assets/CommentsDB")]
public class CommentsDB : ScriptableObject
{
    [SerializeField]
    private List<Comment> _commentsDB;


   public Comment GetCommentById(int id)
    {
        foreach(Comment comment in _commentsDB)
        {
            if(comment.Id == id)
            {
                return comment;
            }
        }
        return null;
    }

    public User GetPosterOfComment(int id)
    {
        foreach (Comment comment in _commentsDB)
        {
            if (comment.Id == id)
            {
                return comment.Poster;
            }
        }
        return null;
    }


    public void AddComment(Comment newComment)
    {
        _commentsDB.Add(newComment);
    }
}
