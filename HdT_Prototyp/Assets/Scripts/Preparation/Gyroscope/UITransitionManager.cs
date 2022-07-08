using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Preparation
{
    public enum UIMode
{
    twoD, threeD
}

public class UITransitionManager : MonoBehaviour
{
    [SerializeField]
    private Image _2Dpanel;

    public UIMode CurrentMode { get => _currentMode; set => _currentMode = value; }
    private UIMode _currentMode;

    // Start is called before the first frame update
    void Start()
    {
        _currentMode = UIMode.twoD;

        //set visibility of 2D panel according to smartphone orientation
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show2DUI()
    {

        _2Dpanel.gameObject.SetActive(true);
        _currentMode = UIMode.twoD;
    }

    public void Hide2DUI()
    {
        _2Dpanel.gameObject.SetActive(false);
        _currentMode = UIMode.threeD;
    }
}
}