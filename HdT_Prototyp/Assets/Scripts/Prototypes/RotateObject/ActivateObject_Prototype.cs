using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ActivateObject_Prototype : MonoBehaviour
{

    private int nrOfTouches;
    private int nrOfTouchesOnModel;
    private int nrOfTouchesReleased;

    private bool isActive;
    private Camera _camera;

    [SerializeField]
    private GameObject _objectToRotate;

    [SerializeField]
    private float _rotationSpeed = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        nrOfTouches = 0;
        nrOfTouchesOnModel = 0;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) //if a new touch was registered
        {
            nrOfTouches++;
            Ray ray = _camera.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit;

            //if(Physics.Raycast(ray, out hit))
            //{
               //nrOfTouchesOnModel++;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(ray.origin, hit.point);

                    //if(hit.transform.tag == "Model")
                    if (hit.collider.tag == "Model")

                        {
                            var objectScript = hit.collider.GetComponent<RotateObjectController>();
                        objectScript.isActive = true;
                        //isActive = objectScript.isActive;

                        nrOfTouchesOnModel++;
                    }
                }
            //}
        }

       /* if (Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0); //position of where the touch happened

            if (screenTouch.phase == TouchPhase.Began) //if a new touch was registered
            {
                nrOfTouches++;
                Ray ray = Camera.main.ScreenPointToRay(screenTouch.position);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    nrOfTouchesOnModel++;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "Model")
                        {
                            var objectScript = hit.collider.GetComponent<RotateObjectController>();
                            //objectScript.isActive = true;
                            isActive = true;

                            nrOfTouchesOnModel++;
                        }
                    }
                }
            }
        else if (screenTouch.phase == TouchPhase.Ended)
        {
                isActive = false;
                nrOfTouchesReleased++;

            }
        else if (screenTouch.phase == TouchPhase.Moved && isActive) //check if the touch has moved 
        {
                if (_objectToRotate != null)
                {
                    //assign horizontal touch movement to y direction of the gameobject
                    _objectToRotate.transform.Rotate(0f, 0f, screenTouch.deltaPosition.x * _rotationSpeed);
                }
            }

            
        }*/
    }


    void OnGUI()
    {

        GUI.Label(new Rect(200, 150, 400, 100), " Nr of Touches " + nrOfTouches);
        GUI.Label(new Rect(200, 200, 400, 100), " Nr of Touches on Model: " + nrOfTouchesOnModel);
        GUI.Label(new Rect(200, 250, 400, 100), " Nr of Touches released: " + nrOfTouchesReleased);

        GUI.Label(new Rect(200, 300, 400, 100), " Touched Model is Active: " + isActive);


    }


}
