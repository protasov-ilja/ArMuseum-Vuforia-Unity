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
        [SerializeField] private Image _buttonBackground = default;

        [SerializeField] private Color _activeColor = default;
        [SerializeField] private Color _inactiveColor = default;
        [SerializeField] private Button _button = default;
        [SerializeField] private ScreenState _screenState = default;

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