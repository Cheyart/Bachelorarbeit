using System.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class POIMenuContentController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title; //main panel

    [SerializeField]
    private TextMeshProUGUI _commentsTitle;  //main panel

    [SerializeField]
    private TextMeshProUGUI _commentToReplyTo;  //comment input

    [SerializeField]
    private LayoutGroup _contentContainerLayoutGroup;  //menu manager (?)

    [SerializeField]
    private Transform _commentsContainer;  //Main panel

    [SerializeField]
    private CommentPrefab _mainCommentPrefab; //main panel

    [SerializeField]
    private CommentPrefab _subCommentPrefab; //main panel

    [SerializeField]
    private TMP_InputField _textInputField; //comment input

    private PointOfInterest _poi;

    private POIMenuManager _poiMenuManager;

    public void Init(POIMenuManager poiMenuManager)
    {
        _poiMenuManager = poiMenuManager;
    }


    public void Setup (PointOfInterest poi)
    {
        _poi = poi;
        Refresh();
    }

    public void Refresh()  //in menu manager
    {
        Reset();
        SetTitle(_poi.Title);
        SetCommentsTitle(_poi.NrOfComments);
        SetComments(_poi.Threads);

        StartCoroutine(UpdateLayoutGroup());
    }

    public void Reset()
    {
        ClearTextInputContent();
        foreach(Transform child in _commentsContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void SetCommentToReplyTo(string message)
    {
        _commentToReplyTo.text = message;
    }

    private void SetTitle(string title)
    {
        _title.text = title;
    }

    private void SetCommentsTitle(int nrOfComments)
    {
        _commentsTitle.text = "Comments (" + nrOfComments.ToString() + ")";
    }

    private void SetComments(List <Thread> threads)
    {
      
       for(int j=threads.Count-1; j>= 0; j--) 
        {

            for (int i =0; i<threads[j].Comments.Count; i++)
            {
                CommentPrefab instance;
                if (i == 0)
                {
                   instance = Instantiate(_mainCommentPrefab, _commentsContainer);
                }
                else
                {
                    instance = Instantiate(_subCommentPrefab, _commentsContainer);
                }
               
                instance.Setup(threads[j].Comments[i], _poiMenuManager);
            }
           
        }
          
    }

    IEnumerator UpdateLayoutGroup()
    {
        _contentContainerLayoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        _contentContainerLayoutGroup.enabled = true;
    }

    public string GetTextInputContent()
    {
        return _textInputField.text;
    }

    public void ClearTextInputContent()
    {
        _textInputField.text = "";
    }

    public void SetTextInputPlaceholderContent(POIMenuState replyState)
    {
        TextMeshProUGUI placeholder = (TextMeshProUGUI)_textInputField.placeholder;

        if (replyState == POIMenuState.replyInput)
        {
            placeholder.text = "Leave a reply...";
        } else
        {
            placeholder.text = "Leave a comment...";
        }
    }


}
