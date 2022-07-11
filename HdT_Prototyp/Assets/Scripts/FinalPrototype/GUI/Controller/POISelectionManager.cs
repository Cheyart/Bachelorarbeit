using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISelectionManager : MonoBehaviour
{

    private POIHandler [] _POIs;

    [SerializeField]
    private POIMenuManager _POIMenuManager;

    [SerializeField]
    private Color _idleColor;

    [SerializeField]
    private Color _selectedColor;

    // Start is called before the first frame update
    void Awake()
    {
        _POIs = FindObjectsOfType<POIHandler>();


        foreach (POIHandler poi in _POIs)
        {
            poi.Setup(this, _idleColor);
        }
    }


   

    public void SelectPOI (int idOfSelectedPOI)
    {

        if(IsActivePOI(idOfSelectedPOI))
        {
            return;
        }

        foreach (POIHandler poi in _POIs)
        {
            if(poi != null && poi.Content.Id == idOfSelectedPOI)
            {
                poi.SetColor(_selectedColor);
                _POIMenuManager.OpenMenu(poi.Content);
                SessionManager.Instance.ActivePOI = poi.Content;

            } else
            {
                poi.SetColor(_idleColor);
            }
        }
    }

    public void DeselectCurrentPOI()
    {
        foreach (POIHandler poi in _POIs)
        {
            if (IsActivePOI(poi.Content.Id))
            {
                poi.SetColor(_idleColor);
            }
        }
        SessionManager.Instance.ActivePOI = null;
    }

    private bool IsActivePOI(int POIId)
    {
        return (SessionManager.Instance.ActivePOI != null && POIId == SessionManager.Instance.ActivePOI.Id);
    }




}
