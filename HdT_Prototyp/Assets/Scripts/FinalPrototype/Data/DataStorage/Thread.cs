using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**@class Thread This class defines a thread, which is consists of comments*/
[CreateAssetMenu(fileName = "New Thread", menuName = "Assets/Thread")]
public class Thread : ScriptableObject
{
    private static int IdCounter = 100;

    [SerializeField]
    private int _id;
    public int Id { get => _id; }

    [SerializeField]
    private List<Comment> _comments;
    public List <Comment> Comments { get => _comments; }

    public void Init(Comment firstComment)
    {
        _id = IdCounter++;
        firstComment.BelongsToThread = _id;
        _comments = new List<Comment>();
        _comments.Add(firstComment);
    }


    public void AddComment (Comment newComment)
    {
        if(newComment != null)
        {
            newComment.BelongsToThread = _id;
            _comments.Add(newComment);
        }
    }


}
