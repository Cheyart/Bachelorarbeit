using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommentInputSection : MonoBehaviour
{
    [SerializeField]
    private CommentInputField _commentInputField;

    [SerializeField]
    private GameObject _replyToSection;
    //set in start? -> child of ReplyDisplay
    [SerializeField]
    private TextMeshProUGUI _commentToReplyTo;

    [SerializeField]
    private GameObject _buttons;

    [SerializeField]
    private GameObject _scrollBar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCommentToReplyTo(string message)
    {
        _commentToReplyTo.text = message;
    }

    public void SetTextInputPlaceholderContent(POIMenuState replyState)
    {
        _commentInputField.SetPlaceholderContent(replyState);
    }

    public string GetTextInputContent()
    {
        return _commentInputField.GetContent();
    }

    public void ClearTextInputContent()
    {
        _commentInputField.ClearContent();
    }

    public void SetTextInputHeight(float height)
    {
        _commentInputField.SetHeight(height);
    }

    public IEnumerator AnimatedTextInputHeightChange(float targetHeight, float duration, EasingFunction easing)
    {
        yield return _commentInputField.AnimatedHeightChange(targetHeight, duration, easing);
    }

    public void ShowCommentInputElements(bool value)
    {
        _buttons.SetActive(value);
        _scrollBar.SetActive(value);
    }

    public void ShowReplyInputElements(bool value)
    {
        _replyToSection.SetActive(value);
    }


}
