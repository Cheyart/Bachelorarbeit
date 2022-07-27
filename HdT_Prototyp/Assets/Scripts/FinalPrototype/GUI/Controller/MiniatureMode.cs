using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Transition { toMiniature, fromMiniature}

//manages miniature mode setup and transition
public class MiniatureMode : MonoBehaviour
{

    [SerializeField]
    private MiniatureCamera _miniatureCamera;
    public Camera Camera { get => _miniatureCamera.Camera; }

    [SerializeField]
    private AnimatedObject _cameraContainer;

    [SerializeField]
    private UserPosition _userPosition;


    private Quaternion _cameraContainerStartRotation;

    private const float CAMERA_TRANSITION_DURATION = 0.7f;
    private const float ROTATION_TRANSITION_DURATION = 1.4f;
    private const float CAMERA_CONTAINER_OFFSET = 90f;



    public void Setup(Camera arCamera)
    {
        _userPosition.ArCamera = arCamera;
        _miniatureCamera.Setup(arCamera, _miniatureCamera.transform.localPosition, _miniatureCamera.transform.localRotation, _miniatureCamera.Camera.fieldOfView);
        _cameraContainerStartRotation = _cameraContainer.transform.localRotation;

    }

    public void Show()
    {
        StartCoroutine(ExecuteTransition(Transition.toMiniature));
    }

    public IEnumerator Hide()
    {
        yield return StartCoroutine(ExecuteTransition(Transition.fromMiniature));
    }


    private IEnumerator ExecuteTransition(Transition transition)
    {
        if(transition == Transition.toMiniature)
        {
            _userPosition.gameObject.SetActive(true);
          
            _miniatureCamera.AlignWithARCamera();
            _miniatureCamera.gameObject.SetActive(true);
            yield return StartCoroutine(_miniatureCamera.TransitionToMiniatureMode(CAMERA_TRANSITION_DURATION));
            StartCameraContainerRotation();

        } else if (transition == Transition.fromMiniature)
        {
            _userPosition.gameObject.SetActive(false);
            yield return StartCoroutine(_miniatureCamera.TransitionFromMiniatureMode(CAMERA_TRANSITION_DURATION));
            _miniatureCamera.gameObject.SetActive(false);
        }
    }


    private void StartCameraContainerRotation()
    {
        float startValue = _cameraContainer.transform.localRotation.eulerAngles.z;
        float angle = GetCameraContainerRotationWithFrontalUserPos() + CAMERA_CONTAINER_OFFSET;
        

        if(startValue> 180)
        {
            startValue = -1 * (360 - startValue);
        }

        if (angle > 180)
        {
            angle = -1 * (360 - angle);
        }

        StartCoroutine(_cameraContainer.LerpRotation(Quaternion.Euler(0, 0, startValue), Quaternion.Euler(0, 0, angle), ROTATION_TRANSITION_DURATION, Space.Self, EasingFunction.easeOut));
    }

    //transform into Util function: get angle between two points
    private float GetCameraContainerRotationWithFrontalUserPos()
    {
        Vector3 direction = AnimationHelper.GetNormalizedDirectionVector(_cameraContainer.transform.localPosition, _userPosition.transform.localPosition);
        float angle = AnimationHelper.GetAngleFromDirectionVector(new Vector2(direction.x, direction.y));
        return angle;
    }

    void OnGUI()
    {
       
        //GUI.Label(new Rect(200, 250, 400, 100), " Start Position (miniature mode)= " + _cameraStartPosition);
       


    }


}
