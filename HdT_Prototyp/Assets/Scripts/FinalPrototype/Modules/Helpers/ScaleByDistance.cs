using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class ScaleByDistance This class scales an object according to the distance between the camera and the object.
 */
public class ScaleByDistance : MonoBehaviour
{

    [SerializeField]
    private Camera _camera; /** Active Camera which will be used to determine the distance to the object*/
    public Camera Camera { get => _camera; set => _camera = value; }

    [SerializeField]
    private float _minDistance; /** Minimum Distance of the scaling (under this distance no scaling will take place)*/

    [SerializeField]
    private float _maxDistance; /** Maximum Distance of the scaling (over this distance no scaling will take place)*/

    [SerializeField]
    private float _scaleAtMinDist; /** The Scale of the object at the Minimum Distance*/

    [SerializeField]
    private float _scaleAtMaxDist; /** The Scale of the object at the Maximum Distance*/

    private float _distance; /** current distance to the camera*/
    private float _scale; /** current scale*/


    void FixedUpdate()
    {
        _distance = Vector3.Distance(_camera.transform.position, transform.position);
        _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);
        _scale = MappingCalculator.Remap(_distance, _minDistance, _maxDistance, _scaleAtMinDist, _scaleAtMaxDist);
        transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    /**
     * Sets the values for this class
     */
    public void SetValues(float minDist, float maxDist, float scaleAtMinDist, float scaleAtMaxDist)
    {
        _minDistance = minDist;
        _maxDistance = maxDist;
        _scaleAtMinDist = scaleAtMinDist;
        _scaleAtMaxDist = scaleAtMaxDist;
    }

}
