using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _topBar;

    [SerializeField]
    private GameObject _poiMenu;

    [SerializeField]
    private GameObject _bottomBar;

    // Start is called before the first frame update
    void Start()
    {
        _topBar.SetActive(false);
        _poiMenu.SetActive(false);
        _bottomBar.SetActive(false);
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
}
