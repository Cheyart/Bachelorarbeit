using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ScrollMask : MonoBehaviour
{
    //[SerializeField]
    //private MainGUIController _guiController;

    [SerializeField]
    private RectTransform _scrollBar;

    //float _bottomBarHeight; -> replace later

    private RectTransform _rectTransform;

    private float _bottomOffset;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _bottomOffset = SessionManager.Instance.GUIController.BottomBarHeight;
    }

    public void SetHeight(float mainPanelYPos)
    {
        float newHeight = mainPanelYPos - _bottomOffset + _rectTransform.anchoredPosition.y;
        float width = _rectTransform.sizeDelta.x;
        _rectTransform.sizeDelta = new Vector2(width, newHeight);
        _scrollBar.sizeDelta = new Vector2(newHeight, _scrollBar.sizeDelta.y);
    }
}
