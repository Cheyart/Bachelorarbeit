using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class POIMenuContentController This class controls the displayed content of the POI menu
 */
public class POIMenuContentController : MonoBehaviour
{

    private POIMenuPanel _menuPanel; /** main panel of the POI menu*/
    private CommentInputSection _inputSection; /** comment input section of the POI menu */


    /** Iniatlizes the references of this class
     */
    public void Init(POIMenuPanel menuPanel, CommentInputSection inputSection)
    {
        _menuPanel = menuPanel;
        _inputSection = inputSection;
    }

    /** Sets up the content of the POI menu
     * @param poiContent Content of the POI which will be shown in the POI menu
     */
    public void Setup(PointOfInterest poiContent)
    {
        _menuPanel.SetupContent(poiContent);
    }

    /** Returns the current user input in the comment input field
     * @return user input
     */
    public string GetCommentInput()
    {
        return _inputSection.GetTextInputContent();

    }

}