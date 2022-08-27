using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class Billboard This class makes the object that it's attached to face towards a designated camera
 */
public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    public Camera Camera { get => _camera; set => _camera = value; }

    [SerializeField]
    private bool _fixXAxis;

    [SerializeField]
    private bool _fixYAxis;

    [SerializeField]
    private bool _fixZAxis;

    [SerializeField]
    private float _xOffset;

    [SerializeField]
    private float _yOffset;

    [SerializeField]
    private float _zOffset;

    private Vector3 _multiplicationVector;

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
            transform.LookAt(_camera.transform, _camera.transform.up);

            transform.localRotation = Quaternion.Euler(Vector3.Scale(transform.localRotation.eulerAngles, _multiplicationVector));
            Vector3 currentRot = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(currentRot.x + _xOffset, currentRot.y + _yOffset, currentRot.z + _zOffset);
        }

    }
}
