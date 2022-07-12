using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CommentPrefab : MonoBehaviour
{
 
    private POIMenuManager _poiMenuManager;

    [SerializeField]
    private TextMeshProUGUI _username;

    [SerializeField]
    private TextMeshProUGUI _date;

    [SerializeField]
    private TextMeshProUGUI _message;

    [SerializeField]
    private Sprite _profilePic;

    [SerializeField]
    private RectTransform _layoutGroup;

    [SerializeField]
    private CommentsDB _commmentsDB;

    [SerializeField]
    private Color _highlightColor;

    private Comment _comment;


    public void Setup(Comment content, POIMenuManager poiMenuManager)
    {
        _poiMenuManager = poiMenuManager;

        _comment = content;

        if(content.ReplyTo != -1)
        {
            User replyToPoster = _commmentsDB.GetPosterOfComment(content.ReplyTo);
            if(replyToPoster != null && replyToPoster.Username != null)
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
        _date.text = content.Date.ToString("dd.MM.yy");
        _message.text = content.Message;
        //update layout group (necessary for size adjustment)
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroup);
    }

    public void ReplyButtonHandler()
    {
        _poiMenuManager.StartReplyInput(_comment);
    }
}
