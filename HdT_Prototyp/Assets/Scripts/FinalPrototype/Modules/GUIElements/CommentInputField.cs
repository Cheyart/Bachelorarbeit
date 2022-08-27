using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/** @class CommentInputField This class controls the setup and behaviour of the text input field for the comment/reply input
 */
[RequireComponent(typeof(TMP_InputField), typeof(AnimatedRectTransform), typeof(RectTransform))]
public class CommentInputField : MonoBehaviour
{
    private TMP_InputField _inputField;
    private AnimatedRectTransform _animation;
    private RectTransform _rectTransform;

    void Start()
    {
        _inputField = GetComponent<TMP_InputField>();
        _animation = GetComponent<AnimatedRectTransform>();
        _rectTransform = GetComponent<RectTransform>();
    }


    public void SetPlaceholderContent(POIMenuState replyState)
    {
        TextMeshProUGUI placeholder = (TextMeshProUGUI)_inputField.placeholder;

        if (replyState == POIMenuState.replyInput)
        {
            placeholder.text = "Leave a reply...";
        }
        else
        {
            placeholder.text = "Leave a comment...";
        }
    }

    public string GetContent()
    {
        return _inputField.text;
    }

    public void ClearContent()
    {
        _inputField.text = "";
    }

    public void SetHeight(float height)
    {
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, height);
    }

    public IEnumerator AnimatedHeightChange(float targetHeight, float duration, EasingFunction easing)
    {
        Vector2 targetSize = new Vector2(_rectTransform.sizeDelta.x, targetHeight);
        yield return _animation.LerpSize(_rectTransform.sizeDelta, targetSize, duration, easing);
    }


}
