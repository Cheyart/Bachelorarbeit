using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class VectorCalculationHelper This class contains functions for vector calculations.
 */
public static class VectorCalculationHelper 
{

    /**
     * Calculates the angle of a direction vector
     * @param vec direction vector
     * @return calucalated angle
     */
    public static float GetAngleFromDirectionVector(Vector2 vec)
    {
        return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
    }

    /**
     * Calucalates the normalized direction vector of two points
     * @param p1 point 1
     * @param p2 point 2
     * @return direction vector
     */
    public static Vector3 GetNormalizedDirectionVector(Vector3 p1, Vector3 p2)
    {
        return (p1 - p2).normalized;
    }
}
