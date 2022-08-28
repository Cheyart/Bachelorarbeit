using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/** @ class CommentPrefab This class is responsible for the initial setup of the Comment Prefabs
 */
public class CommentPrefab : MonoBehaviour
{
 

    [SerializeField]
    private TextMeshProUGUI _username; /** Text component displaying the poster of the comment*/

    [SerializeField]
    private TextMeshProUGUI _date; /** Text component displaying the date when the comment was posted*/

    [SerializeField]
    private TextMeshProUGUI _message; /** Text component displaying the comment*/

    [SerializeField]
    private Image _profilePic; /** Image component displaying the profile picture of the poster*/

    [SerializeField]
    private Color _highlightColor; /** Highlight Color for comments which have been posted by the currently active user*/

    private POIMenuManager _poiMenuManager; /** POI Menu Manager*/

    private Comment _comment; /** Comment displayed in this object*/

    /**
     * Sets up the Content
     * @param content Comment and related content which will be display
     * @param poiMenuManager reference to POIMenuManager
     */
    public void Setup(Comment content, POIMenuManager poiMenuManager)
    {
        _poiMenuManager = poiMenuManager;

        _comment = content;

        if(content.ReplyTo != -1)
        {
            User replyToPoster = SessionManager.Instance.CommentsDB.GetPosterOfComment(content.ReplyTo);


            if (replyToPoster != null && replyToPoster.Username != null)
            {
                _username.text = content.Poster.Username + "   @" + replyToPoster.Username;
            } else
            {
                _username.text = content.Poster.Username;
            }
        }
        else
        {
            _username.text = content.Poster.Username;
        }

        if(content.Poster == SessionManager.Instance.LoggedInUser)
        {
            _username.color = _highlightColor;
        }

        if(_profilePic != null)
        {
            _profilePic.sprite = content.Poster.ProfilePic;
        }

        _date.text = content.Date.ToString("dd.MM.yy");
        _message.text = content.Message;
        RectTransform rectTransform = _message.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);         //necessary for size adjustment

    }

    /**Reply Button Handler
     */
    public void ReplyButtonHandler()
    {
        _poiMenuManager.StartReplyInput(_comment);
    }
}
