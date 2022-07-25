using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition { toMiniature, fromMiniature}

//manages miniature mode setup and transition
public class MiniatureMode : MonoBehaviour
{
    //change to MiniatureCamera
   /* [SerializeField]
    private Camera _camera;
    public Camera Camera { get => _camera; }*/

    [SerializeField]
    private MiniatureCamera _miniatureCamera;
    public Camera Camera { get => _miniatureCamera.Camera; }

    [SerializeField]
    private AnimatedObject _cameraContainer;

    // [SerializeField]
    //private GameObject _cameraContainer;

    [SerializeField]
    private UserPosition _userPosition;

   /* private Camera _arCamera;

    private Vector3 _cameraStartPosition;

    private Quaternion _cameraStartRotation;

    private float _cameraStartFOV;*/

    private Quaternion _cameraContainerStartRotation;

    private const float CAMERA_TRANSITION_DURATION = 0.7f;
    private const float ROTATION_TRANSITION_DURATION = 1.4f;
    private const float CAMERA_CONTAINER_OFFSET = 90f;

    /*private float _angle = 0;
    private float _difference = 0;
    private float _startZ, _targetZ;*/


    public void Setup(Camera arCamera)
    {
        _userPosition.ArCamera = arCamera;
        _miniatureCamera.Setup(arCamera, _miniatureCamera.transform.localPosition, _miniatureCamera.transform.localRotation, _miniatureCamera.Camera.fieldOfView);
        _cameraContainerStartRotation = _cameraContainer.transform.localRotation;


        //_miniatureCamera.ARCamera = arCamera;
        //_arCamera = arCamera;
        //_cameraStartFOV = _camera.fieldOfView;
        //_cameraStartPosition = _camera.transform.localPosition;
        //_cameraStartRotation = _camera.transform.localRotation;
    }

    public void Show()
    {
        StartCoroutine(ExecuteTransition(Transition.toMiniature));
    }

    public void Hide()
    {
        StartCoroutine(ExecuteTransition(Transition.fromMiniature));
    }


    private IEnumerator ExecuteTransition(Transition transition)
    {
        if(transition == Transition.toMiniature)
        {
            _userPosition.gameObject.SetActive(true);
            /* _camera.transform.position = _arCamera.transform.position;
             _camera.transform.rotation = _arCamera.transform.rotation;
             _camera.fieldOfView = _arCamera.fieldOfView;*/
            _miniatureCamera.AlignWithARCamera();
            _miniatureCamera.gameObject.SetActive(true);

            //yield return StartCoroutine(SetCameraContainerRotation());
            //StartCoroutine(_miniatureCamera.LerpPosition(_camera.transform.localPosition, _cameraStartPosition, CAMERA_TRANSITION_DURATION, Space.Self, EasingFunction.easeIn));
            yield return StartCoroutine(_miniatureCamera.TransitionToMiniatureMode(CAMERA_TRANSITION_DURATION));

           // StartCoroutine(LerpCameraRotation(_camera.transform.localRotation, _cameraStartRotation, Transition.toMiniature));
           // yield return StartCoroutine(LerpCameraFOV(_camera.fieldOfView, _cameraStartFOV));
            StartCameraContainerRotation();

        } else if (transition == Transition.fromMiniature)
        {
            _userPosition.gameObject.SetActive(false);
            //StartCoroutine(_miniatureCamera.LerpPosition(_camera.transform.position, _arCamera.transform.position, CAMERA_TRANSITION_DURATION, Space.World, EasingFunction.easeInAndOut));
            yield return StartCoroutine(_miniatureCamera.TransitionFromMiniatureMode(CAMERA_TRANSITION_DURATION));


            //StartCoroutine(LerpCameraRotation(_camera.transform.rotation, _arCamera.transform.rotation, Transition.fromMiniature));
            //yield return StartCoroutine(LerpCameraFOV(_camera.fieldOfView, _arCamera.fieldOfView));
            _miniatureCamera.gameObject.SetActive(false);

        }


    }

   /* private IEnumerator SetCameraContainerRotation()
    {
        _angle = GetCameraContainerRotationWitFrontalUserPos();
        _cameraContainer.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, _angle + CAMERA_CONTAINER_OFFSET));
        yield return null;
    }*/

    private void StartCameraContainerRotation()
    {
        float startValue = _cameraContainer.transform.localRotation.eulerAngles.z;
        float angle = GetCameraContainerRotationWithFrontalUserPos() + CAMERA_CONTAINER_OFFSET;
        

        //startValue%360
        if(startValue> 180)
        {
            startValue = -1 * (360 - startValue);
        }

        if (angle > 180)
        {
            angle = -1 * (360 - angle);
        }

        // StartCoroutine(LerpCameraContainerZRotation(startValue, angle));
        StartCoroutine(_cameraContainer.LerpRotation(Quaternion.Euler(0, 0, startValue), Quaternion.Euler(0, 0, angle), ROTATION_TRANSITION_DURATION, Space.Self, EasingFunction.easeOut));
    }

    //transform into Util function: get angle between two points
    private float GetCameraContainerRotationWithFrontalUserPos()
    {
        Vector3 direction = AnimationHelper.GetNormalizedDirectionVector(_cameraContainer.transform.localPosition, _userPosition.transform.localPosition);
        float angle = AnimationHelper.GetRotationFromDirectionVector(new Vector2(direction.x, direction.y));
        return angle;
    }


    /*private IEnumerator LerpCameraPosition(Vector3 startPosition, Vector3 targetPosition, Transition transition)
    {
        float time = 0;

        while (time < CAMERA_TRANSITION_DURATION)
        {
            float t = time / CAMERA_TRANSITION_DURATION;
            //t = TransitionHelper.EaseIn(t);
            if(transition == Transition.toMiniature)
            {
                _camera.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, AnimationHelper.CalculateEasing(t, EasingFunction.easeIn));
            } else if (transition == Transition.fromMiniature)
            {
                _camera.transform.position = Vector3.Lerp(startPosition, targetPosition, AnimationHelper.CalculateEasing(t, EasingFunction.easInAndOut));
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (transition == Transition.toMiniature)
        {
            _camera.transform.localPosition = targetPosition;
        } 
    }*/

  /*  private IEnumerator LerpCameraRotation(Quaternion startRotation, Quaternion targetRotation, Transition transition)
    {
        float time = 0;

        while (time < CAMERA_TRANSITION_DURATION)
        {
            float t = time / CAMERA_TRANSITION_DURATION;

            if(transition == Transition.toMiniature)
            {
                _camera.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, AnimationHelper.CalculateEasing(t, EasingFunction.easeIn));

            } else if(transition == Transition.fromMiniature)
            {
                _camera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, AnimationHelper.CalculateEasing(t, EasingFunction.easeInAndOut));
            }

            time += Time.deltaTime;
            yield return null;
        }

        if(transition == Transition.toMiniature)
        {
            _camera.transform.localRotation = targetRotation;
        }
    }*/

   /* private IEnumerator LerpCameraFOV(float startValue, float targetValue)
    {
        float time = 0;

        while (time < CAMERA_TRANSITION_DURATION)
        {
            float t = time / CAMERA_TRANSITION_DURATION;
            _camera.fieldOfView = Mathf.Lerp(startValue, targetValue, AnimationHelper.CalculateEasing(t, EasingFunction.easeIn));
            time += Time.deltaTime;
            yield return null;
        }
        _camera.fieldOfView = targetValue;

    }*/

   /* private IEnumerator LerpCameraContainerZRotation(float startZValue, float targetZValue)
    {
   
        float time = 0;
        Vector3 tempRotation = new Vector3(0, 0, startZValue);


        while (time < ROTATION_TRANSITION_DURATION)
        {
            float t = time / ROTATION_TRANSITION_DURATION;
            tempRotation.z = Mathf.Lerp(startZValue, targetZValue, AnimationHelper.CalculateEasing(t, EasingFunction.easeOut));
            
            _cameraContainer.transform.localRotation = Quaternion.Euler(tempRotation);
            time += Time.deltaTime;
            yield return null;
        }

        _cameraContainer.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, targetZValue) );

        Debug.Log("End Coroutine z value = " + _cameraContainer.transform.localRotation.eulerAngles);
    }*/


    void OnGUI()
    {
       
        //GUI.Label(new Rect(200, 250, 400, 100), " Start Position (miniature mode)= " + _cameraStartPosition);
       


    }


}
