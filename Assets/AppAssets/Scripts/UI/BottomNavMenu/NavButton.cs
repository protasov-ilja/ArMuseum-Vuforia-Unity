using System;
using AppAssets.Scripts.UI.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AppAssets.Scripts.UI.BottomNavMenu
{
    public class NavButton : MonoBehaviour
    {
        [SerializeField] private Image _buttonIcon = default;
        [SerializeField] private TMP_Text _buttonText = default;

        [SerializeField] private Button _button = default;
        [SerializeField] private ScreenState _screenState = default;

        private Action<ScreenState> OnScreenSelected;
        private Color _activeColor;
        private Color _inactiveColor;

        public bool IsActive { get; private set; }
        public ScreenState ScreenState => _screenState;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }
        
        public void Initialize(BottomNavMenuController controller, Color activeColor, Color inactiveColor)
        {
            _activeColor = activeColor;
            _inactiveColor = inactiveColor;
            OnScreenSelected += controller.OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            if (IsActive) return;
            
            IsActive = true;
            _buttonIcon.color = _activeColor;
            _buttonText.color = _activeColor;
            
            OnScreenSelected?.Invoke(_screenState);
        }

        public void ActivateButton(bool isActive)
        {
            if (isActive == IsActive) return;

            _buttonIcon.color = isActive ? _activeColor : _inactiveColor;
            _buttonText.color = isActive ? _activeColor : _inactiveColor;

            IsActive = isActive;
        }

        public void ActiveForce( bool isActive )
        {
            _buttonIcon.color = isActive ? _activeColor : _inactiveColor;
            _buttonText.color = isActive ? _activeColor : _inactiveColor;

            IsActive = isActive;
        }
    }
}