using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToRotate;

    [SerializeField]
    private float _rotationSpeed;

    private bool _isActive;

    Color activeColor = new Color();
    GUIStyle guiStyle = new GUIStyle();
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        _isActive = false;
        guiStyle.normal.textColor = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {

            //if there has been touch screen input
            if (Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0); //position of where the touch happened

                if (screenTouch.phase == TouchPhase.Moved) //check if the touch has moved 
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
        counter++;

       /* if (_isActive)
        {
            activeColor = Color.red;
        }
        else
        {
            activeColor = Color.white;
        }
        GetComponent<MeshRenderer>().material.color = activeColor;*/
    }

}
