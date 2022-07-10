using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**@class Thread This class defines a thread, which is made up of several comments*/
[CreateAssetMenu(fileName = "New Thread", menuName = "Assets/Thread")]
public class Thread : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int Id { get => _id; }

    [SerializeField]
    private List<Comment> _comments;
    public List <Comment> Comments { get => _comments; }

    private int _nrOfComments;
    public int NrOfComments { get => _nrOfComments; }

    private void Awake()
    {
        Debug.Log("Awake ScriptableObject Thread");
        _nrOfComments = _comments.Count;
    }

    public void AddComment (Comment newComment)
    {
        if(newComment != null)
        {
            _comments.Add(newComment);
            _nrOfComments++;
        }
    }
}
