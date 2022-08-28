using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class MiniatureCamera This class controls the behaviour of the Miniature Camera
 */
[RequireComponent(typeof(AnimatedTransform), typeof(Camera), typeof(AnimatedCamera))]
public class MiniatureCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _thisCamera; /** Camera active in Miniature Mode*/
    public Camera Camera { get => _thisCamera; }

    private Camera _arCamera; /** Camera active in AR Mode*/

    private AnimatedTransform _animator; /** Animator responsible for animations of Transform component*/
    private AnimatedCamera _cameraAnimator; /** Animator responsible for animations of Camera component*/

    private Vector3 _startPosition; /** Position at start of the session */
    private Quaternion _startRotation;  /** Rotation at start of the session */
    private float _startFOV;  /** FOV at start of the session */

    void Start()
    {
        SetComponents();
    }

    /**
     * Sets up the camera 
     */
    public void Setup(Camera arCamera, Vector3 startPosition, Quaternion startRotation, float startFOV)
    {
        SetComponents();
        _startPosition = startPosition;
        _startRotation = startRotation;
        _startFOV = startFOV;
        _arCamera = arCamera;
    }

    /** Alligns the Miniature Camera with the AR Camera (position, rotation and FOV)
     */
    public void AlignWithARCamera()
    {
        transform.position = _arCamera.transform.position;
        transform.rotation = _arCamera.transform.rotation;
        _thisCamera.fieldOfView = _arCamera.fieldOfView;
    }

    /** Executes the camera transition from AR to Miniature mode
     * @param duration Duration of the transition
     */
    public IEnumerator TransitionToMiniatureMode(float duration)
    {
        StartCoroutine(_animator.LerpPosition(transform.localPosition, _startPosition, duration, Space.Self, EasingFunction.easeIn));
        StartCoroutine(_animator.LerpRotation(transform.localRotation, _startRotation, duration, Space.Self, EasingFunction.easeIn));
        yield return StartCoroutine(_cameraAnimator.LerpFOV(_thisCamera.fieldOfView, _startFOV, duration, EasingFunction.easeIn));

    }

    /** Executes the camera transition from Miniature to AR mode
    * @param duration Duration of the transition
    */
    public IEnumerator TransitionFromMiniatureMode(float duration)
    {
        StartCoroutine(_animator.LerpPosition(transform.position, _arCamera.transform.position, duration, Space.World, EasingFunction.easeInAndOut));
        StartCoroutine(_animator.LerpRotation(transform.rotation, _arCamera.transform.rotation, duration, Space.World, EasingFunction.easeInAndOut));
        yield return StartCoroutine(_cameraAnimator.LerpFOV(_thisCamera.fieldOfView, _arCamera.fieldOfView, duration, EasingFunction.easeInAndOut));
    }

   /** Sets the references needed in this class
    */
    private void SetComponents()
    {
        _animator = GetComponent<AnimatedTransform>();
        _thisCamera = GetComponent<Camera>();
        _cameraAnimator = GetComponent<AnimatedCamera>();
    }

}
