using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class POIMenuContentSetup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private Transform _commentsContainer;

    [SerializeField]
    private LayoutGroup _contentContainerLayoutGroup;

    [SerializeField]
    private CommentContentSetup _mainCommentPrefab;

    [SerializeField]
    private CommentContentSetup _subCommentPrefab;

  

    // private CommentsContainerSetup _currentCommentsContainer;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup (PointOfInterest content)
    {
        Reset();
        SetTitle(content.Title);
        SetComments(content.Threads);

        StartCoroutine(UpdateLayoutGroup());
    }

    public void Reset()
    {
        foreach(Transform child in _commentsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    private void SetTitle(string title)
    {
        _title.text = title;
    }

    private void SetComments(List <Thread> threads)
    {
      
       foreach (Thread thread in threads)
        {
            for (int i =0; i<thread.Comments.Count; i++)
            {
                CommentContentSetup instance;
                if (i == 0)
                {
                   instance = Instantiate(_mainCommentPrefab, _commentsContainer);
                }
                else
                {
                    instance = Instantiate(_subCommentPrefab, _commentsContainer);
                }
               
                instance.Setup(thread.Comments[i]);
            }
           
        }
          
    }

    IEnumerator UpdateLayoutGroup()
    {
        _contentContainerLayoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        _contentContainerLayoutGroup.enabled = true;
    }


}
