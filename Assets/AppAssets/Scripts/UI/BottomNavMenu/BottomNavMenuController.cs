using System;
using System.Collections.Generic;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace AppAssets.Scripts.UI.BottomNavMenu
{
    public class BottomNavMenuController : SerializedMonoBehaviour
    {
        [SerializeField] private List<NavButton> _menuButtons;
        
        [Inject] private GlobalDataContainer _dataContainer;

        private ScreenStateManager _screenManager;
        
        private void Awake()
        {
            _screenManager = _dataContainer.ScreenManager;
        }

        private void Start()
        {
            foreach (var navButton in _menuButtons)
            {
                navButton.Initialize(this);
            }
        }

        public void OnButtonClicked(ScreenState selectedState)
        {
            foreach (var navButton in _menuButtons)
            {
                if (navButton.ScreenState == selectedState) continue;

                if (navButton.IsActive)
                {
                    navButton.ActivateButton(false);
                }
            }

            if (_screenManager == null)
            {
                _screenManager = _dataContainer.ScreenManager;
            }
            
            _screenManager.SetScreenState(selectedState);
        }
    }
}