using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Instructions
{
    scanQRCode, clickPOI, switchMode, rotateModel, adjustMenuSize, switchView
}
public class UserInstructionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _startScreen;

    private Dictionary<Instructions, InstructionScreen> _instructionScreens;

    private InstructionScreen _currentInstructionScreen;


    // Start is called before the first frame update
    void Start()
    {
        SetInstructionScreens();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInstruction(Instructions instruction)
    {
        //Debug.Log("Show Instruction: " + instruction);
        InstructionScreen screen;
        if(_instructionScreens == null)
        {
            SetInstructionScreens();
        }

        if (_instructionScreens.TryGetValue(instruction, out screen))
        {
            if (!screen.HasBeenShowen)
            {
                screen.Show();
                _currentInstructionScreen = screen;
            }
        }

    }

    public void HideInstruction(Instructions instructions)
    {
        if(_currentInstructionScreen != null)
        {
            _currentInstructionScreen.Hide();

        }
    }

    private void SetInstructionScreens()
    {
        InstructionScreen[] instructionScreens = FindObjectsOfType<InstructionScreen>(true);
        Debug.Log("Instruction Screens length = " + instructionScreens.Length);

        _instructionScreens = new Dictionary<Instructions, InstructionScreen>();
        foreach (InstructionScreen screen in instructionScreens)
        {
            screen.Init();
            _instructionScreens.Add(screen.Instruction, screen);
        }
    }
}
