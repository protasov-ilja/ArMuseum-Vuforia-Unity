using System;
using AppAssets.Scripts.UI.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AppAssets.Scripts.UI.BottomNavMenu
{
    public class NavButton : MonoBehaviour
    {
        [SerializeField] private Image _buttonIcon;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private Image _buttonBackground;

        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Button _button;
        [SerializeField] private ScreenState _screenState;

        private Action<ScreenState> OnScreenSelected;
        
        public bool IsActive { get; private set; }
        public ScreenState ScreenState => _screenState;

        private void Start()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }
        
        public void Initialize(BottomNavMenuController controller)
        {
            OnScreenSelected += controller.OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            if (IsActive) return;
            
            IsActive = true;
            _buttonBackground.color = Color.green;
            
            OnScreenSelected?.Invoke(_screenState);
        }

        public void ActivateButton(bool isActive)
        {
            if (isActive == IsActive) return;

            _buttonBackground.color = isActive ? Color.green : Color.gray;

            IsActive = isActive;
        }
    }
}