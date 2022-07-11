using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum POIMenuState
{
    closed, small, medium, big, commentInput, replyInput
}


[RequireComponent(typeof (POIMenuContentController), typeof(POIMenuTransitionController))]
public class POIMenuManager : MonoBehaviour
{
    [SerializeField]
    private POISelectionManager _POISelectionManager;

    [SerializeField]
    private CommentManager _commentManager;


    private POIMenuTransitionController _transitionController;

    private POIMenuContentController _contentController;

    public POIMenuState State { get; private set; }

    private POIMenuState _stateBeforeComment;


    private void Awake()
    {
        _transitionController = gameObject.GetComponent<POIMenuTransitionController>();
        _contentController = gameObject.GetComponent<POIMenuContentController>();
        State = POIMenuState.closed;
    }


    public void OpenMenu(PointOfInterest content)
    {
       _contentController.Setup(content);

        if(State == POIMenuState.closed) {
            _transitionController.TransitionFromTo(State, POIMenuState.medium);
            State = POIMenuState.medium;
        }

    }

    public void CloseMenu()
    {
        _POISelectionManager.DeselectCurrentPOI();

        _transitionController.TransitionFromTo(State, POIMenuState.closed);
        State = POIMenuState.closed;
    }

    public void StartCommentInput()
    {
        _transitionController.TransitionFromTo(State, POIMenuState.commentInput);
        _stateBeforeComment = State;
        State = POIMenuState.commentInput;
        //_transitionController.State = POIMenuState.commentInput;
    }

    public void StartCommentReply(int id, string message)
    {
        //transitionManager.CommentToReplyTo = message;
        //transitionManager.State = POIMenuState.commentReply;


        //oder TransitionToCommentReply(message);
    }


    //ADD: Comment Input Handler
    public void SaveComment()
    {
        User poster = SessionManager.Instance.LoggedInUser;
        if (poster == null)
        {
            Debug.LogError("The user is not logged in");
            return;
        }

        string message = _contentController.GetTextInputConent();
        if (message == null || message == "")
        {
            Debug.LogError("The message is invalid");
            //TO DO: add display for user?
            return;
        }

        if (State == POIMenuState.commentInput)
        {
            _commentManager.AddNewComment(SessionManager.Instance.ActivePOI, poster, message);
            _transitionController.TransitionFromTo(State, _stateBeforeComment);
        }
        else if (State == POIMenuState.replyInput)
        {
            //SaveReply();
        }
    }

   
}
