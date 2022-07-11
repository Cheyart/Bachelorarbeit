using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class POIMenuTransitionController : MonoBehaviour
{
    private POIMenuContentController _contentController;

    [SerializeField]
    private float _transitionDuration = 1f;

    [SerializeField]
    private RectTransform _mainPanel;

    [SerializeField]
    private GameObject _mainPanelScrollBar;

    [SerializeField]
    private TMP_InputField _textInputField;

    private RectTransform _inputFieldRectTransform;

    [SerializeField]
    private GameObject _textInputButtons;

    [SerializeField]
    private GameObject _textInputScrollBar;

    [SerializeField]
    private GameObject _handleBar;

    [SerializeField]
    private GameObject _commentsContainer;

    [SerializeField]
    private GameObject _commentsHeader;

    private Vector3 _mainPanelPosition;

    private const float CLOSED_Y_POS = 140f;
    private const float SMALL_Y_POS = 370f;
    private const float MEDIUM_Y_POS = 1330f;
    private const float BIG_Y_POS = 1960f;

    private const float TEXT_INPUT_SMALL = 90f;
    private const float TEXT_INPUT_BIG = 300f;






    // Start is called before the first frame update
    void Awake()
    {
        //_state = POIMenuState.closed;
        _contentController = gameObject.GetComponent<POIMenuContentController>();
        _inputFieldRectTransform = _textInputField.GetComponent<RectTransform>();

        ResetToDefault();

        _mainPanelPosition = _mainPanel.anchoredPosition;
        _mainPanel.anchoredPosition = new Vector3(_mainPanelPosition.x, CLOSED_Y_POS, _mainPanelPosition.z);
    }


    public void TransitionFromTo(POIMenuState oldState, POIMenuState newState)
    {

        float yPos = GetYPos(newState);
        bool refreshAfterTransition = false;
        if(oldState == POIMenuState.closed)
        {
            ResetToDefault();

        }

        //if transitioning from main state to comment/reply view state
        if (oldState != POIMenuState.commentInput && oldState != POIMenuState.replyInput)
        {
            if(newState == POIMenuState.commentInput)
            {
                TransitionToCommentInput();
            } else if (newState == POIMenuState.replyInput)
            {

            }
        }
        //if transitioning from comment/reply state back to main state
        else
        {
            refreshAfterTransition = true;
            TransitionFromCommentInput(newState);
        }

        StartCoroutine(LerpMainPanelPosition(new Vector3(_mainPanelPosition.x, yPos, _mainPanelPosition.z), refreshAfterTransition));


    }

    private float GetYPos(POIMenuState newState)
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

    private void TransitionToCommentInput()
    {
        DisplayMainPanelElements(false);
        StartCoroutine(LerpTextInputHeight(TEXT_INPUT_BIG, POIMenuState.commentInput));
    }

    private void TransitionFromCommentInput(POIMenuState newState)
    {
        DisplayCommentInputElements(false);
        _contentController.ClearTextInputContent();

        StartCoroutine(LerpTextInputHeight(TEXT_INPUT_SMALL, newState));
        //_contentController.Refresh();


    }

    private void ResetToDefault()
    {
        _inputFieldRectTransform.sizeDelta = new Vector2(_inputFieldRectTransform.sizeDelta.x, TEXT_INPUT_SMALL);
        //_textInputField.text = "";

        DisplayCommentInputElements(false);
        DisplayMainPanelElements(true);
    }

   private IEnumerator LerpMainPanelPosition(Vector3 targetPosition, bool refreshAfterTransition)
   {
        Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
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
            _contentController.Refresh();
        }

    }

    private IEnumerator LerpTextInputHeight(float targetHeight, POIMenuState newState)
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

        if(newState == POIMenuState.commentInput)
        {
            DisplayCommentInputElements(true);
        } else
        {
            DisplayMainPanelElements(true);
        }
    }

    private void DisplayCommentInputElements(bool value)
    {
        _textInputButtons.SetActive(value);
        _textInputScrollBar.SetActive(value);
    }

    private void DisplayMainPanelElements(bool value)
    {
        _commentsContainer.SetActive(value);
        _commentsHeader.SetActive(value);
        _mainPanelScrollBar.SetActive(value);
    }


}
