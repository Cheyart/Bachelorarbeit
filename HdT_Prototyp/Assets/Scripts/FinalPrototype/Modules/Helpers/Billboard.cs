using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
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

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(_camera.transform, _camera.transform.up);

       
        
        transform.localRotation = Quaternion.Euler(Vector3.Scale(transform.localRotation.eulerAngles, _multiplicationVector));
        Vector3 currentRot = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(currentRot.x + _xOffset, currentRot.y + _yOffset, currentRot.z + _zOffset);
        
    }
}
