using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum POIMenuState
{
    closed, small, medium, big, commentInput, replyInput
}

public class POIMenuTransitionManager : MonoBehaviour
{
    [SerializeField]
    private float _transitionDuration = 1f;

    [SerializeField]
    private RectTransform _mainPanel;

    [SerializeField]
    private RectTransform _textInputField;

    [SerializeField]
    private Transform _commentsContainer;

    [SerializeField]
    private Transform _commentsHeader;


    private POIMenuState _state;
    public POIMenuState State
    {
        get { return _state; }
        set
        {
            if(_state == value)
            {
                return;
            }

            _state = value;

            float yPos = 0;
            switch (_state)
            {
                case POIMenuState.closed:
                    yPos = CLOSED_Y_POS;
                    break;
                case POIMenuState.small:
                    yPos = SMALL_Y_POS;
                    break;
                case POIMenuState.medium:
                    yPos = MEDIUM_Y_POS;
                    break;
                case POIMenuState.commentInput:
                    yPos = BIG_Y_POS;
                    TransitionToCommentInput();
                    break;
                default:
                    break;

            }

            StartCoroutine(LerpMainPanelPosition(new Vector3(_mainPanelPosition.x, yPos, _mainPanelPosition.z)));
        }
    }

    private Vector3 _mainPanelPosition;

    private const float CLOSED_Y_POS = 140f;
    private const float SMALL_Y_POS = 370f;
    private const float MEDIUM_Y_POS = 1330f;
    private const float BIG_Y_POS = 2025f;

    private const float TEXT_INPUT_SMALL = 90f;
    private const float TEXT_INPUT_BIG = 300f;






    // Start is called before the first frame update
    void Start()
    {
        _state = POIMenuState.closed;
        _mainPanelPosition = _mainPanel.anchoredPosition;
        _mainPanel.anchoredPosition = new Vector3(_mainPanelPosition.x, CLOSED_Y_POS, _mainPanelPosition.z);
        Debug.Log("Panel position = " + _mainPanel.position);
        Debug.Log("Panel anchored position = " + _mainPanel.anchoredPosition);

        Debug.Log("Panel local position = " + _mainPanel.localPosition);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void TransitionMainPanelTo(POIMenuState newState)
    {
        float yPos = 0;
        switch (newState)
        {
            case POIMenuState.closed: yPos = CLOSED_Y_POS;
                break;
            case POIMenuState.small:
                yPos = SMALL_Y_POS;
                break;
            case POIMenuState.medium:
                yPos = MEDIUM_Y_POS;
                break;
            default:
                break;

        }

        StartCoroutine(LerpMainPanelPosition(new Vector3(_mainPanelPosition.x, yPos, _mainPanelPosition.z)));
    }*/

    public void TransitionToCommentInput()
    {
        _commentsContainer.gameObject.SetActive(false);
        _commentsHeader.gameObject.SetActive(false);
        StartCoroutine(LerpTextInputHeight(TEXT_INPUT_BIG));
    }

   private IEnumerator LerpMainPanelPosition(Vector3 targetPosition)
   {
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
    }

    private IEnumerator LerpTextInputHeight(float targetHeight)
    {
        float time = 0;
        Vector2 startSize = _textInputField.sizeDelta;
        Vector2 targetSize = new Vector2(startSize.x, targetHeight);
        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _textInputField.sizeDelta = Vector2.Lerp(startSize, targetSize, t);


            time += Time.deltaTime;
            yield return null;
        }
        _textInputField.sizeDelta = targetSize;
    }


}
