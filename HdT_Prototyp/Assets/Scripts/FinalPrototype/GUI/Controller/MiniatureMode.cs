using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureMode : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject _cameraContainer;

    private const float CAMERA_CONTAINER_OFFSET = -50f;

    private Vector3 _cameraStartPosition;

    private Quaternion _cameraStartRotation;

    private float _cameraStartFOV;
   
    private Camera _arCamera;

    private float _transitionDuration = 0.7f;

    [SerializeField]
    private UserPosition _userPosition;

    private float _angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* _camera.transform.position = _arCamera.transform.position;
         _camera.transform.rotation = _arCamera.transform.rotation;
         _camera.fieldOfView = _arCamera.fieldOfView;*/

        _angle = GetCameraContainerZRotation();

    }

    public void Setup(Camera arCamera)
    {
        _userPosition.ArCamera = arCamera;
        _arCamera = arCamera;
        _cameraStartFOV = _camera.fieldOfView;


    }

    public void Show()
    {
        if(_cameraStartPosition == null || _cameraStartPosition.Equals(new Vector3 (0, 0, 0))) {
            _cameraStartPosition = _camera.transform.position;
            _cameraStartRotation = _camera.transform.rotation;

        }


        StartCameraTransition();
        _camera.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _camera.gameObject.SetActive(false);
    }


    private void StartCameraTransition()
    {
        StartCoroutine(LerpCameraPosition(_arCamera.transform.position, _cameraStartPosition));
        StartCoroutine(LerpCameraRotation(_arCamera.transform.rotation, _cameraStartRotation));
        StartCoroutine(LerpCameraFOV(_arCamera.fieldOfView, _cameraStartFOV));
       // float angle = GetCameraContainerZRotation();
       // StartCoroutine(LerpCameraContainerRotation(Quaternion.identity, Quaternion.Euler(0, 0, angle)))  ;
    }

    //transform into Util function: get angle between two points
    private float GetCameraContainerZRotation()
    {

        Vector3 direction = (_cameraContainer.transform.localPosition - _userPosition.transform.localPosition).normalized;
        float angle = GetAngleFromVector(new Vector2(direction.x, direction.y));
        return angle;

    }

    //Util function
    private float GetAngleFromVector(Vector2 vec)
    {
        return (Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg) % 360;
    }


    private IEnumerator LerpCameraPosition(Vector3 startPosition, Vector3 targetPosition)
    {
        //Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
        float time = 0;

        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _camera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            yield return null;
        }

        _camera.transform.position = targetPosition;
    }

    private IEnumerator LerpCameraRotation(Quaternion startRotation, Quaternion targetRotation)
    {
        //Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
        float time = 0;

        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _camera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            time += Time.deltaTime;
            yield return null;
        }

        _camera.transform.rotation = targetRotation;
    }

    private IEnumerator LerpCameraFOV(float startValue, float targetValue)
    {
        //Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
        float time = 0;

        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _camera.fieldOfView = Mathf.Lerp(startValue, targetValue, t);
            time += Time.deltaTime;
            yield return null;
        }

        _camera.fieldOfView = targetValue;
    }

    private IEnumerator LerpCameraContainerRotation(Quaternion startRotation, Quaternion targetRotation)
    {
        //Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
        float time = 0;

        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            _cameraContainer.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            time += Time.deltaTime;
            yield return null;
        }

        _cameraContainer.transform.rotation = targetRotation;
    }

    void OnGUI()
    {
       
        GUI.Label(new Rect(200, 250, 400, 100), " Start Position = " + _cameraStartPosition);
        GUI.Label(new Rect(200, 300, 400, 100), " Start Rotation = " + _cameraStartRotation);
        GUI.Label(new Rect(200, 350, 400, 100), " Start FOV = " + _cameraStartFOV);
        GUI.Label(new Rect(200, 400, 400, 100), " current position = " + _camera.transform.position);
        GUI.Label(new Rect(200, 450, 400, 100), " current rotation = " + _camera.transform.rotation);
        GUI.Label(new Rect(200, 500, 400, 100), " current fov = " + _camera.fieldOfView);
        GUI.Label(new Rect(200, 550, 400, 100), " angle = " + _angle);








    }


}
