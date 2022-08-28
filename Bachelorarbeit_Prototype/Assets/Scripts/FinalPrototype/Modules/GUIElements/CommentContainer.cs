using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class CommentContainer This class is responsible for adding and deleting content from the Comment Container
 */
public class CommentContainer : MonoBehaviour
{

    private POIMenuManager _poiMenuManager; /** POI Menu Manager*/

    [SerializeField]
    private CommentPrefab _mainCommentPrefab; /** Main Comment Prefab*/

    [SerializeField]
    private CommentPrefab _subCommentPrefab; /** Sub Comment (Reply) Prefab */

    public void Start()
    {
        _poiMenuManager = SessionManager.Instance.POIMenuManager;
    }

    /** Deletes current content*/
    public void DeleteContent()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    /** Sets content to new comments
     * @param threads Threads which will be added to the container
     */
    public void SetComments(List<Thread> threads)
    {

        //for (int j = 0; j < threads.Count; j++)
        for (int j = threads.Count-1; j >= 0; j--)
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
