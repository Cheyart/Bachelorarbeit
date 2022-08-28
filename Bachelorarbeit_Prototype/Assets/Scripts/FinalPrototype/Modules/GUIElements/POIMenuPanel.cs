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
    private CommentContainer _commentsContainer; /** Comments container*/

    [SerializeField]
    private ScrollMask _scrollMask; /** Scroll Mask*/

    [SerializeField]
    private GameObject _commentsHeader; /** Header of the comments section*/

    [SerializeField]
    private TextMeshProUGUI _title; /** Text Compoent displaying the POI title*/

    [SerializeField]
    private TextMeshProUGUI _commentsTitle; /** Text Compoent displaying the comments title (amount of comments)*/

    [SerializeField]
    private RefreshLayoutGroup _layoutGroup; /** Layout Group placed highest in the hierarchy*/

    [SerializeField]
    private GameObject _handleBar; /** Handlerbar*/


    [SerializeField]
    private GameObject _scrollbarHandle; /** Scrollbar Handle*/


    private PointOfInterest _currentPoi; /** POI currently displayed*/

    private RectTransform _rectTransform; /** RectTransform Component*/
                                           

    private AnimatedRectTransform _animation; /** Animator for the RectTransform Component*/


    private void Start()
    {
        SetComponents();
    }

    /** Sets the Components for this class */
    private void SetComponents()
    {
        _rectTransform = GetComponent<RectTransform>();
        _animation = GetComponent<AnimatedRectTransform>();

    }

    /**
     * Sets the content of the menu panel
     * @param poi POI which the content will be set to
     */
    public void SetupContent(PointOfInterest poi)
    {
        _currentPoi = poi;
        RefreshContent();
    }

    /** Refresh the content and layout
     */
    public void RefreshContent()
    {
        ResetContent();
        SetTitle(_currentPoi.Title);
        SetCommentsTitle(_currentPoi.NrOfComments);
        SetComments(_currentPoi.Threads);
        _layoutGroup.NeedsRefresh = true;
    }

    /**
     * Reset the content to its default
     */
    public void ResetContent()
    {
        _commentsContainer.DeleteContent();
    }

    /**
     * Set Visibility of the Main Panel
     * @param value true if it should be set to visible
     */
    public void ShowContent(bool value)
    {
        _commentsContainer.gameObject.SetActive(value);
        _commentsHeader.SetActive(value);
        _scrollbarHandle.SetActive(value);
        _handleBar.SetActive(value);
    }

    /**
     * Set the y-Position of the main panel
     */
    public void SetYPosition(float targetYPos)
    {
        if (_rectTransform == null)
        {
            SetComponents();
        }

        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, targetYPos);
    }

    /**
     * Execute an animated transition to a y position
     * @param targetYPos Target y position
     * @param duration Duration of the animation
     * @param easing Easing funtion used for the animation
     */
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

    /**
     * Set the POI Title
     * @param title
     */
    private void SetTitle(string title)
    {
        _title.text = title;
    }

    /**
     * Set the comments title
     * @param nrOfComments
     */
    private void SetCommentsTitle(int nrOfComments)
    {
        _commentsTitle.text = "Comments (" + nrOfComments.ToString() + ")";
    }

    /**
     * Set up the Comments
     * @param threads
     */
    private void SetComments(List<Thread> threads)
    {
        _commentsContainer.SetComments(threads);

    }



}
