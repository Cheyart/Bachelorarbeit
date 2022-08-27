using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class Billboard This class makes the object that it's attached to face towards a designated camera
 */
public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Camera _camera; /** Camera which the billboard will be facing towards */
    public Camera Camera { get => _camera; set => _camera = value; }

    [SerializeField]
    private bool _fixXAxis; /** true if the billboard should be fixed on the x Axis*/

    [SerializeField]
    private bool _fixYAxis; /** true if the billboard should be fixed on the y Axis*/

    [SerializeField]
    private bool _fixZAxis; /** true if the billboard should be fixed on the z Axis*/

    [SerializeField]
    private float _xOffset; /** Offset value for the x Axis*/

    [SerializeField]
    private float _yOffset; /** Offset value for the y Axis*/

    [SerializeField]
    private float _zOffset; /** Offset value for the z Axis*/

    private Vector3 _multiplicationVector; /** multiplication Vector used to restrict roation to specific axis */

    void Start()
    {
        float x = 0;
        float y = 0;
        float z = 0;

        if (!_fixXAxis)
        {
            x = 1;
        }
        if (!_fixYAxis)
        {
            y = 1;
        }
        if (!_fixZAxis)
        {
            z = 1;
        }
        _multiplicationVector = new Vector3(x, y, z);

    }

    void LateUpdate()
    {
        if (_camera != null)
        {
            SetRotationToFaceCamera();
        }

    }

    /**
     * Sets the Rotation of the Billboard to face the specified camera
     */
    private void SetRotationToFaceCamera()
    {
        transform.LookAt(_camera.transform, _camera.transform.up);

        transform.localRotation = Quaternion.Euler(Vector3.Scale(transform.localRotation.eulerAngles, _multiplicationVector));
        Vector3 currentRot = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(currentRot.x + _xOffset, currentRot.y + _yOffset, currentRot.z + _zOffset);
    }
}
