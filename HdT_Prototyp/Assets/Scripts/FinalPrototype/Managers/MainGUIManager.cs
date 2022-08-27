using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains information about the main GUI elements and manages their behavior
public class MainGUIManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _topBar;

    [SerializeField]
    private GameObject _poiMenu;

    [SerializeField]
    private GameObject _bottomBar;

   // private float _topBarHeight;
    public float TopBarHeight { get; private set; }

    //private float _bottomBarHeight;
    public float BottomBarHeight { get; private set; }


    private void Start()
    {
        _topBar.SetActive(false);
        _poiMenu.SetActive(false);
        _bottomBar.SetActive(false);

        TopBarHeight = _topBar.GetComponent<RectTransform>().rect.height;
        BottomBarHeight = _bottomBar.GetComponent<RectTransform>().rect.height;
    }

    public void ShowMainGUI()
    {
        Debug.Log("Show Main GUI");
        _topBar.SetActive(true);
        _bottomBar.SetActive(true);
        _poiMenu.SetActive(true);
    }

}
