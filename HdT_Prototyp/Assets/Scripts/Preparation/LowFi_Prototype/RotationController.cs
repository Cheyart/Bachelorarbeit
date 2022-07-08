using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Preparation
{
    public class RotationController : MonoBehaviour
{

    private int nrOfTouches;
    private int nrOfTouchesOnModel;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        nrOfTouches = 0;
        nrOfTouchesOnModel = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) //if a new touch was registered
        {
            nrOfTouches++;
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                nrOfTouchesOnModel++;

                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.tag == "Model")
                    {
                        var objectScript = hit.collider.GetComponent<RotateObjectController>();
                        objectScript.isActive = true;
                        isActive = objectScript.isActive;

                        nrOfTouchesOnModel++;
                    }
                }
            }
        }
    }


    void OnGUI()
    {

        GUI.Label(new Rect(200, 150, 400, 100), " Nr of Touches " + nrOfTouches);
        GUI.Label(new Rect(200, 200, 400, 100), " Nr of Touches on Model: " + nrOfTouchesOnModel);
        GUI.Label(new Rect(200, 250, 400, 100), " Touched Model is Active: " + isActive);


    }


}
}