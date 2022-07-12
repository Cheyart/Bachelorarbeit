using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New POI_DB", menuName = "Assets/POI_DB")]
public class PointOfInterestDB : ScriptableObject
{

    [SerializeField]
    private List<PointOfInterest> _poiDB;

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
