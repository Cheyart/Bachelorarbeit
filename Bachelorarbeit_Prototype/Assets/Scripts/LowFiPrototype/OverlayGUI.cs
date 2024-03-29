using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowFiPrototype
{
    public class OverlayGUI : MonoBehaviour
    {

        private ViewTransitionController _transitionController;

        [SerializeField]
        private GameObject _arGUI;

        [SerializeField]
        private GameObject _vrGUI;


        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SwitchToVrGUI()
        {
            _arGUI.gameObject.SetActive(false);
            _vrGUI.gameObject.SetActive(true);
        }

        public void SwitchToArGUI()
        {
            _arGUI.gameObject.SetActive(true);
            _vrGUI.gameObject.SetActive(false);
        }

        public void HideBothViews()
        {
            _arGUI.gameObject.SetActive(false);
            _vrGUI.gameObject.SetActive(false);
        }

    }
}