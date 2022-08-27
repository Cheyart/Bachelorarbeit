using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @enum Transition defines the which transition takes place.
 */
public enum Transition { toMiniature, fromMiniature }


/** @class MiniatureModeController controls the miniature mode setup and the transition from/to miniature mode.
 */
public class MiniatureModeController : MonoBehaviour
{

    [SerializeField]
    private MiniatureCamera _miniatureCamera;
    public Camera Camera { get => _miniatureCamera.Camera; }

    [SerializeField]
    private AnimatedTransform _cameraContainer;

    [SerializeField]
    private UserPosition _userPosition;


    private const float CAMERA_TRANSITION_DURATION = 0.7f;
    private const float ROTATION_TRANSITION_DURATION = 1.4f;
    private const float CAMERA_CONTAINER_OFFSET = 90f;

    private bool _instructionWasShown;

    public void Setup(Camera arCamera)
    {
        _userPosition.ArCamera = arCamera;
        _miniatureCamera.Setup(arCamera, _miniatureCamera.transform.localPosition, _miniatureCamera.transform.localRotation, _miniatureCamera.Camera.fieldOfView);

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
        if (transition == Transition.toMiniature)
        {
            _userPosition.gameObject.SetActive(true);

            _miniatureCamera.AlignWithARCamera();
            _miniatureCamera.gameObject.SetActive(true);
            yield return StartCoroutine(_miniatureCamera.TransitionToMiniatureMode(CAMERA_TRANSITION_DURATION));
            StartCameraContainerRotation();

        }
        else if (transition == Transition.fromMiniature)
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

        if (startValue > 180)
        {
            startValue = -1 * (360 - startValue);
        }

        if (angle > 180)
        {
            angle = -1 * (360 - angle);
        }

        StartCoroutine(_cameraContainer.LerpRotation(Quaternion.Euler(0, 0, startValue), Quaternion.Euler(0, 0, angle), ROTATION_TRANSITION_DURATION, Space.Self, EasingFunction.easeOut));
    }

    private float GetCameraContainerRotationWithFrontalUserPos()
    {
        Vector3 direction = VectorCalculationHelper.GetNormalizedDirectionVector(_cameraContainer.transform.localPosition, _userPosition.transform.localPosition);
        float angle = VectorCalculationHelper.GetAngleFromDirectionVector(new Vector2(direction.x, direction.y));
        return angle;
    }


}
