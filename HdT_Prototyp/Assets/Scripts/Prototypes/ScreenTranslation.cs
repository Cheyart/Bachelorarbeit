using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTranslation : MonoBehaviour
{
    public GameObject _gameobjectInWorld;
    //public RectTransform _gameobjectOnScreen;
    public Camera _mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenPos = _mainCamera.WorldToScreenPoint(_gameobjectInWorld.transform.position);
        transform.localPosition = screenPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
