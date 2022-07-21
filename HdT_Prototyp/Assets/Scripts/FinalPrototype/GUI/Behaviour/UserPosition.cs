using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPosition : MonoBehaviour
{
    private Camera _arCamera;
    public Camera ArCamera { get => _arCamera; set => _arCamera = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(_arCamera != null)
        {
            transform.position = _arCamera.transform.position;
        }
    }
}
