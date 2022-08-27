using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**@class CommentsDB This class represents the comments DB, containing all the comments poses by users*/

[CreateAssetMenu(fileName = "New CommentsDB", menuName = "Assets/CommentsDB")]
public class CommentsDB : ScriptableObject
{
    [SerializeField]
    private List<Comment> _commentsDB;


    public void Setup()
    {
        _commentsDB.RemoveAll(c => c == null);
    }


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


    public void AddComment(Comment newComment)
    {
        _commentsDB.Add(newComment);
    }
}
