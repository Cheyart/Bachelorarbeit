using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Preparation
{
    public class OverlayGUI : MonoBehaviour
{

    private ViewTransitionController _transitionController;

    [SerializeField]
    private GameObject _arGUI;

    [SerializeField]
    private GameObject _vrGUI;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SwitchToVrGUI()
    {
        _arGUI.gameObject.SetActive(false);
        _vrGUI.gameObject.SetActive(true);
    }

    public void SwitchToArGUI()
    {
        _arGUI.gameObject.SetActive(true);
        _vrGUI.gameObject.SetActive(false);
    }

    public void HideBothViews()
    {
        _arGUI.gameObject.SetActive(false);
        _vrGUI.gameObject.SetActive(false);
    }

   

}
}