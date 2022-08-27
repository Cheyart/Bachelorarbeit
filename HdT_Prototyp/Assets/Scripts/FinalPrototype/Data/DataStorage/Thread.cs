using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**@class Thread This class defines a thread, which is consists of comments*/
[CreateAssetMenu(fileName = "New Thread", menuName = "Assets/Thread")]
public class Thread : ScriptableObject
{
    private static int IdCounter = 100;

    [SerializeField]
    private int _id; /** unique ID*/
    public int Id { get => _id; } /** unique ID*/

    [SerializeField]
    private List<Comment> _comments; /** List of comments saved in this thread */
    public List <Comment> Comments { get => _comments; } /** List of comments saved in this thread */

    /** Initializes a new thread
     * @param firstComment first comment to be saved in this thread
     */
    public void Init(Comment firstComment)
    {
        _id = IdCounter++;
        firstComment.BelongsToThread = _id;
        _comments = new List<Comment>();
        _comments.Add(firstComment);
    }

    /**
     * Add a new comment
     * @param newComment Comment to be added
     */
    public void AddComment (Comment newComment)
    {
        if(newComment != null)
        {
            newComment.BelongsToThread = _id;
            _comments.Add(newComment);
        }
    }


}
