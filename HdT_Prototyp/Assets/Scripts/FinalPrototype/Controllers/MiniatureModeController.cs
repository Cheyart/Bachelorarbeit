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
    private MiniatureCamera _miniatureCamera; /** Camera used in the Miniature mode*/
    public Camera Camera { get => _miniatureCamera.Camera; }  /** Camera used in the Miniature mode*/

    [SerializeField]
    private AnimatedTransform _cameraContainer; /** Container of the Miniature Camera */

    [SerializeField]
    private UserPosition _userPosition; /** user position object */


    private const float CAMERA_TRANSITION_DURATION = 0.7f; /** camera transition time to transition between AR and Miniature mode */
    private const float ROTATION_TRANSITION_DURATION = 1.4f; /** rotation transition duration after switch to Miniature mode */
    private const float CAMERA_CONTAINER_OFFSET = 90f; /** z-rotation offset of the camera container */

    /** Sets up the Miniature mode
     * @param arCamera Camera active in AR mode
     */
    public void Setup(Camera arCamera)
    {
        _userPosition.ArCamera = arCamera;
        _miniatureCamera.Setup(arCamera, _miniatureCamera.transform.localPosition, _miniatureCamera.transform.localRotation, _miniatureCamera.Camera.fieldOfView);

    }

    /** Transition to Miniature Mode
     */
    public void Show()
    {
        StartCoroutine(ExecuteTransition(Transition.toMiniature));
    }

    /**
     * Transition out of Miniature Mode
     */
    public IEnumerator Hide()
    {
        yield return StartCoroutine(ExecuteTransition(Transition.fromMiniature));
    }

    /** Executes a transition to or from Miniature mode
     * @paramn transition Indicates if the transition should be to or from Miniature mode
     */
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

    /** Animated the Miniature Camera Rotation according the the user position
     */
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

    /** Determines the Camera Container z-rotation so that the user position faces the camera
     * @return z-rotation for the camera container
     */
    private float GetCameraContainerRotationWithFrontalUserPos()
    {
        Vector3 direction = VectorCalculationHelper.GetNormalizedDirectionVector(_cameraContainer.transform.localPosition, _userPosition.transform.localPosition);
        float angle = VectorCalculationHelper.GetAngleFromDirectionVector(new Vector2(direction.x, direction.y));
        return angle;
    }


}
