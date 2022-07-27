using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGUIController : MonoBehaviour
{
    //[SerializeField]
    //private POIMenuManager _poiMenuManager;

    //[SerializeField]
    //private OffScreenPointer _offScreenPointer;

    [SerializeField]
    private GameObject _topBar;

    [SerializeField]
    private GameObject _poiMenu;

    [SerializeField]
    private GameObject _bottomBar;

    //add Viewport; ?

    //private float _screenWidth;
   // private float _screenHeight;
    private float _topBarHeight;
    public float TopBarHeight { get => _topBarHeight; }

    private float _bottomBarHeight;
    public float BottomBarHeight { get => _bottomBarHeight; }


   // private float _poiMenuHeight;
    //private Vector2 _viewportHeightCoordinates;
    //private Vector2 _viewportWidthCoordinates;

    private void Awake()
    {
        Debug.Log("AWAKE MAIN GUI Controller");
        _topBar.SetActive(false);
        _poiMenu.SetActive(false);
        _bottomBar.SetActive(false);

        //DELETE
        //_poiMenuHeight = 0;
       // SetViewportDimensions(_poiMenuHeight);

        _topBarHeight = _topBar.GetComponent<RectTransform>().rect.height;
        _bottomBarHeight = _bottomBar.GetComponent<RectTransform>().rect.height;
        Debug.Log("Bottom Bar height = " + _bottomBarHeight);

    }

    // Start is called before the first frame update
    void Start()
    {
    
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

   /* public void SetPOIMenuHeight(float poiMenuHeight)
    {
        _poiMenuHeight = poiMenuHeight;
        SetViewportDimensions(poiMenuHeight);
    }*/

    /*public void SetViewportDimensions(float poiMenuHeight)
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
    }*/


}
