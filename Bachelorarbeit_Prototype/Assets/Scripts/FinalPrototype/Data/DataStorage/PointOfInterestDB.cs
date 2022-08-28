using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class PointOfInterestDB This class represents the POI database, containing all the POIs in the system*/
[CreateAssetMenu(fileName = "New POI_DB", menuName = "Assets/POI_DB")]
public class PointOfInterestDB : ScriptableObject
{

    [SerializeField]
    private List<PointOfInterest> _poiDB; /** List simulating the POI database*/

    /** Sets up the databse (deletes void entries)
     */
    public void Setup()
    {
        _poiDB.RemoveAll(poi => poi == null);

        foreach(PointOfInterest poi in _poiDB)
        {
            poi.Threads.RemoveAll(thread => thread == null);
            poi.SetNrOfComments();
        }
    }
}
