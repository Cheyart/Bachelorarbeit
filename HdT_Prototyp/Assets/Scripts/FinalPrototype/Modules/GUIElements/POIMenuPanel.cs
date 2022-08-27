using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/** @class POIMenuPanel This class controls the the setup and behaviour of the POI menu's main panel
 */
[RequireComponent(typeof(RectTransform), typeof(AnimatedRectTransform))]
public class POIMenuPanel : MonoBehaviour
{

    [SerializeField]
    private CommentContainer _commentsContainer;

    [SerializeField]
    private ScrollMask _scrollMask;

    [SerializeField]
    private GameObject _commentsHeader;

    [SerializeField]
    private TextMeshProUGUI _title;

    [SerializeField]
    private TextMeshProUGUI _commentsTitle;

    [SerializeField]
    private RefreshLayoutGroup _layoutGroup;

    [SerializeField]
    private GameObject _handleBar;


    [SerializeField]
    private GameObject _scrollbarHandle; 


    private PointOfInterest _currentPoi;

    private RectTransform _rectTransform;

    private AnimatedRectTransform _animation;


    private void Start()
    {
        SetComponents();
    }

    private void SetComponents()
    {
        _rectTransform = GetComponent<RectTransform>();
        _animation = GetComponent<AnimatedRectTransform>();

    }

    public void SetupContent(PointOfInterest poi)
    {
        _currentPoi = poi;
        RefreshContent();
    }


    public void RefreshContent()
    {
        ResetContent();
        SetTitle(_currentPoi.Title);
        SetCommentsTitle(_currentPoi.NrOfComments);
        SetComments(_currentPoi.Threads);
        _layoutGroup.NeedsRefresh = true;
    }

    public void ResetContent()
    {
        _commentsContainer.DeleteContent();
    }

    public void ShowContent(bool value)
    {
        _commentsContainer.gameObject.SetActive(value);
        _commentsHeader.SetActive(value);
        _scrollbarHandle.SetActive(value);
        _handleBar.SetActive(value);
    }


    public void SetYPosition(float targetYPos)
    {
        if (_rectTransform == null)
        {
            SetComponents();
        }

        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, targetYPos);
    }

    public IEnumerator AnimatedTranslateOnYAxis(float targetYPos, float duration, EasingFunction easing)
    {
        if (_animation == null)
        {
            SetComponents();
        }

        Vector2 targetPosition = new Vector2(_rectTransform.anchoredPosition.x, targetYPos);
        yield return StartCoroutine(_animation.LerpPosition(_rectTransform.anchoredPosition, targetPosition, duration, easing));
        _scrollMask.SetHeight(_rectTransform.anchoredPosition.y);
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
