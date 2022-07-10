using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (POIMenuContentSetup))]
public class POIMenuController : MonoBehaviour
{
    [SerializeField]
    private POIMenuContentSetup _contentSetup;

    //add state (small, medium, big)
    //add animated transition


    // Start is called before the first frame update
    void Start()
    {
        _contentSetup = GetComponent<POIMenuContentSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(PointOfInterest content)
    {
       _contentSetup.Setup(content);
        gameObject.SetActive(true);  //TO DO -> make transition animation (appear from bottom)
    }

    public void Hide()
    {
        gameObject.SetActive(false); //TO DO -> make disappear animation (disappear to bottom)
    }

}
