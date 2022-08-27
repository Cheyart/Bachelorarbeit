using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/** @class CommentInputField This class controls the setup and behaviour of the text input field for the comment/reply input
 */
[RequireComponent(typeof(TMP_InputField), typeof(AnimatedRectTransform), typeof(RectTransform))]
public class CommentInputField : MonoBehaviour
{
    private TMP_InputField _inputField; /** Comment input field*/
    private AnimatedRectTransform _animation; /**Animator for the RectTransform component*/
    private RectTransform _rectTransform; /** RectTransform component of this object*/

    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        _animation = GetComponent<AnimatedRectTransform>();
        _rectTransform = GetComponent<RectTransform>();
    }

    /**
     * Sets the placeholder text in the comment input field according to the reply state 
     * @param replyState comment reply state (new comment or reply comment)
     */
    public void SetPlaceholderContent(POIMenuState replyState)
    {
        TextMeshProUGUI placeholder = (TextMeshProUGUI)_inputField.placeholder;

        if (replyState == POIMenuState.replyInput)
        {
            placeholder.text = "Leave a reply...";
        }
        else
        {
            placeholder.text = "Leave a comment...";
        }
    }

    /** get the content from the comment input field*/
    public string GetContent()
    {
        return _inputField.text;
    }

    /** clear content from the comment input field */
    public void ClearContent()
    {
        _inputField.text = "";
    }

    /** Set the height of the comment input field*/
    public void SetHeight(float height)
    {
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, height);
    }

    /** Execute an animated transition to a target height
     * @param targetHeight Height to which will be transitioned
     * @param duration Duration of the transition
     * @param easing Easing function of the animation
     */
    public IEnumerator AnimatedHeightChange(float targetHeight, float duration, EasingFunction easing)
    {
        Vector2 targetSize = new Vector2(_rectTransform.sizeDelta.x, targetHeight);
        yield return _animation.LerpSize(_rectTransform.sizeDelta, targetSize, duration, easing);
    }


}
