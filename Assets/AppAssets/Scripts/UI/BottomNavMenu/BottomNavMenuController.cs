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
        [Inject] private GlobalDataContainer _dataContainer;
        
        [SerializeField] private List<NavButton> _menuButtons = default;
        [SerializeField] private ScreenState _defaultActiveState = default;
        [SerializeField] private Color _activeColor = default;
        [SerializeField] private Color _inactiveColor = default;

        private ScreenStateManager _screenManager;

        private void Start()
        {
            _screenManager = _dataContainer.ScreenManager;
            foreach (var navButton in _menuButtons)
            {
                navButton.Initialize(this, _activeColor, _inactiveColor );
                if (navButton.ScreenState == _defaultActiveState )
                {
                    navButton.ActiveForce( true );
                }
                else
                {
                    navButton.ActiveForce( false );
                }
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