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

    [SerializeField]
    private MainGUIController _guiController;


    private POIMenuTransitionController _transitionController;

    //Delete?
    private POIMenuContentController _contentController;

    [SerializeField]
    private POIMenuPanel _menuPanel;

    [SerializeField]
    private CommentInputSection _inputSection;

    public POIMenuState State { get; private set; }

    private POIMenuState _stateBeforeComment;

    private Comment _commentToReplyTo;

    private int _clickCounter;


    private void Awake()
    {
        _transitionController = gameObject.GetComponent<POIMenuTransitionController>();
        _transitionController.Init(_menuPanel, _inputSection);
        _contentController = gameObject.GetComponent<POIMenuContentController>();
        State = POIMenuState.closed;
       // State = POIMenuState.medium; //FOR TESTING
       // _contentController.Init(this);
    }


    public void OpenMenu(PointOfInterest content)
    {
        _clickCounter++;
        //_contentController.Setup(content);
        _menuPanel.Setup(content);

        if(State == POIMenuState.closed) {
            _transitionController.TransitionFromTo(State, POIMenuState.medium);
            SwitchState(POIMenuState.medium);
            
        }
    }

    public void CloseMenu()
    {
        _POISelectionManager.DeselectCurrentPOI();

        _transitionController.TransitionFromTo(State, POIMenuState.closed);
        SwitchState(POIMenuState.closed);


    }

    public void ExpandMenu()
    {
        if(State == POIMenuState.medium)
        {
            _transitionController.TransitionFromTo(State, POIMenuState.big);
            SwitchState(POIMenuState.big);
        } else if(State == POIMenuState.small)
        {
            _transitionController.TransitionFromTo(State, POIMenuState.medium);
            SwitchState(POIMenuState.medium);
        }
    }

    public void ContractMenu()
    {
        if (State == POIMenuState.medium)
        {
            _transitionController.TransitionFromTo(State, POIMenuState.small);
            SwitchState(POIMenuState.small);
        }
        else if (State == POIMenuState.big)
        {
            _transitionController.TransitionFromTo(State, POIMenuState.medium);
            SwitchState(POIMenuState.medium);
        }
    }

    public void StartCommentInput()
    {
        if(State == POIMenuState.replyInput)
        {
            return;
        }

        _transitionController.TransitionFromTo(State, POIMenuState.commentInput);
        _stateBeforeComment = State;
        SwitchState(POIMenuState.commentInput);

        //_transitionController.State = POIMenuState.commentInput;
    }

    public void StartReplyInput(Comment commentToReplyTo)
    {
        // _contentController.SetCommentToReplyTo(commentToReplyTo.Message);
        _inputSection.SetCommentToReplyTo(commentToReplyTo.Message);
        _transitionController.TransitionFromTo(State, POIMenuState.replyInput);
        _stateBeforeComment = State;
        SwitchState(POIMenuState.replyInput);

        _commentToReplyTo = commentToReplyTo;
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

        //string message = _contentController.GetTextInputContent();
        string message = _inputSection.GetTextInputContent();

        if (message == null || message == "")
        {
            Debug.LogError("The message is invalid");
            //TO DO: add display for user?
            return;
        }

        if (State == POIMenuState.commentInput)
        {
            _commentManager.AddNewComment(SessionManager.Instance.ActivePOI, poster, message);
        }
        else if (State == POIMenuState.replyInput)
        {
            _commentManager.AddReply(poster, message, _commentToReplyTo);
        }
        _transitionController.TransitionFromTo(State, _stateBeforeComment);
        SwitchState(_stateBeforeComment);
    }

    public void CancelComment()
    {
        _transitionController.TransitionFromTo(State, _stateBeforeComment);
        SwitchState(_stateBeforeComment);
    }

    private void SwitchState(POIMenuState newState)
    {
        State = newState;
        _guiController.SetPOIMenuHeight(GetMenuHeight());
    }

    private float GetMenuHeight()
    {
        if(State == POIMenuState.closed)
        {
            return 0f;
        } else
        {
            return _transitionController.GetYPos(State);
        }
    }

    void OnGUI()
 {


         GUI.Label(new Rect(200, 500, 400, 100), " Menu open click counter = " + _clickCounter);


 }

}
