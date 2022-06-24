using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 0.4f;

    [SerializeField]
    private GameObject _objectToRotate;

    public bool isActive;
    Color activeColor = new Color();

    private int _touchPhaseEnded;

   

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
                    if(_objectToRotate != null)
                    {
                        //assign horizontal touch movement to y direction of the gameobject
                        _objectToRotate.transform.Rotate(0f, 0f, screenTouch.deltaPosition.x * _rotationSpeed);
                    }           
                }

                if(screenTouch.phase == TouchPhase.Ended)
                {
                    isActive = false;
                    _touchPhaseEnded++;
                }
            }
       }
       else
       {
           activeColor = Color.white;
       }

        GetComponent<MeshRenderer>().material.color = activeColor;
    }

    public void ClickTest()
    {
        Debug.Log("Click was detected");
    }

    void OnGUI()
    {
        GUI.Label(new Rect(200, 500, 400, 100), " Camera Rotation " + _objectToRotate.transform.rotation);
        GUI.Label(new Rect(200, 550, 400, 100), " Model Is Active: " + isActive);
        GUI.Label(new Rect(200, 600, 400, 100), " Touch Phase ended: " + _touchPhaseEnded);
    }
}
