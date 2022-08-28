using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowFiPrototype
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
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) //if a new touch was registered
            {
                nrOfTouches++;
                Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    nrOfTouchesOnModel++;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.tag == "Model")
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

    }
}