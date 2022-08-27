using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class ScaleByDistance This class scales an object according to the distance between the camera and the object.
 */
public class ScaleByDistance : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;
    public Camera Camera { get => _camera; set => _camera = value; }

    [SerializeField]
    private float _minDistance;

    [SerializeField]
    private float _maxDistance;

    [SerializeField]
    private float _scaleAtMinDist;

    [SerializeField]
    private float _scaleAtMaxDist;

    private float _distance;
    private float _scale;


    // Update is called once per frame
    void FixedUpdate()
    {
        _distance = Vector3.Distance(_camera.transform.position, transform.position);
        _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
        _scale = MappingCalculator.Remap(_distance, _minDistance, _maxDistance, _scaleAtMinDist, _scaleAtMaxDist);
        transform.localScale = new Vector3(_scale, _scale, _scale);
    }


    public void SetValues(float minDist, float maxDist, float scaleAtMinDist, float scaleAtMaxDist)
    {
        _minDistance = minDist;
        _maxDistance = maxDist;
        _scaleAtMinDist = scaleAtMinDist;
        _scaleAtMaxDist = scaleAtMaxDist;
    }

    //util
    /*public static float Remap(float val, float in1, float in2, float out1, float out2)
    {
        return out1 + (val - in1) * (out2 - out1) / (in2 - in1);
    }*/


}
