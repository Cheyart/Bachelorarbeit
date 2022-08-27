using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Preparation
{
    public class TextInputManager : MonoBehaviour
    {

        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private TextMeshProUGUI _displayText;

        private TouchScreenKeyboard mobileKeys;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return)) //doesnt work for mobile
            {
                DisplayInputText();
            }

            if (mobileKeys != null && mobileKeys.status == TouchScreenKeyboard.Status.Done)
            {
                DisplayInputText();
            }
        }

        public void DisplayInputText()
        {
            _displayText.text = _inputField.text;
        }

        public void ResetInputText()
        {
            _inputField.text = "";
        }

        public void AssignMobileKeys()
        {
            mobileKeys = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }

        public void EnlargeTextField()
        {

        }


        public void Test()
        {
            Debug.Log("Test Ausgabe");
        }


    }
}