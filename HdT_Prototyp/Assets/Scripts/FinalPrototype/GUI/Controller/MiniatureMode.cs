using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition { toMiniature, fromMiniature}

//manages miniature mode setup and transition
public class MiniatureMode : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject _cameraContainer;

    [SerializeField]
    private UserPosition _userPosition;

    private Camera _arCamera;

    private Vector3 _cameraStartPosition;

    private Quaternion _cameraStartRotation;

    private float _cameraStartFOV;

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
        _arCamera = arCamera;
        _cameraStartFOV = _camera.fieldOfView;
        _cameraStartPosition = _camera.transform.localPosition;
        _cameraStartRotation = _camera.transform.localRotation;
        _cameraContainerStartRotation = _cameraContainer.transform.localRotation;
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
            _camera.transform.position = _arCamera.transform.position;
            _camera.transform.rotation = _arCamera.transform.rotation;
            _camera.fieldOfView = _arCamera.fieldOfView;
            _camera.gameObject.SetActive(true);

            //yield return StartCoroutine(SetCameraContainerRotation());
            StartCoroutine(LerpCameraPosition(_camera.transform.localPosition, _cameraStartPosition, Transition.toMiniature));
            StartCoroutine(LerpCameraRotation(_camera.transform.localRotation, _cameraStartRotation, Transition.toMiniature));
            yield return StartCoroutine(LerpCameraFOV(_camera.fieldOfView, _cameraStartFOV));
            StartCameraContainerRotation();

        } else if (transition == Transition.fromMiniature)
        {
            _userPosition.gameObject.SetActive(false);
            StartCoroutine(LerpCameraPosition(_camera.transform.position, _arCamera.transform.position, Transition.fromMiniature));
            StartCoroutine(LerpCameraRotation(_camera.transform.rotation, _arCamera.transform.rotation, Transition.fromMiniature));
            yield return StartCoroutine(LerpCameraFOV(_camera.fieldOfView, _arCamera.fieldOfView));
            _camera.gameObject.SetActive(false);

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
        float angle = GetCameraContainerRotationWitFrontalUserPos() + CAMERA_CONTAINER_OFFSET;
        

        //startValue%360
        if(startValue> 180)
        {
            startValue = -1 * (360 - startValue);
        }

        if (angle > 180)
        {
            angle = -1 * (360 - angle);
        }

        StartCoroutine(LerpCameraContainerZRotation(startValue, angle));
    }

    //transform into Util function: get angle between two points
    private float GetCameraContainerRotationWitFrontalUserPos()
    {
        Vector3 direction = TransitionHelper.GetNormalizedDirectionVector(_cameraContainer.transform.localPosition, _userPosition.transform.localPosition);
        float angle = TransitionHelper.GetRotationFromDirectionVector(new Vector2(direction.x, direction.y));
        return angle;
    }


    private IEnumerator LerpCameraPosition(Vector3 startPosition, Vector3 targetPosition, Transition transition)
    {
        float time = 0;

        while (time < CAMERA_TRANSITION_DURATION)
        {
            float t = time / CAMERA_TRANSITION_DURATION;
            //t = TransitionHelper.EaseIn(t);
            if(transition == Transition.toMiniature)
            {
                _camera.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, TransitionHelper.EaseIn(t));
            } else if (transition == Transition.fromMiniature)
            {
                _camera.transform.position = Vector3.Lerp(startPosition, targetPosition, TransitionHelper.EaseInAndOut(t));
            }

            time += Time.deltaTime;
            yield return null;
        }

        if (transition == Transition.toMiniature)
        {
            _camera.transform.localPosition = targetPosition;
        } 
    }

    private IEnumerator LerpCameraRotation(Quaternion startRotation, Quaternion targetRotation, Transition transition)
    {
        float time = 0;

        while (time < CAMERA_TRANSITION_DURATION)
        {
            float t = time / CAMERA_TRANSITION_DURATION;

            if(transition == Transition.toMiniature)
            {
                _camera.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, TransitionHelper.EaseIn(t));

            } else if(transition == Transition.fromMiniature)
            {
                _camera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, TransitionHelper.EaseInAndOut(t));
            }

            time += Time.deltaTime;
            yield return null;
        }

        if(transition == Transition.toMiniature)
        {
            _camera.transform.localRotation = targetRotation;
        }
    }

    private IEnumerator LerpCameraFOV(float startValue, float targetValue)
    {
        float time = 0;

        while (time < CAMERA_TRANSITION_DURATION)
        {
            float t = time / CAMERA_TRANSITION_DURATION;
            _camera.fieldOfView = Mathf.Lerp(startValue, targetValue, TransitionHelper.EaseIn(t));
            time += Time.deltaTime;
            yield return null;
        }
        _camera.fieldOfView = targetValue;

    }

    private IEnumerator LerpCameraContainerZRotation(float startZValue, float targetZValue)
    {
   
        float time = 0;
        Vector3 tempRotation = new Vector3(0, 0, startZValue);


        while (time < ROTATION_TRANSITION_DURATION)
        {
            float t = time / ROTATION_TRANSITION_DURATION;
            tempRotation.z = Mathf.Lerp(startZValue, targetZValue, TransitionHelper.EaseOut(t));
            
            _cameraContainer.transform.localRotation = Quaternion.Euler(tempRotation);
            time += Time.deltaTime;
            yield return null;
        }

        _cameraContainer.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, targetZValue) );

        Debug.Log("End Coroutine z value = " + _cameraContainer.transform.localRotation.eulerAngles);
    }


    /*void OnGUI()
    {
       
        GUI.Label(new Rect(200, 250, 400, 100), " Start Position (local)= " + _cameraStartPosition);
        GUI.Label(new Rect(200, 300, 400, 100), " Start Rotation (local)= " + _cameraStartRotation.eulerAngles);
        GUI.Label(new Rect(200, 350, 400, 100), " Start FOV = " + _cameraStartFOV);
        GUI.Label(new Rect(200, 400, 400, 100), " current position = " + _camera.transform.position);
        GUI.Label(new Rect(200, 450, 400, 100), " current rotation = " + _camera.transform.rotation.eulerAngles);
        GUI.Label(new Rect(200, 500, 400, 100), " current local position = " + _camera.transform.localPosition);
        GUI.Label(new Rect(200, 550, 400, 100), " current local rotation = " + _camera.transform.localRotation.eulerAngles);
        GUI.Label(new Rect(200, 600, 400, 100), " current fov = " + _camera.fieldOfView);

        GUI.Label(new Rect(200, 700, 400, 100), " angle = " + _angle);
        GUI.Label(new Rect(200, 750, 400, 100), " camera container local rotation = " + _cameraContainer.transform.localRotation.eulerAngles);
        GUI.Label(new Rect(200, 800, 400, 100), " camera container local position = " + _cameraContainer.transform.localPosition);

        GUI.Label(new Rect(200, 850, 400, 100), " start Z = " + _startZ);
         GUI.Label(new Rect(200, 900, 400, 100), " target Z = " + _targetZ);
        GUI.Label(new Rect(200, 950, 400, 100), " difference = " + _difference);


    }*/


}
