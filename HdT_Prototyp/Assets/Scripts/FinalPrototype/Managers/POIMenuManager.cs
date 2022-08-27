using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/** @enum POIMenuState defines the differents states that the POI menu can have
 */
public enum POIMenuState
{
    closed, small, medium, big, commentInput, replyInput
}

/** @class POIMenuManager coordinates the display and behaviour of the POI menu
 */
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
    public POIMenuState State { get => _state; }

    private POIMenuState _stateBeforeComment;
    private Comment _commentToReplyTo;

    private string _debugText = "";

    private bool _instructionWasShown;


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

        if (!_instructionWasShown)
        {
            SessionManager.Instance.InstructionController.ShowInstruction(Instructions.adjustMenuSize, 1f, true);
            _instructionWasShown = true;
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


}
