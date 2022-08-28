using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowFiPrototype
{
    public class POISelectionController : MonoBehaviour
    {
        [SerializeField]
        private PointOfInterest[] _POIs;

        private int _currentSelectedPOI;



        // Start is called before the first frame update
        void Start()
        {
            _currentSelectedPOI = -1;
            int id = 5;
            foreach (PointOfInterest poi in _POIs)
            {
                poi.Id = id;
                poi.IsSelected = false;
                id++;
            }
        }


        public void switchSelectedPOI(int idOfNewSelection)
        {
            int tempId = -1;
            if (_currentSelectedPOI != idOfNewSelection)
            {
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
            }
            else if (_currentSelectedPOI == idOfNewSelection)
            {
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
    }
}
