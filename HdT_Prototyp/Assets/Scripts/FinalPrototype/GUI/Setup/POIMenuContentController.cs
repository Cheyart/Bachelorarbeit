using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class POIMenuContentController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private LayoutGroup _contentContainerLayoutGroup;

    [SerializeField]
    private Transform _commentsContainer;

    [SerializeField]
    private CommentContentSetup _mainCommentPrefab;

    [SerializeField]
    private CommentContentSetup _subCommentPrefab;

    [SerializeField]
    private TMP_InputField _textInputField;

    private PointOfInterest _poi;


    public void Setup (PointOfInterest poi)
    {
        _poi = poi;
        Refresh();
    }

    public void Refresh()
    {
        Reset();
        SetTitle(_poi.Title);
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

    public string GetTextInputConent()
    {
        return _textInputField.text;
    }

    public void ClearTextInputContent()
    {
        _textInputField.text = "";
    }


}
