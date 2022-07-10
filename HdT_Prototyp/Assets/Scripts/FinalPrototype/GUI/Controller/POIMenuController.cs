using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum POIMenuState
{
    closed, small, medium, big
}

[RequireComponent(typeof (POIMenuContentSetup), typeof(POIMenuTransitionManager))]
public class POIMenuController : MonoBehaviour
{
    [SerializeField]
    private POIMenuContentSetup _contentSetup;

    [SerializeField]
    private Transform _menuPanel;

    [SerializeField]
    private POISelectionManager _POISelectionManager;

    [SerializeField]
    private POIMenuTransitionManager _transitionManager;

    private POIMenuState _state;

    //add state (small, medium, big)
    //add animated transition


    // Start is called before the first frame update
    void Start()
    {
        _state = POIMenuState.closed;

        //_contentSetup = GetComponent<POIMenuContentSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(PointOfInterest content)
    {
       _contentSetup.Setup(content);
        // _menuPanel.gameObject.SetActive(true);  //TO DO -> make transition animation (appear from bottom)
        if(_state == POIMenuState.closed) {
            _transitionManager.TransitionMainPanelTo(POIMenuState.medium);
            _state = POIMenuState.medium;
        }

    }

    public void Hide()
    {
        _POISelectionManager.DeselectCurrentPOI();
        //_menuPanel.gameObject.SetActive(false); //TO DO -> make disappear animation (disappear to bottom)
        _transitionManager.TransitionMainPanelTo(POIMenuState.closed);
        _state = POIMenuState.closed;
        _contentSetup.Reset();

    }

}
