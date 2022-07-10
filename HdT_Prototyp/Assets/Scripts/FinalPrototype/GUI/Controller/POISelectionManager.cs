using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISelectionManager : MonoBehaviour
{
    [SerializeField]
    private POIMenuController _POIMenuController;

    private POIHandler [] _POIs;

    [SerializeField]
    private Color _idleColor;

    [SerializeField]
    private Color _selectedColor;

    private int _currentlySelectedPOI;


    // Start is called before the first frame update
    void Awake()
    {
        _POIs = GetComponentsInChildren<POIHandler>();

        foreach(POIHandler poi in _POIs)
        {
            poi.Setup(this, _idleColor);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPOI (int idOfSelectedPOI)
    {
        Debug.Log("inside manager: POI with id " + idOfSelectedPOI + " was clicked");

        foreach (POIHandler poi in _POIs)
        {
            if(poi.Content.Id == idOfSelectedPOI)
            {
                poi.SetColor(_selectedColor);
                _POIMenuController.Show(poi.Content);
                _currentlySelectedPOI = poi.Content.Id;

            } else
            {
                poi.SetColor(_idleColor);
            }
        }
    }
}
