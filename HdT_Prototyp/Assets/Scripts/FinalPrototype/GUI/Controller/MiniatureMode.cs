using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniatureMode : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private UserPosition _userPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Camera arCamera)
    {
        _userPosition.ArCamera = arCamera;
    }

    public void Show()
    {
        _camera.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _camera.gameObject.SetActive(false);
    }
        
}
