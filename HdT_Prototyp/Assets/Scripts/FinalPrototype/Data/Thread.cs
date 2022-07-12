using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**@class Thread This class defines a thread, which is made up of several comments*/
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

    public int NrOfComments { get => _comments.Count; }

    

    public void Init(Comment firstComment)
    {
        _id = IdCounter++;
        _comments = new List<Comment>();
        _comments.Add(firstComment);
    }


    /*public void AddComment (Comment newComment)
    {
        if(newComment != null)
        {
            _comments.Add(newComment);
            _nrOfComments++;
        }
    }*/


}
