using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/** @class CommentInputSection This class controls the display and behaviour of the Comment Input Section of the POI Menu
 */
public class CommentInputSection : MonoBehaviour
{
    [SerializeField]
    private CommentInputField _commentInputField; /** Comment input field*/

    [SerializeField]
    private GameObject _replyToSection; /** section displaying the comment to reply to*/

    [SerializeField]
    private TextMeshProUGUI _commentToReplyTo; /** Text component displaying the comment to which a reply is written*/

    [SerializeField]
    private GameObject _buttons; /** Buttons of the comment input section*/

    [SerializeField]
    private GameObject _scrollBar; /** Scrollbar of the comment input field*/

    [SerializeField]
    private Image _profilePic; /** current user's profile picture*/


    void Start()
    {
        _profilePic.sprite = SessionManager.Instance.LoggedInUser.ProfilePic;
    }

    /** Sets the comment to which a reply is written
     * @param message content of this comment
     */
    public void SetCommentToReplyTo(string message)
    {
        _commentToReplyTo.text = "\n" + message;
    }

    /**
     * Sets the Placeholder text for the comment input field according to the reply state
     * @param replyState comment reply state (new comment or reply)
     */
    public void SetTextInputPlaceholderContent(POIMenuState replyState)
    {
        _commentInputField.SetPlaceholderContent(replyState);
    }

    /**
     * return the content of the comment input field
     * @return conent from comment input field
     */
    public string GetTextInputContent()
    {
        return _commentInputField.GetContent();
    }

    /**
     * clears the comment input field
     */
    public void ClearTextInputContent()
    {
        _commentInputField.ClearContent();
    }

    /**
     * sets the comment input field height
     * @param height Height to which the input field will be set to
     */
    public void SetTextInputHeight(float height)
    {
        _commentInputField.SetHeight(height);
    }

    /**
     * Executes an animated transition of the comment input field, to a target height
     * @param targetHeight Height to which will be transitioned
     * @param duration Duration of the transition
     * @param easing Easing function of the animation
     */
    public IEnumerator AnimatedTextInputHeightChange(float targetHeight, float duration, EasingFunction easing)
    {
        yield return _commentInputField.AnimatedHeightChange(targetHeight, duration, easing);
    }

    /**
     * Sets visibility of comment input section elements
     * @param value true if elements should be set to visible
     */
    public void ShowCommentInputElements(bool value)
    {
        _buttons.SetActive(value);
        _scrollBar.SetActive(value);
    }

    /**
    * Sets visibility of the "reply to comment" elements
    * @param value true if elements should be set to visible
    */
    public void ShowReplyInputElements(bool value)
    {
        _replyToSection.SetActive(value);
    }


}
