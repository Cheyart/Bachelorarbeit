using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//TO DO: Vererbung für Comment und Reply Prefab 
public class CommentPrefab : MonoBehaviour
{
 

    [SerializeField]
    private TextMeshProUGUI _username;

    [SerializeField]
    private TextMeshProUGUI _date;

    [SerializeField]
    private TextMeshProUGUI _message;

    [SerializeField]
    private Image _profilePic;

    [SerializeField]
    private Color _highlightColor;

    private POIMenuManager _poiMenuManager;

    private Comment _comment;


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
        //update layout group (necessary for size adjustment)
        RectTransform rectTransform = _message.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void ReplyButtonHandler()
    {
        _poiMenuManager.StartReplyInput(_comment);
    }
}
