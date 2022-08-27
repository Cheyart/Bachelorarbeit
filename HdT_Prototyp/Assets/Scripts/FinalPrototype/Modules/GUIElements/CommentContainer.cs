using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class CommentContainer This class is responsible for adding and deleting content from the Comment Container
 */
public class CommentContainer : MonoBehaviour
{
    //get from Session Manager
    //[SerializeField]
    private POIMenuManager _poiMenuManager;

    [SerializeField]
    private CommentPrefab _mainCommentPrefab;

    [SerializeField]
    private CommentPrefab _subCommentPrefab;

    public void Start()
    {
        _poiMenuManager = SessionManager.Instance.POIMenuManager;
    }

    public void DeleteContent()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetComments(List<Thread> threads)
    {

        for (int j = 0; j < threads.Count; j++)
        {

            for (int i = 0; i < threads[j].Comments.Count; i++)
            {
                CommentPrefab instance;
                if (i == 0)
                {
                    instance = Instantiate(_mainCommentPrefab, transform);
                }
                else
                {
                    instance = Instantiate(_subCommentPrefab, transform);
                }

                instance.Setup(threads[j].Comments[i], _poiMenuManager);
            }

        }

    }
}
