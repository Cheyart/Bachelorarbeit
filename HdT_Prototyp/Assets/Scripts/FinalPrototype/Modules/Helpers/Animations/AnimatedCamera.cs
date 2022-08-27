using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class AnimatedCamera This class contains functions to animate a Camera object
 */
[RequireComponent(typeof(Camera))]
public class AnimatedCamera : MonoBehaviour
{
    private Camera _camera;

    void Start()
    {
        SetComponents();
    }

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

    private void SetComponents()
    {
        _camera = GetComponent<Camera>();

    }
}

