using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatedObject), typeof(Camera))]
public class MiniatureCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _thisCamera;
    public Camera Camera { get => _thisCamera; }

    private AnimatedObject _animation;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private float _startFOV;

    private Camera _arCamera;


    // Start is called before the first frame update
    void Start()
    {
        SetComponents();
    }


    public void Setup(Camera arCamera, Vector3 startPosition, Quaternion startRotation, float startFOV)
    {
        SetComponents();
        _startPosition = startPosition;
        _startRotation = startRotation;
        _startFOV = startFOV;
        _arCamera = arCamera;
    }

    public void AlignWithARCamera()
    {
        transform.position = _arCamera.transform.position;
        transform.rotation = _arCamera.transform.rotation;
        _thisCamera.fieldOfView = _arCamera.fieldOfView;
    }

    public IEnumerator TransitionToMiniatureMode(float duration)
    {
        StartCoroutine(_animation.LerpPosition(transform.localPosition, _startPosition, duration, Space.Self, EasingFunction.easeIn));
        StartCoroutine(_animation.LerpRotation(transform.localRotation, _startRotation, duration, Space.Self, EasingFunction.easeIn));
        yield return StartCoroutine(LerpFOV(_thisCamera.fieldOfView, _startFOV, duration, EasingFunction.easeIn));

    }

    public IEnumerator TransitionFromMiniatureMode(float duration)
    {
        StartCoroutine(_animation.LerpPosition(transform.position, _arCamera.transform.position, duration, Space.World, EasingFunction.easeInAndOut));
        StartCoroutine(_animation.LerpRotation(transform.rotation, _arCamera.transform.rotation, duration, Space.World, EasingFunction.easeInAndOut));
        yield return StartCoroutine(LerpFOV(_thisCamera.fieldOfView, _arCamera.fieldOfView, duration, EasingFunction.easeInAndOut));
    }

   
    private void SetComponents()
    {
        _animation = GetComponent<AnimatedObject>();
        _thisCamera = GetComponent<Camera>();
    }

    private IEnumerator LerpFOV(float startValue, float targetValue, float duration, EasingFunction easing)
    {
        float time = 0;

        while (time < duration)
        {
            float t = time / duration;
            _thisCamera.fieldOfView = Mathf.Lerp(startValue, targetValue, EasingFunctionCalculator.CalculateEasing(t, easing));
            time += Time.deltaTime;
            yield return null;
        }
        _thisCamera.fieldOfView = targetValue;

    }


}
