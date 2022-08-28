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
[RequireComponent(typeof(POIMenuTransitionController), typeof(POIMenuContentController))]
public class POIMenuManager : MonoBehaviour
{

    [SerializeField]
    private POIMenuPanel _menuPanel; /** The main panel of the POI menu*/

    [SerializeField]
    private CommentInputSection _inputSection; /** The comment input section of the POI menu*/

    private SessionManager _sessionManager; /** Session Manager */
    private POISelectionManager _POISelectionManager; /** POI Selection Manager */
    private CommentManager _commentManager; /** Comment Manager */
    private POIMenuTransitionController _transitionController; /** POI Menu Transition Controller */
    private POIMenuContentController _contentController; /** POI Menu Content Controller */


    private POIMenuState _state; /** the current state of the menu */
    public POIMenuState State { get => _state; } /** the current state of the menu */

    private POIMenuState _stateBeforeComment; /** the menu state before it was switched to comment input */
    private Comment _commentToReplyTo; /** the comment which is currently being replied to */

    private bool _instructionWasShown; /** Value indicating if the instruction for the POI Menu was already shown */


    private void Awake()
    {
        _sessionManager = SessionManager.Instance;
        _POISelectionManager = _sessionManager.POISelectionManager;
        _commentManager = _sessionManager.CommentManager;

        _transitionController = gameObject.GetComponent<POIMenuTransitionController>();
        _contentController = gameObject.GetComponent<POIMenuContentController>();
        _transitionController.Init(_menuPanel, _inputSection);
        _contentController.Init(_menuPanel, _inputSection);

        _state = POIMenuState.closed;

    }


    /** Setup the content and open (show) the menu 
     * @param content Content of the POI which will be displayed in the menu
     */
    public void OpenMenu(PointOfInterest content)
    {

        _contentController.Setup(content);

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

    /** Close (hide) the menu
     */
    public void CloseMenu()
    {
        _POISelectionManager.DeselectCurrentPOI();

        _transitionController.TransitionFromTo(_state, POIMenuState.closed);
        _state = POIMenuState.closed;
    }

    /** Expand the menu size
     */
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

    /** Contract the menu size
     */
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

    /** Switch to comment input state
     */
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

    /** Switch to reply input state
     * @param commentToReplyTo Comment for which a reply will be written
     */
    public void StartReplyInput(Comment commentToReplyTo)
    {
        _inputSection.SetCommentToReplyTo(commentToReplyTo.Message);
        _transitionController.TransitionFromTo(_state, POIMenuState.replyInput);
        _stateBeforeComment = _state;
        _state = POIMenuState.replyInput;

        _commentToReplyTo = commentToReplyTo;
    }

    /** Saves the current user input as a new comment and switches back to the default state
     */
    public void SaveComment()
    {
        User poster = _sessionManager.LoggedInUser;
        if (poster == null)
        {
            Debug.LogError("The user is not logged in");
            return;
        }

        string message = _contentController.GetCommentInput();

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

    /** Resets the current user input and switches back to the default state
     */
    public void CancelComment()
    {
        _transitionController.TransitionFromTo(_state, _stateBeforeComment);
        _state = _stateBeforeComment;
    }


}
