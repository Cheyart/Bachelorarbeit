using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowFiPrototype
{
    [RequireComponent(typeof(Camera))]
    public class ActivateObject_Prototype : MonoBehaviour
    {


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

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) //if a new touch was registered
            {
                int hitPoints = 1;

                Ray ray = _camera.ScreenPointToRay(Input.touches[0].position);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "POI")
                    {
                        if (hitPoints == 1)
                        {


                            var selectedPOI = hit.collider.GetComponent<PointOfInterest>();

                            _poiSelectionController.switchSelectedPOI(selectedPOI.Id);

                            hitPoints--;

                        }
                    }

                    if (hit.collider.tag == "Model")
                    {
                        if (hitPoints == 1)
                        {
                            var objectScript = hit.collider.GetComponent<RotateObjectController>();
                            objectScript.isActive = true;

                        }
                    }

                }

            }
        }




    }
}
