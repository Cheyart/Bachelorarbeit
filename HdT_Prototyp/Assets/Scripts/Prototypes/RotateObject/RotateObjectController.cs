using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 0.4f;

    public bool isActive = false;
    Color activeColor = new Color();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            activeColor = Color.red;

            //if there has been touch screen input
            if(Input.touchCount == 1)
            {
                Touch screenTouch = Input.GetTouch(0); //position of where the touch happened

                if(screenTouch.phase == TouchPhase.Moved) //check if the touch has moved 
                {
                    //assign horizontal touch movement to y direction of the gameobject
                    transform.Rotate(0f, -screenTouch.deltaPosition.x * _rotationSpeed, 0f);
                }

                if(screenTouch.phase == TouchPhase.Ended)
                {
                    isActive = false;
                }
            }
        }
        else
        {
            activeColor = Color.white;
        }

        GetComponent<MeshRenderer>().material.color = activeColor;
    }
}
