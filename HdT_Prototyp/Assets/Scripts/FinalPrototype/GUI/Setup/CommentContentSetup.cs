using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CommentContentSetup : MonoBehaviour
{
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


    public void Setup(Comment content)
    {
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
        _date.text = content.Date.ToString("dd.MM.yy");
        _message.text = content.Message;
        //update layout group (necessary for size adjustment)
        LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroup);
    }

    public void ReplyToComment()
    {
        //poiMenuController.StartCommmentReply(_id, _message);
    }
}
