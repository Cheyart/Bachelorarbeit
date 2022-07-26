using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//manages transition between different states of the POI menu
public class POIMenuTransitionController : MonoBehaviour
{
 

    [SerializeField]
    private MainViewport _mainViewport;

    [SerializeField]
    private RefreshLayoutGroup _layoutGroup;

    [SerializeField]
    private float _transitionDuration = 1f;

    private POIMenuPanel _menuPanel;
    private CommentInputSection _inputSection;

    private const float CLOSED_Y_POS = 140f;
    private const float SMALL_Y_POS = 370f;
    private const float MEDIUM_Y_POS = 1100f;
    private const float BIG_Y_POS = 1970f;
    private float _bottomOffset;

    private const float TEXT_INPUT_SMALL = 90f;
    private const float TEXT_INPUT_BIG = 300f;



    public void Init(POIMenuPanel menuPanel, CommentInputSection inputSection)
    {
      
        _menuPanel = menuPanel;
        _inputSection = inputSection;
        _menuPanel.SetYPosition(CLOSED_Y_POS);
        _bottomOffset = SessionManager.Instance.GUIController.BottomBarHeight;
  
        _mainViewport.SetBottomOffset(_bottomOffset);
    }


    public void TransitionFromTo(POIMenuState oldState, POIMenuState newState)
    {
        StartCoroutine(TransitionFromToCoroutine(oldState, newState));
    }

   
    private IEnumerator TransitionFromToCoroutine(POIMenuState oldState, POIMenuState newState)
    {
        float menuPanelYPos = GetMenuPanelYPos(newState);
        bool refreshAfterTransition = false;

        if (oldState == POIMenuState.closed)
        {
            ResetToDefault();
        }

        if(TransitioningFromMainStateToCommentInputState(oldState, newState))
        { 
        //if transitioning from main state to comment/reply state
        //if (oldState != POIMenuState.commentInput && oldState != POIMenuState.replyInput)
        //{
           // if (newState == POIMenuState.commentInput || newState == POIMenuState.replyInput)
            //{
                StartCoroutine(TransitionToCommentInput(newState));
            //}

        }
        //if transitioning from comment/reply state back to main state
        else
        {
            refreshAfterTransition = true;
            StartCoroutine(TransitionFromCommentInput(oldState));
        }

        _mainViewport.AnimatedBottomOffsetTransition(menuPanelYPos, _transitionDuration, EasingFunction.easeInAndOut);
        yield return StartCoroutine(_menuPanel.AnimatedTranslateOnYAxis(menuPanelYPos, _transitionDuration, EasingFunction.easeInAndOut));

        if (refreshAfterTransition)
        {
            _menuPanel.RefreshContent();
        }
       
    }

    //make private?
    public float GetMenuPanelYPos(POIMenuState newState)
    {
        if (newState == POIMenuState.closed)
        {
            return CLOSED_Y_POS;
        }
        else if (newState == POIMenuState.small)
        {
            return SMALL_Y_POS;
        }
        else if (newState == POIMenuState.medium)
        {
            return MEDIUM_Y_POS;
        }
        else
        {
            return BIG_Y_POS;
        }
    }

  

    private IEnumerator TransitionToCommentInput(POIMenuState replyState)
    {

        _menuPanel.ShowContent(false);
        _inputSection.SetTextInputPlaceholderContent(replyState);

        yield return StartCoroutine(_inputSection.AnimatedTextInputHeightChange(TEXT_INPUT_BIG, _transitionDuration, EasingFunction.easeInAndOut));

        if (replyState == POIMenuState.replyInput)
        {
            _inputSection.ShowReplyInputElements(true);
            _layoutGroup.NeedsRefresh = true;
        }
        _inputSection.ShowCommentInputElements(true);

    }

    private IEnumerator TransitionFromCommentInput(POIMenuState oldState)
    {
        _inputSection.ShowCommentInputElements(false);
        _inputSection.ClearTextInputContent();

        if (oldState == POIMenuState.replyInput)
        {
            _inputSection.ShowReplyInputElements(false);
            _inputSection.SetTextInputPlaceholderContent(POIMenuState.commentInput);
        }

        yield return StartCoroutine(_inputSection.AnimatedTextInputHeightChange(TEXT_INPUT_SMALL, _transitionDuration, EasingFunction.easeInAndOut));
        _menuPanel.ShowContent(true);
    }


    private void ResetToDefault()
    {
        _inputSection.SetTextInputHeight(TEXT_INPUT_SMALL);
        _inputSection.ShowCommentInputElements(false);
        _inputSection.ShowReplyInputElements(false);
        _menuPanel.ShowContent(true);
    }

    private bool TransitioningFromMainStateToCommentInputState(POIMenuState oldState, POIMenuState newState)
    {
        return (oldState != POIMenuState.commentInput && oldState != POIMenuState.replyInput && (newState == POIMenuState.commentInput || newState == POIMenuState.replyInput));
     }



}
