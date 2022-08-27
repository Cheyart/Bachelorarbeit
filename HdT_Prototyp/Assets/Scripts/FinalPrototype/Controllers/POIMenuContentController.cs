using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class POIMenuContentController This class controls the displayed content of the POI menu
 */
public class POIMenuContentController : MonoBehaviour
{

    private POIMenuPanel _menuPanel;
    private CommentInputSection _inputSection;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(POIMenuPanel menuPanel, CommentInputSection inputSection)
    {
        _menuPanel = menuPanel;
        _inputSection = inputSection;
    }

    public void Setup(PointOfInterest poiContent)
    {
        _menuPanel.SetupContent(poiContent);
    }

    public string GetCommentInput()
    {
        return _inputSection.GetTextInputContent();

    }

}