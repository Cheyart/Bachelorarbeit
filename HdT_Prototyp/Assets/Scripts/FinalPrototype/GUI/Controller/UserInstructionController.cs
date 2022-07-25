using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Instructions
{
    scanQRCode, clickPOI, switchMode, rotateModel, adjustMenuSize, switchView
}
public class UserInstructionController : MonoBehaviour
{
    [SerializeField]
    private GameObject _startScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInstruction(Instructions instructions)
    {
        switch (instructions)
        {
            case Instructions.scanQRCode:
                _startScreen.SetActive(true);
                break;

        }
    }

    public void HideInstruction(Instructions instructions)
    {
        switch (instructions)
        {
            case Instructions.scanQRCode:
                _startScreen.SetActive(false);
                break;
               
        }
    }
}
