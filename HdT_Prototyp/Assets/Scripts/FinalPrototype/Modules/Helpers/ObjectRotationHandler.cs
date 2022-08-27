using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** @class ObjectRotationHandler This class registers left/right swipe gestures on a gameObject and makes a designated gameObject rotate accordingly (on the z-axis)
 */
public class ObjectRotationHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToRotate;

    [SerializeField]
    private float _rotationSpeed;

    private bool _isActive;


    void Start()
    {
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            if (Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0); 

                if (screenTouch.phase == TouchPhase.Moved)
                {
                    if (_objectToRotate != null)
                    {
                        //assign horizontal touch movement to y direction of the gameobject
                        _objectToRotate.transform.Rotate(0f, 0f, screenTouch.deltaPosition.x * _rotationSpeed);
                    }
                }

                if (screenTouch.phase == TouchPhase.Ended)
                {
                    _isActive = false;
                }
            }
        }
    }

    public void Activate()
    {
        _isActive = true;
    }

}
