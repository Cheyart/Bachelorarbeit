using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScreen : MonoBehaviour
{
    [SerializeField]
    private Instructions _instruction;
    public Instructions Instruction { get => _instruction; }

    private bool _hasBeenShown;
    public bool HasBeenShowen { get => _hasBeenShown; set => _hasBeenShown = value; }

    private UserInstructionController _instructionManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(bool showScreen, UserInstructionController instructionManager)
    {
        _instructionManager = instructionManager;
        gameObject.SetActive(showScreen);
    }

    public void SetVisibility(bool value)
    {
        gameObject.SetActive(value);
    }


    public void Hide()
    {
        _hasBeenShown = true;
        //gameObject.SetActive(false);
        _instructionManager.HideInstructionScreen();
    }

   /* private void OnGUI()
    {
        GUI.Label(new Rect(200, 500, 400, 200), "Current instruction screen" + _instructionManager.CurrentInstructionScreen.Instruction);


    }*/
}
