using System;
using System.Collections.Generic;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.Utils;
using Zenject;

namespace ARMuseum.ChooseMuseumScreen
{
    public class ScreenStateManager
    {
        [Inject] private GenericSceneManager _sceneManager; 
        
        public ScreenState CurrentScreenState { get; private set; }

        private readonly Dictionary<ScreenState, IScreenManager> _screenBindings;

        public Action<ScreenState> OnScreenStateChanged;

        private Dictionary<ScreenState, string> _sceneBindings = new Dictionary<ScreenState, string>
        {
            { ScreenState.Navigation, "ARNavigationScene" },
            { ScreenState.Scan, "ObjectsScanScene" },
            { ScreenState.ExhibitType, "MainScene" }
        };
        
        public ScreenStateManager(Dictionary<ScreenState, IScreenManager> screenBindings)
        {
            _screenBindings = screenBindings;
            CurrentScreenState = ScreenState.Museums;
            OnScreenStateChanged?.Invoke(ScreenState.Museums);
        }
        
        public void Subscribed(IStateChangingListener data)
        {
        }
        
        public void SetScreenState(ScreenState newState)
        {
            if ( CurrentScreenState == ScreenState.Museums && newState != ScreenState.ExhibitType )
            {
                throw new Exception($"wrong State currState: { CurrentScreenState } newState { newState }");
            }
            
            ChangeActiveScreen(newState);
        }

        private void ChangeActiveScreen(ScreenState newState)
        {
            if (CurrentScreenState == newState)
            {
                if (!_screenBindings[newState].IsActivated) _screenBindings[newState].ActivateScreen();
                return;
            }
            
            if (_screenBindings[CurrentScreenState] != null)
            {
                _screenBindings[CurrentScreenState].DeactivateScreen();
            }
            
            if (_screenBindings[newState] != null)
            {
                _screenBindings[newState].ActivateScreen();
            }

            CurrentScreenState = newState;
            OnScreenStateChanged?.Invoke(CurrentScreenState);
        }

        public void LoadCurrentScreenState()
        {
            ChangeActiveScreen(CurrentScreenState);
        }
    }
}