using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class POISelectionManager : MonoBehaviour
{

    private POIHandler [] _POIs;

    [SerializeField]
    private POIMenuManager _POIMenuManager;

    [SerializeField]
    private OffScreenPointer _offScreenPointer;

    [SerializeField]
    private Color _idleColor;

    [SerializeField]
    private Color _selectedColor;

    private SessionManager _sessionManager;

    // Start is called before the first frame update
    void Awake()
    {
        //_sessionManager = SessionManager.Instance;
  
    }

    public void SetupPOIs()
    {
        _POIs = FindObjectsOfType<POIHandler>(true);



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
                //_sessionManager.ActivePOI = poi.Content;

                if (poi.IsVisibleInAR)
                {
                    _offScreenPointer.Target = poi.gameObject;
                }
            
               


            }
            else
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
        _offScreenPointer.Target = null;
    }

    private bool IsActivePOI(int POIId)
    {
        return (SessionManager.Instance.ActivePOI != null && POIId == SessionManager.Instance.ActivePOI.Id);
    }

 

    void OnGUI()
    {
        if(_POIs != null)
        {
            GUI.Label(new Rect(200, 700, 400, 100), " Nr of POIs = " + _POIs.Length);

        }
       


    }



}
