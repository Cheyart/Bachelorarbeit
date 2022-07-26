using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class POIMenuPanel : MonoBehaviour
{
   
    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private TextMeshProUGUI _commentsTitle;

    [SerializeField]
    private CommentContainer _commentsContainer;

    [SerializeField]
    private RefreshLayoutGroup _layoutGroup;

    private PointOfInterest _currentPoi;

    public void Setup(PointOfInterest poi)
    {
        _currentPoi = poi;
        Refresh();
    }

    public void Refresh()  
    {
        Reset();
        SetTitle(_currentPoi.Title);
        SetCommentsTitle(_currentPoi.NrOfComments);
        SetComments(_currentPoi.Threads);
        _layoutGroup.NeedsRefresh = true;
    }

    public void Reset()
    {
        _commentsContainer.DeleteContent();
    }

    private void SetTitle(string title)
    {
        _title.text = title;
    }

    private void SetCommentsTitle(int nrOfComments)
    {
        _commentsTitle.text = "Comments (" + nrOfComments.ToString() + ")";
    }

    private void SetComments(List<Thread> threads)
    {
        _commentsContainer.SetComments(threads);

    }


 

}
