using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;



public enum Views
{
    arView, vrView
}
public class ViewTransitionController : MonoBehaviour
{
    public Views CurrentView { get => _currentView; set => _currentView = value;}
    [SerializeField]
    private Views _currentView = Views.arView;

    [SerializeField]
    private ARView _arView;

    [SerializeField]
    private VRView _vrView;

    [SerializeField]
    private OverlayGUI _overlayGUI;

    private int _setupTest;

   

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateSession()
    {
        _overlayGUI.Show();
    }

    private void setUpWIM()
    {

    }

    public void SwitchToVR()
    {
        if (!_vrView.SceneIsSetup)
        {
            //_setupTest++;

            if (_arView.TrackedImage.transform.position != null && _arView.TrackedImage.transform.rotation != null)
            {
                //_setupTest++;
                _vrView.Setup(_arView.TrackedImage.transform.position, _arView.TrackedImage.transform.rotation, _arView.ArCamera);
                _setupTest++;
            }
            else
            {
                return;
            }
        }

        _setupTest += 2;
        _vrView.Show();
        //_arView.gameObject.SetActive(false);
        _overlayGUI.SwitchToVrGUI();
        _currentView = Views.vrView;
    }

    public void SwitchToAR()
    {
        _vrView.Hide();
        //_arView.gameObject.SetActive(true);
        _overlayGUI.SwitchToArGUI();
        _currentView = Views.arView;

    }

    /*void OnGUI()
    {
            GUI.Label(new Rect(200, 250, 400, 100), " Setup = " + _setupTest);
            GUI.Label(new Rect(200, 300, 400, 100), " Tracked image position = " + _arView.TrackedImage.transform.position);
            GUI.Label(new Rect(200, 350, 400, 100), " Tracked Image rotation = " + _arView.TrackedImage.transform.rotation);
            GUI.Label(new Rect(200, 400, 400, 100), " ARCamera position = " + _arView.ArCamera.transform.position);

    }*/
}
