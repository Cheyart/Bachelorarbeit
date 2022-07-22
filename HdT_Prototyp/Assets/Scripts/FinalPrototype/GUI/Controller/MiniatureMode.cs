using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureMode : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject _cameraContainer;

    private const float CAMERA_CONTAINER_OFFSET = 90f;

    private Vector3 _cameraStartPosition;

    private Quaternion _cameraStartRotation;

    private Quaternion _cameraContainerStartRotation;

    private float _cameraStartFOV;
   
    private Camera _arCamera;

    private float _transitionDuration = 2f;

    [SerializeField]
    private UserPosition _userPosition;

    private float _angle = 0;

    private float _startZ, _targetZ;

    private int counter;

    // Start is called before the first frame update
    void Start()
    {
        //_cameraContainer.transform.localRotation = Quaternion.Euler(0, 0, 90);
        //Debug.Log(_cameraContainer.transform.localRotation);
        //StartCoroutine(LerpCameraContainerZRotation(0, 90));


    }

    // Update is called once per frame
    void Update()
    {
        /* _camera.transform.position = _arCamera.transform.position;
         _camera.transform.rotation = _arCamera.transform.rotation;
         _camera.fieldOfView = _arCamera.fieldOfView;*/
       // Debug.Log(_cameraContainer.transform.localRotation);

       //_angle = GetCameraContainerZRotation();

    }

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
        /*if(_cameraStartPosition == null || _cameraStartPosition.Equals(new Vector3 (0, 0, 0))) {
            _cameraStartPosition = _camera.transform.position;
            _cameraStartRotation = _camera.transform.rotation;

        }*/

        _cameraContainer.transform.localRotation = _cameraContainerStartRotation;
        StartCameraTransition();
        _camera.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _camera.gameObject.SetActive(false);
    }


    private void StartCameraTransition()
    {
        _camera.transform.position = _arCamera.transform.position;
        _camera.transform.rotation = _arCamera.transform.rotation;
        _camera.fieldOfView = _arCamera.fieldOfView;

        /*StartCoroutine(LerpCameraPosition(_arCamera.transform.position, _cameraStartPosition));
        StartCoroutine(LerpCameraRotation(_arCamera.transform.rotation, _cameraStartRotation));
        StartCoroutine(LerpCameraFOV(_arCamera.fieldOfView, _cameraStartFOV));*/
        StartCoroutine(LerpCameraPosition(_camera.transform.localPosition, _cameraStartPosition));
        StartCoroutine(LerpCameraRotation(_camera.transform.localRotation, _cameraStartRotation));
        StartCoroutine(LerpCameraFOV(_camera.fieldOfView, _cameraStartFOV));


        //float angle = GetCameraContainerZRotation();
        //StartCoroutine(LerpCameraContainerRotation(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, angle)))  ;
        //_cameraContainer.transform.localRotation = Quaternion.Euler(0, 0, 90);

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
            //_camera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            _camera.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            time += Time.deltaTime;
            yield return null;
        }

        //_camera.transform.position = targetPosition;
    }

    private IEnumerator LerpCameraRotation(Quaternion startRotation, Quaternion targetRotation)
    {
        //Debug.Log("Start Coroutine to pos y = " + targetPosition.y);
        float time = 0;

        while (time < _transitionDuration)
        {
            float t = time / _transitionDuration;
            t = t * t * (3f - 2f * t);
            //_camera.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            _camera.transform.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            time += Time.deltaTime;
            yield return null;
        }

        //_camera.transform.rotation = targetRotation;

        //_angle += 45f;
        _angle = GetCameraContainerZRotation();
        //StartCoroutine(LerpCameraContainerZRotation(_cameraContainer.transform.localRotation.eulerAngles.z, angle + CAMERA_CONTAINER_OFFSET));
        StartCoroutine(LerpCameraContainerZRotation(_cameraContainer.transform.localRotation.eulerAngles.z, _angle + CAMERA_CONTAINER_OFFSET));


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

        //_camera.fieldOfView = targetValue;
    }

    private IEnumerator LerpCameraContainerZRotation(float startZValue, float targetZValue)
    {
        _startZ = startZValue;
        _targetZ = targetZValue;
        Debug.Log("Start Coroutine z value = " + _cameraContainer.transform.localRotation.eulerAngles);
        float time = 0;
        float zValue = startZValue;
        Vector3 tempRotation = new Vector3(0, 0, startZValue);
        //Quaternion tempRotation = _cameraContainer.transform.localRotation;

        float duration = 7f;

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            tempRotation.z = Mathf.Lerp(startZValue, targetZValue, t);
            
            _cameraContainer.transform.localRotation = Quaternion.Euler(tempRotation);
            time += Time.deltaTime;
            yield return null;
        }

        

        Debug.Log("End Coroutine z value = " + _cameraContainer.transform.localRotation.eulerAngles);

        //_cameraContainer.transform.localRotation = targetRotation;
    }

    void OnGUI()
    {
       
        GUI.Label(new Rect(200, 250, 400, 100), " Start Position (local)= " + _cameraStartPosition);
        GUI.Label(new Rect(200, 300, 400, 100), " Start Rotation (local)= " + _cameraStartRotation.eulerAngles);
        GUI.Label(new Rect(200, 350, 400, 100), " Start FOV = " + _cameraStartFOV);
        GUI.Label(new Rect(200, 400, 400, 100), " current position = " + _camera.transform.position);
        GUI.Label(new Rect(200, 450, 400, 100), " current rotation = " + _camera.transform.rotation.eulerAngles);
        GUI.Label(new Rect(200, 500, 400, 100), " current local position = " + _camera.transform.localPosition);
        GUI.Label(new Rect(200, 550, 400, 100), " current local rotation = " + _camera.transform.localRotation.eulerAngles);
        GUI.Label(new Rect(200, 600, 400, 100), " current fov = " + _camera.fieldOfView);

        GUI.Label(new Rect(200, 700, 400, 100), " angle = " + (_angle + CAMERA_CONTAINER_OFFSET).ToString());
        GUI.Label(new Rect(200, 750, 400, 100), " camera container local rotation = " + _cameraContainer.transform.localRotation.eulerAngles);
        GUI.Label(new Rect(200, 800, 400, 100), " camera container local position = " + _cameraContainer.transform.localPosition);

        GUI.Label(new Rect(200, 850, 400, 100), " start Z = " + _startZ);
         GUI.Label(new Rect(200, 900, 400, 100), " target Z = " + _targetZ);









    }


}
