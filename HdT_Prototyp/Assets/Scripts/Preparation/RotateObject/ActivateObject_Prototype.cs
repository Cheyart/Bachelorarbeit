using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Preparation
{
    [RequireComponent(typeof(Camera))]
    public class ActivateObject_Prototype : MonoBehaviour
    {

        private int nrOfTouches;
        private int nrOfTouchesOnModel;
        private int nrOfTouchesOnPOI;

        private int nrOfTouchesReleased;

        private bool isActive;
        private Camera _camera;

        [SerializeField]
        private GameObject _objectToRotate;

        [SerializeField]
        private POISelectionController _poiSelectionController;

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
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) //if a new touch was registered
            {
                int hitPoints = 1;
                nrOfTouches++;
                Ray ray = _camera.ScreenPointToRay(Input.touches[0].position);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "POI")
                    {
                        if (hitPoints == 1)
                        {

                            nrOfTouchesOnPOI += 5;
                            var selectedPOI = hit.collider.GetComponent<PointOfInterest>();
                            nrOfTouchesOnPOI += 3;
                            _poiSelectionController.switchSelectedPOI(selectedPOI.Id);
                            nrOfTouchesOnPOI++;
                            hitPoints--;
                            //objectScript.IsSelected = true;
                            //if(hit.transform.tag == "Model")
                        }
                    }

                    if (hit.collider.tag == "Model")
                    {
                        if (hitPoints == 1)
                        {
                            var objectScript = hit.collider.GetComponent<RotateObjectController>();
                            objectScript.isActive = true;
                            isActive = objectScript.isActive;

                            nrOfTouchesOnModel++;
                            hitPoints--;

                        }
                    }

                }

            }
        }




        /*void OnGUI()
        {

            GUI.Label(new Rect(200, 150, 400, 100), " Nr of Touches " + nrOfTouches);
            GUI.Label(new Rect(200, 200, 400, 100), " Nr of Touches on Model: " + nrOfTouchesOnModel);
            GUI.Label(new Rect(200, 500, 400, 100), " Nr of Touches on POI: " + nrOfTouchesOnPOI);

            GUI.Label(new Rect(200, 300, 400, 100), " Touched Model is Active: " + isActive);


        }*/


    }
}
