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

    private RectTransform _containerLayoutGroup;

    [SerializeField]
    private Transform _mainCommentPrefab;

   // private CommentsContainerSetup _currentCommentsContainer;

    // Start is called before the first frame update
    void Start()
    {
        _containerLayoutGroup = _commentsContainer.GetComponent<RectTransform>();
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

        //rebuild layout group, to adjust layout to content fitters (double refresh necessary, because of nested content fitters)
        LayoutRebuilder.ForceRebuildLayoutImmediate(_containerLayoutGroup);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_containerLayoutGroup);
    }

    private void Reset()
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
        // _currentCommentsContainer = Instantiate(_commentsContainerPrefab, _commentsContainerParent);
        //_currentCommentsContainer.Setup(_mainCommentPrefab, threads);
        foreach (Thread thread in threads)
        {
            Instantiate(_mainCommentPrefab, _commentsContainer);
        }
       
        
    }

   /*IEnumerator UpdateLayoutGroup()
    {
       
    }*/
}
