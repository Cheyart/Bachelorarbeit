using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum POIMenuState
{
    closed, small, medium, big, commentInput, replyInput
}

//manages the POI menu behaviour. sets upp the content and communicates to the Transition Controller when a transition from one state to another is required.
[RequireComponent(typeof(POIMenuTransitionController))]
public class POIMenuManager : MonoBehaviour
{

    [SerializeField]
    private POIMenuPanel _menuPanel;

    [SerializeField]
    private CommentInputSection _inputSection;

    private SessionManager _sessionManager;
    private POISelectionManager _POISelectionManager;
    private CommentManager _commentManager;
    private POIMenuTransitionController _transitionController;

    private POIMenuState _state;
    private POIMenuState _stateBeforeComment;
    private Comment _commentToReplyTo;

    private string _debugText = "";


    private void Awake()
    {
        _sessionManager = SessionManager.Instance;
        _POISelectionManager = _sessionManager.POISelectionManager;
        _commentManager = _sessionManager.CommentManager;

        _transitionController = gameObject.GetComponent<POIMenuTransitionController>();
        _transitionController.Init(_menuPanel, _inputSection);
        _state = POIMenuState.closed;

    }


    public void OpenMenu(PointOfInterest content)
    {
        _menuPanel.SetupContent(content);

        if (_state == POIMenuState.closed)
        {
            _transitionController.TransitionFromTo(_state, POIMenuState.medium);
            _state = POIMenuState.medium;
        }
    }

    public void CloseMenu()
    {
        _POISelectionManager.DeselectCurrentPOI();

        _transitionController.TransitionFromTo(_state, POIMenuState.closed);
        _state = POIMenuState.closed;
    }

    public void ExpandMenu()
    {
        if (_state == POIMenuState.medium)
        {
            _transitionController.TransitionFromTo(_state, POIMenuState.big);
            _state = POIMenuState.big;
        }
        else if (_state == POIMenuState.small)
        {
            _transitionController.TransitionFromTo(_state, POIMenuState.medium);
            _state = POIMenuState.medium;
        }
    }

    public void ContractMenu()
    {
        if (_state == POIMenuState.medium)
        {
            _transitionController.TransitionFromTo(_state, POIMenuState.small);
            _state = POIMenuState.small;
        }
        else if (_state == POIMenuState.big)
        {
            _transitionController.TransitionFromTo(_state, POIMenuState.medium);
            _state = POIMenuState.medium;
        }
    }

    public void StartCommentInput()
    {
        if (_state == POIMenuState.replyInput)
        {
            return;
        }

        _transitionController.TransitionFromTo(_state, POIMenuState.commentInput);
        _stateBeforeComment = _state;
        _state = POIMenuState.commentInput;
    }

    public void StartReplyInput(Comment commentToReplyTo)
    {
        _inputSection.SetCommentToReplyTo(commentToReplyTo.Message);
        _transitionController.TransitionFromTo(_state, POIMenuState.replyInput);
        _stateBeforeComment = _state;
        _state = POIMenuState.replyInput;

        _commentToReplyTo = commentToReplyTo;
    }


    public void SaveComment()
    {
        _debugText += "Inside save comment,  \n";
        User poster = _sessionManager.LoggedInUser;
        if (poster == null)
        {
            Debug.LogError("The user is not logged in");
            return;
        }

        string message = _inputSection.GetTextInputContent();

        if (message == null || message == "")
        {
            Debug.LogError("The message is invalid");
            return;
        }

        if (_state == POIMenuState.commentInput)
        {
            _commentManager.AddNewComment(_sessionManager.ActivePOI, poster, message);
        }
        else if (_state == POIMenuState.replyInput)
        {
            _debugText += "Inside add reply to comment Manager \n";
            _commentManager.AddReply(poster, message, _commentToReplyTo);
        }
        _transitionController.TransitionFromTo(_state, _stateBeforeComment);
        _state = _stateBeforeComment;
    }

    public void CancelComment()
    {
        _transitionController.TransitionFromTo(_state, _stateBeforeComment);
        _state = _stateBeforeComment;
    }

   /* private void OnGUI()
    {
        GUI.Label(new Rect(200, 300, 100, 100), _debugText );
        GUI.Label(new Rect(200, 350, 100, 100), "commentManager = " + _commentManager );

    }*/

}
