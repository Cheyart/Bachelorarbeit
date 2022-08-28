using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/** @class POIMenuTransitionController controls the transitions between the different states of the POI menu
 */
public class POIMenuTransitionController : MonoBehaviour
{

    [SerializeField]
    private MainViewport _mainViewport; /** The Viewport in the AR Mode, meaning the part of the screen that is not covered by the 2D-GUI (POI menu, top-bar, bottom-bar)*/

    [SerializeField]
    private RefreshLayoutGroup _layoutGroup; /** the layout group at the top of the panel hierarchy*/

    [SerializeField]
    private float _transitionDuration = 1f; /** Duration for the transitions */

    private POIMenuPanel _menuPanel; /** POI menu main panel*/
    private CommentInputSection _inputSection; /** comment input section of the POI menu */

    private float CLOSED_Y_POS = 140f; /** panel y-position when menu is closed */
    private const float SMALL_Y_POS = 370f;  /** panel y-position when menu is small*/
    private const float MEDIUM_Y_POS = 1100f;  /** panel y-position when menu is at medium size (default)*/
    private const float BIG_Y_POS = 2100f;  /** panel y-position when menu is big (full screen)*/

    private const float TEXT_INPUT_SMALL = 90f; /** Comment input field height at default state */
    private const float TEXT_INPUT_BIG = 300f; /** Comment input field height at comment input state */


    /** Initializes the references in this class and does initial setup 
     */
    public void Init(POIMenuPanel menuPanel, CommentInputSection inputSection)
    {

        _menuPanel = menuPanel;
        _inputSection = inputSection;
        _menuPanel.SetYPosition(CLOSED_Y_POS);
        CLOSED_Y_POS = SessionManager.Instance.GUIController.BottomBarHeight;
        _mainViewport.SetBottomOffset(CLOSED_Y_POS);
    
    }

    /** Executes a transition from one menu state to another
     * @param oldState menu state which the transition will start from
     * @param newState menu state which the transition will end in
     */
    public void TransitionFromTo(POIMenuState oldState, POIMenuState newState)
    {
        StartCoroutine(TransitionFromToCoroutine(oldState, newState));
    }

    /**Coroutine which executes a transition from one menu state to another
     * @param oldState menu state which the transition will start from
     * @param newState menu state which the transition will end in
     */
    private IEnumerator TransitionFromToCoroutine(POIMenuState oldState, POIMenuState newState)
    {
        float menuPanelYPos = GetMenuPanelYPos(newState);
        bool refreshAfterTransition = false;

        if (oldState == POIMenuState.closed)
        {
            ResetToDefault();
        }

        if (TransitioningFromMainStateToCommentInputState(oldState, newState))
        {

            StartCoroutine(TransitionToCommentInput(newState));

        }
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

    /** Returns the y-position for the menu panel for a specific state
     * @param state State for which the y-position will be returned
     * @return y-position for the menu panel
     */
    private float GetMenuPanelYPos(POIMenuState state)
    {
        if (state == POIMenuState.closed)
        {
            return CLOSED_Y_POS;
        }
        else if (state == POIMenuState.small)
        {
            return SMALL_Y_POS;
        }
        else if (state == POIMenuState.medium)
        {
            return MEDIUM_Y_POS;
        }
        else
        {
            return BIG_Y_POS;
        }
    }


    /** Executes the transition to the comment input state
     * @param replyState Comment state which will be transitioned to (can be new Comment or Reply)
     */
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


    /** Executes the transition from a comment input state to a default state
     * @param oldState State which the POI menu was in before entering the comment input state
     */
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

    /** Sets all the elements of the POI Menu back to their default state
     */
    private void ResetToDefault()
    {
        _inputSection.SetTextInputHeight(TEXT_INPUT_SMALL);
        _inputSection.ShowCommentInputElements(false);
        _inputSection.ShowReplyInputElements(false);
        _menuPanel.ShowContent(true);
    }

    /** Chechks if the transition will be from a default state (comment display) to a comment input state 
     * @param oldState The state at the start of the transition
     * œparam newState The state at the end of the transition
     * @return true of the transition will be from a default state to a comment input state
     */
    private bool TransitioningFromMainStateToCommentInputState(POIMenuState oldState, POIMenuState newState)
    {
        return (oldState != POIMenuState.commentInput && oldState != POIMenuState.replyInput && (newState == POIMenuState.commentInput || newState == POIMenuState.replyInput));
    }

}
