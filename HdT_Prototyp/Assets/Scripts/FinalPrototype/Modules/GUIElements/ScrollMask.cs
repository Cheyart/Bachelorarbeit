using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class ScrollMask This class is responsible for the adjustment of the scroll mask in the POI menu
 */
[RequireComponent(typeof(RectTransform))]
public class ScrollMask : MonoBehaviour
{

    [SerializeField]
    private RectTransform _scrollBar; /** Scrollbar related to the scroll mask*/

    private RectTransform _rectTransform; /** RectTransform Component*/

    private float _bottomOffset; /** bottom offset*/

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _bottomOffset = SessionManager.Instance.GUIController.BottomBarHeight;
    }

    /** Set Height of the scrollmask according to the POI menu panel position
     * @param mainPanelYPos POI menu panel y position
     */
    public void SetHeight(float mainPanelYPos)
    {
        float newHeight = mainPanelYPos - _bottomOffset + _rectTransform.anchoredPosition.y;
        float width = _rectTransform.sizeDelta.x;
        _rectTransform.sizeDelta = new Vector2(width, newHeight);
        _scrollBar.sizeDelta = new Vector2(newHeight, _scrollBar.sizeDelta.y);
    }
}
