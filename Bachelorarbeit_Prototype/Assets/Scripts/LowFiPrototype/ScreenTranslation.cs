using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowFiPrototype
{
    public class ScreenTranslation : MonoBehaviour
    {
        public GameObject _gameobjectInWorld;
        public Camera _mainCamera;


        // Start is called before the first frame update
        void Start()
        {
            Vector3 screenPos = _mainCamera.WorldToScreenPoint(_gameobjectInWorld.transform.position);
            transform.localPosition = screenPos;
        }

    }
}
