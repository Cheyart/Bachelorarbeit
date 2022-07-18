using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGUIController : MonoBehaviour
{
    //[SerializeField]
    //private POIMenuManager _poiMenuManager;

    [SerializeField]
    private OffScreenPointer _offScreenPointer;

    [SerializeField]
    private GameObject _topBar;

    [SerializeField]
    private GameObject _poiMenu;

    [SerializeField]
    private GameObject _bottomBar;

    //private float _screenWidth;
   // private float _screenHeight;
    private float _topBarHeight;
    private float _bottomBarHeight;

    private float _poiMenuHeight;
    private Vector2 _viewportHeightCoordinates;
    private Vector2 _viewportWidthCoordinates;



    // Start is called before the first frame update
    void Start()
    {
        _topBar.SetActive(false);
        _poiMenu.SetActive(false);
        _bottomBar.SetActive(false);
        _poiMenuHeight = 0;
        SetViewportDimensions(_poiMenuHeight);


        //_screenWidth = Screen.width;
        //_screenHeight = Screen.height;

        _topBarHeight = _topBar.GetComponent<RectTransform>().rect.height;
        _bottomBarHeight = _bottomBar.GetComponent<RectTransform>().rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainGUI()
    {
        _topBar.SetActive(true);
        _bottomBar.SetActive(true);
        _poiMenu.SetActive(true);
    }

    /*public Vector2 GetViewportHeight()
    {
        

        float bottom = _poiMenuManager.GetMenuHeight();
        if (bottom == 0)
        {
            bottom = _bottomBarHeight;
        }

        float top = _screenHeight - _topBarHeight;

        return new Vector2(bottom, top);
    }*/

    /*public void SetViewportWidthCoordinates()
    {
        _viewportHeightCoordinates = new Vector2()
        return new Vector2(0, _screenWidth);
    }*/

    public void SetPOIMenuHeight(float poiMenuHeight)
    {
        _poiMenuHeight = poiMenuHeight;
        SetViewportDimensions(poiMenuHeight);
    }

    public void SetViewportDimensions(float poiMenuHeight)
    {
        _viewportWidthCoordinates = new Vector2(0, Screen.width);

        float bottom = poiMenuHeight;
        if (bottom == 0)
        {
            bottom = _bottomBarHeight;
        }

        float top = Screen.height - _topBarHeight;
        _viewportHeightCoordinates = new Vector2(bottom, top);

        _offScreenPointer.SetViewportDimensions(_viewportWidthCoordinates, _viewportHeightCoordinates);
    }


}
