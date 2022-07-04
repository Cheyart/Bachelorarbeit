using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POISelectionController : MonoBehaviour
{
    [SerializeField]
    private PointOfInterest[] _POIs;

    private int _currentSelectedPOI;
    private int _idOfNewSelection;
    private int _insideA;
    private int _insideB;





    // Start is called before the first frame update
    void Start()
    {
        _currentSelectedPOI = -1;
        int id = 5;
        foreach(PointOfInterest poi in _POIs)
        {
            poi.Id = id;
            poi.IsSelected = false;
            id++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchSelectedPOI (int idOfNewSelection){
        _idOfNewSelection = idOfNewSelection;
        int tempId = -1;
        if(_currentSelectedPOI != idOfNewSelection)
        {
            _insideA++;
            foreach (PointOfInterest poi in _POIs)
            {
                if (poi.Id != idOfNewSelection)
                {
                    poi.IsSelected = false;
                }
                else
                {
                    poi.IsSelected = true;
                }
            }
             tempId = idOfNewSelection;
        } else if (_currentSelectedPOI == idOfNewSelection)
        {
            _insideB++;
            foreach (PointOfInterest poi in _POIs)
            {
                if (poi.Id == idOfNewSelection)
                {
                    poi.IsSelected = false;
                    tempId = -1;
                }
               
            }
        }
        _currentSelectedPOI = tempId;

    }

     /*void OnGUI()
  {
      

      GUI.Label(new Rect(200, 250, 400, 100), " current selected POI = " + _currentSelectedPOI);
        GUI.Label(new Rect(200, 300, 400, 100), " id of new selection = " + _idOfNewSelection);
        GUI.Label(new Rect(200, 350, 400, 100), " Inside A = " + _insideA);
        GUI.Label(new Rect(200, 400, 400, 100), " Inside B = " + _insideB);

    }*/
}
