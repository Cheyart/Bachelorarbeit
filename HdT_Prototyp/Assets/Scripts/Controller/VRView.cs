using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VRView : MonoBehaviour
{
    public GameObject WIMScene { get => _WIMScene; set => _WIMScene = value; }
    [SerializeField]
    private GameObject _WIMScene;

    [SerializeField]
    private PlayerPosition _playerPosition;

    public bool SceneIsSetup { get => _sceneIsSetup; set => _sceneIsSetup = value; }
    private bool _sceneIsSetup;

    private Vector3 _scenePosition;
    private Quaternion _sceneRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DELETE?
        _WIMScene.transform.position = _scenePosition;
        _WIMScene.transform.rotation = _sceneRotation;
    }

    public void Setup(Vector3 position, Quaternion rotation, Camera arCamera)
    {
        _WIMScene.transform.position = position;
        _WIMScene.transform.rotation = rotation;

        _scenePosition = position;
        _sceneRotation = rotation;

        if(_playerPosition != null)
        {
            _playerPosition.ARCamera = arCamera;
            _playerPosition.IsActive = true;
        }
        _sceneIsSetup = true;
    }

    public void Show()
    {
        //_WIMScene.gameObject.SetActive(true);
        //_playerPosition.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
