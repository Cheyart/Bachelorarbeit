using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentContainer : MonoBehaviour
{
    //get from Session Manager
    [SerializeField]
    private POIMenuManager _poiMenuManager;

    [SerializeField]
    private CommentPrefab _mainCommentPrefab;

    [SerializeField]
    private CommentPrefab _subCommentPrefab;


    public void DeleteContent()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetComments(List<Thread> threads)
    {

        for (int j = threads.Count - 1; j >= 0; j--)
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
