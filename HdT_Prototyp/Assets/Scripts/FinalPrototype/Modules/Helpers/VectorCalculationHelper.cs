using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorCalculationHelper 
{
    //return euler rotation angle of a direction vector
    public static float GetAngleFromDirectionVector(Vector2 vec)
    {
        return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
    }

    public static Vector3 GetNormalizedDirectionVector(Vector3 v1, Vector3 v2)
    {
        return (v1 - v2).normalized;
    }
}
