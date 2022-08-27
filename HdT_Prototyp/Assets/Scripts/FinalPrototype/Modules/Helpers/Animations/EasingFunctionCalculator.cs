using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EasingFunction{
    easeIn, easeOut, easeInAndOut, linear
}

public static class EasingFunctionCalculator 
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

    private static float Flip(float x)
    {
        return 1 - x;
    }

    private static float Square (float x)
    {
        return x * x;
    }
}
