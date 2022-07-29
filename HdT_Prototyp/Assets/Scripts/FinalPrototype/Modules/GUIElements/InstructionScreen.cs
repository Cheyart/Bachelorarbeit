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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _hasBeenShown = true;
        gameObject.SetActive(false);
    }
}
