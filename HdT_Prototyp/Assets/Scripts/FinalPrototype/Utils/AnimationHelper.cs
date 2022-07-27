using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EasingFunction{
    easeIn, easeOut, easeInAndOut, linear
}
//source for easing transitions: https://www.febucci.com/2018/08/easing-functions/
//rename: animation helper
public static class AnimationHelper 
{
    public static float CalculateEasing(float t, EasingFunction easing)
    {
        switch (easing)
        {
            case EasingFunction.linear: return t;
            case EasingFunction.easeIn: return EaseIn(t);
            case EasingFunction.easeOut: return EaseOut(t);
            case EasingFunction.easeInAndOut: return EaseInAndOut(t);
            default: return t;
        }
    }

    private static float EaseInAndOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    private static float EaseIn(float t)
    {
         return Square(t);
    }

    private static float EaseOut(float t)
    {
        return Flip(Square(Flip(t)));
    }

    //return euler rotation angle of a direction vector
    public static float GetAngleFromDirectionVector(Vector2 vec)
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
