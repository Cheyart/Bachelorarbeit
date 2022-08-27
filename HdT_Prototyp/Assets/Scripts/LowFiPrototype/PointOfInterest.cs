using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowFiPrototype
{
    public class PointOfInterest : MonoBehaviour
    {
        public int Id { get => _id; set => _id = value; }
        [SerializeField]
        private int _id;


        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;


                if (_isSelected)
                {

                    GetComponent<MeshRenderer>().material.color = Color.green;
                }
                else
                {

                    GetComponent<MeshRenderer>().material.color = Color.blue;
                }
            }
        }


        private bool _isSelected;

        // Start is called before the first frame update
        void Start()
        {
            _isSelected = false;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
