using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//source for easing transitions: https://www.febucci.com/2018/08/easing-functions/
//rename: animation helper
public static class TransitionHelper 
{

    public static float EaseInAndOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    public static float EaseIn(float t)
    {
         return Square(t);
    }

    public static float EaseOut(float t)
    {
        return Flip(Square(Flip(t)));
    }

    //return euler rotation angle of a direction vector
    public static float GetRotationFromDirectionVector(Vector2 vec)
    {
        return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
    }

    public static Vector3 GetNormalizedDirectionVector(Vector3 v1, Vector3 v2)
    {
        return (v1 - v2).normalized;
    }

    private static float Flip(float x)
    {
        return 1 - x;
    }

    private static float Square (float x)
    {
        return x * x;
    }
}
