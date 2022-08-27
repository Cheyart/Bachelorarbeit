using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @enum EasingFunction defines the type of Easing Function*/
public enum EasingFunction{
    easeIn, easeOut, easeInAndOut, linear
}

//source of the easing funtions: https://www.febucci.com/2018/08/easing-functions/

/**
 * @class EasingFunctionCalculator calculates easing functions used for animations
 */
public static class EasingFunctionCalculator 
{
    /** Returns the value of an easing function
     * @param t input value
     * @param easing Type of easing for which a value will be calculated
     * @return the result of the easing function
     */
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

    /**
     * Calculates value for an Ease in and out function
     * @param t input function
     * @return calculated value
     */
    private static float EaseInAndOut(float t)
    {
        return t * t * (3f - 2f * t);
    }

    /**
     * Calculates value for an Ease In function
     * @param t input function
     * @return calculated value
     */
    private static float EaseIn(float t)
    {
         return Square(t);
    }

    /**
     * Calculates value for an Ease Out function
     * @param t input function
     * @return calculated value
     */
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
