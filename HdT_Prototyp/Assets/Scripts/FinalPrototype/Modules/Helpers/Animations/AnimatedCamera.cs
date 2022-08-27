using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class AnimatedCamera This class contains functions to animate a Camera object
 */
[RequireComponent(typeof(Camera))]
public class AnimatedCamera : MonoBehaviour
{
    private Camera _camera; /** Camera component which will be animated*/

    void Start()
    {
        SetComponents();
    }

    /** Lerp the field of view of the camera
     * @param startValue Start value of the FOV
     * @param targetValue Target value of the FOV
     * @param duration Duration of the animation
     * @param easing Easing function which will be used for the animation
     */
    public IEnumerator LerpFOV(float startValue, float targetValue, float duration, EasingFunction easing)
    {
        if(_camera == null)
        {
            SetComponents();
        }

        float time = 0;

        while (time < duration)
        {
            float t = time / duration;
            _camera.fieldOfView = Mathf.Lerp(startValue, targetValue, EasingFunctionCalculator.CalculateEasing(t, easing));
            time += Time.deltaTime;
            yield return null;
        }
        _camera.fieldOfView = targetValue;
    }

    /**
     * Sets the components needed for this class
     */
    private void SetComponents()
    {
        _camera = GetComponent<Camera>();

    }
}

