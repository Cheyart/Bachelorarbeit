using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POIMenuState
{
    closed, small, medium, big, commentInput, replyInput
}


[RequireComponent(typeof (POIMenuContentSetup), typeof(POIMenuTransitionController))]
public class POIMenuManager : MonoBehaviour
{
    [SerializeField]
    private POISelectionManager _POISelectionManager;

    private POIMenuTransitionController _transitionController;

    private POIMenuContentSetup _contentSetup;

    private POIMenuState _state;


    private void Awake()
    {
        _transitionController = gameObject.GetComponent<POIMenuTransitionController>();
        _contentSetup = gameObject.GetComponent<POIMenuContentSetup>();
        _state = POIMenuState.closed;
    }


    public void OpenMenu(PointOfInterest content)
    {
       _contentSetup.Setup(content);

        if(_state == POIMenuState.closed) {
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

    public void StartCommentInput()
    {
        Debug.Log("Start state = " + _state);
        _transitionController.TransitionFromTo(_state, POIMenuState.commentInput);
        _state = POIMenuState.commentInput;
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
        //if(_transitionController.State == POIMenuState.commentInput)
        //{
            Comment newComment = ScriptableObject.CreateInstance("Comment") as Comment;
           // newComment.Init(_logindata.LoggedInUser);
        //}
    }

   
}
