using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class POIMenuTransitionController : MonoBehaviour
{
   // private POIMenuContentController _contentController;
    private POIMenuPanel _menuPanel;
    private CommentInputSection _inputSection;

    [SerializeField]
    //private LayoutGroup _contentContainerLayoutGroup;
    private RefreshLayoutGroup _layoutGroup;

    [SerializeField]
    private float _transitionDuration = 1f;

   // [SerializeField]
   // private RectTransform _mainPanel; 

   // [SerializeField]
    //private RectTransform _mainPanelScrollBar; 
    //[SerializeField]
    //private GameObject _mainPanelScrollBarGo; 

   // [SerializeField]
   // private TMP_InputField _textInputField; 

   // private RectTransform _inputFieldRectTransform; 

   // [SerializeField]
  //  private GameObject _replyDisplay; 

   // [SerializeField]
   // private GameObject _textInputButtons;

   // [SerializeField]
   // private GameObject _textInputScrollBar;

    //[SerializeField]
   // private GameObject _handleBar;

   // [SerializeField]
    //private GameObject _commentsContainer;

 //   [SerializeField]
   // private GameObject _commentsHeader;

    //[SerializeField]
    //private RectTransform _scrollMask;

    //[SerializeField]
    //private MainGUIController _guiController;

    //private Vector3 _mainPanelPosition;

    private const float CLOSED_Y_POS = 140f;
    private const float SMALL_Y_POS = 370f;
    private const float MEDIUM_Y_POS = 1100f;
    private const float BIG_Y_POS = 1970f;

    private const float TEXT_INPUT_SMALL = 90f;
    private const float TEXT_INPUT_BIG = 300f;

   
   // private int _counter = 0;


    // Start is called before the first frame update
    void Awake()
    {

        //_state = POIMenuState.closed;
       // _contentController = gameObject.GetComponent<POIMenuContentController>();
       /* _inputFieldRectTransform = _textInputField.GetComponent<RectTransform>();

        ResetToDefault();

       
        _menuPanel.SetYPosition(CLOSED_Y_POS);*/
       // _mainPanelPosition = _mainPanel.anchoredPosition;
       // _mainPanel.anchoredPosition = new Vector3(_mainPanelPosition.x, CLOSED_Y_POS, _mainPanelPosition.z);
        //_mainPanel.anchoredPosition = new Vector3(_mainPanelPosition.x, MEDIUM_Y_POS, _mainPanelPosition.z); //FOR TESTING

    }

    public void Init(POIMenuPanel menuPanel, CommentInputSection inputSection)
    {
        _menuPanel = menuPanel;
        _inputSection = inputSection;

       // _inputFieldRectTransform = _textInputField.GetComponent<RectTransform>();
        //ResetToDefault();
        _menuPanel.SetYPosition(CLOSED_Y_POS);
    }


    public void TransitionFromTo(POIMenuState oldState, POIMenuState newState)
    {
       // _counter++;
        //Debug.Log("TRANSITION from " + oldState.ToString() + " to " + newState.ToString());

        StartCoroutine(TransitionFromToCoroutine(oldState, newState));

    }

    //TO DO
    //break up function (?)
    //don't add after effect inside coroutine
    private IEnumerator TransitionFromToCoroutine(POIMenuState oldState, POIMenuState newState)
    {
        float menuPanelYPos = GetMenuPanelYPos(newState);
        bool refreshAfterTransition = false;

        if (oldState == POIMenuState.closed) //if new state == closed
        {
            ResetToDefault();
        }

        //if transitioning from main state to comment/reply state
        if (oldState != POIMenuState.commentInput && oldState != POIMenuState.replyInput)
        {
            if (newState == POIMenuState.commentInput || newState == POIMenuState.replyInput)
            {
                StartCoroutine(TransitionToCommentInput(newState));
            }

        }
        //if transitioning from comment/reply state back to main state
        else
        {
            refreshAfterTransition = true;
            StartCoroutine(TransitionFromCommentInput(oldState));
        }

        //yield return StartCoroutine(LerpMainPanelPosition(new Vector3(_mainPanelPosition.x, menuPanelYPos, _mainPanelPosition.z), refreshAfterTransition));
        yield return StartCoroutine(_menuPanel.AnimatedTranslateOnYAxis(menuPanelYPos, _transitionDuration, EasingFunction.easeInAndOut));

        if (refreshAfterTransition)
        {
            _menuPanel.RefreshContent();
        }
        //if(newState == POIMenuState.big || newState == POIMenuState.medium)
        //{
            //SetScrollMaskHeight();
        //}
       
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

   /* private void SetScrollMaskHeight()
    {
        float newHeight = _mainPanel.anchoredPosition.y - _guiController.BottomBarHeight + _scrollMask.anchoredPosition.y;
        float width = _scrollMask.sizeDelta.x;
         _scrollMask.sizeDelta = new Vector2 (width, newHeight);
        _mainPanelScrollBar.sizeDelta = new Vector2(newHeight, _mainPanelScrollBar.sizeDelta.y);
    }*/

    private IEnumerator TransitionToCommentInput(POIMenuState replyState)
    {

        _menuPanel.ShowContent(false);
        // _contentController.SetTextInputPlaceholderContent(replyState);
        _inputSection.SetTextInputPlaceholderContent(replyState);

        //StartCoroutine(LerpTextInputHeight(TEXT_INPUT_BIG, replyState));
        yield return StartCoroutine(_inputSection.AnimatedTextInputHeightChange(TEXT_INPUT_BIG, _transitionDuration, EasingFunction.easeInAndOut));

        if (replyState == POIMenuState.replyInput)
        {
            _inputSection.ShowReplyInputElements(true);
            // StartCoroutine(UpdateLayoutGroup());
            _layoutGroup.NeedsRefresh = true;
        }
        _inputSection.ShowCommentInputElements(true);

    }

    /*private void TransitionToReplyInput()
    {
        DisplayMainPanelElements(false);
        _contentController.SetTextInputPlaceholderContent(POIMenuState.replyInput);
        StartCoroutine(LerpTextInputHeight(TEXT_INPUT_BIG, POIMenuState.replyInput));
    }*/




    private IEnumerator TransitionFromCommentInput(POIMenuState oldState)
    {
        // DisplayCommentInputElements(false);
        _inputSection.ShowCommentInputElements(false);
        _inputSection.ClearTextInputContent();

        if (oldState == POIMenuState.replyInput)
        {
            //DisplayReplyInputElements(false);
            _inputSection.ShowReplyInputElements(false);
            _inputSection.SetTextInputPlaceholderContent(POIMenuState.commentInput);
        }


        // StartCoroutine(LerpTextInputHeight(TEXT_INPUT_SMALL, newState));
        yield return StartCoroutine(_inputSection.AnimatedTextInputHeightChange(TEXT_INPUT_SMALL, _transitionDuration, EasingFunction.easeInAndOut));

        _menuPanel.ShowContent(true);

        //_contentController.Refresh();


    }


    private void ResetToDefault()
    {
        //_inputFieldRectTransform.sizeDelta = new Vector2(_inputFieldRectTransform.sizeDelta.x, TEXT_INPUT_SMALL);
        //_textInputField.text = "";
        _inputSection.SetTextInputHeight(TEXT_INPUT_SMALL);

        // DisplayCommentInputElements(false);
        //DisplayReplyInputElements(false);
        _inputSection.ShowCommentInputElements(false);
        _inputSection.ShowReplyInputElements(false);
        _menuPanel.ShowContent(true);
    }

  /* private IEnumerator LerpMainPanelPosition(Vector3 targetPosition, bool refreshAfterTransition)
   {
       //Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
        float time = 0;
        Vector3 startPosition = _mainPanel.anchoredPosition;
        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _mainPanel.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        _mainPanel.anchoredPosition = targetPosition;
     
        if (refreshAfterTransition)
        {
            //_contentController.Refresh();
            _menuPanel.RefreshContent();
        }
    }*/

   /* private IEnumerator LerpTextInputHeight(float targetHeight, POIMenuState newState)
    {

        float time = 0;
        Vector2 startSize = _inputFieldRectTransform.sizeDelta;
        Vector2 targetSize = new Vector2(startSize.x, targetHeight);
        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _inputFieldRectTransform.sizeDelta = Vector2.Lerp(startSize, targetSize, t);


            time += Time.deltaTime;
            yield return null;
        }
        _inputFieldRectTransform.sizeDelta = targetSize;

        if(newState == POIMenuState.commentInput || newState == POIMenuState.replyInput)
        {
            if(newState == POIMenuState.replyInput)
            {
                DisplayReplyInputElements(true);
                StartCoroutine(UpdateLayoutGroup());
            }
            DisplayCommentInputElements(true);
        } else
        {
            _menuPanel.ShowContent(true);
        }
    }*/

   /* private void DisplayCommentInputElements(bool value)
    {
        _textInputButtons.SetActive(value);
        _textInputScrollBar.SetActive(value);
    }

    private void DisplayReplyInputElements(bool value)
    {
        _replyDisplay.SetActive(value);
    }*/

    /*private void DisplayMainPanelElements(bool value)
    {
        _commentsContainer.SetActive(value);
        _commentsHeader.SetActive(value);
       // _mainPanelScrollBar.gameObject.SetActive(value);

        //TEST
        _mainPanelScrollBarGo.SetActive(value);

    }*/

    /*IEnumerator UpdateLayoutGroup()
    {
        _contentContainerLayoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        _contentContainerLayoutGroup.enabled = true;
    }*/

    void OnGUI()
    {


       // GUI.Label(new Rect(200, 450, 400, 100), " transition  counter = " + _counter);


    }
}
