using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class MainGUIController controls the activation/deactivation of the overall GUI
 */
public class MainGUIController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _topBar;

    [SerializeField]
    private GameObject _poiMenu;

    [SerializeField]
    private GameObject _bottomBar;

    public float TopBarHeight { get; private set; }

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
        _topBar.SetActive(true);
        _bottomBar.SetActive(true);
        _poiMenu.SetActive(true);
    }

}