using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommentInputSection : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _commentToReplyTo;

    [SerializeField]
    private TMP_InputField _textInputField;


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
        TextMeshProUGUI placeholder = (TextMeshProUGUI)_textInputField.placeholder;

        if (replyState == POIMenuState.replyInput)
        {
            placeholder.text = "Leave a reply...";
        }
        else
        {
            placeholder.text = "Leave a comment...";
        }
    }

    public string GetTextInputContent()
    {
        return _textInputField.text;
    }

    public void ClearTextInputContent()
    {
        _textInputField.text = "";
    }

  
}
