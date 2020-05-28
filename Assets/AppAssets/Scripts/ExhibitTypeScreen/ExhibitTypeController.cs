using System;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.ExhibitTypeScreen.Enums;
using ARMuseum.Scriptables;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ARMuseum.ExhibitTypeScreen
{
    public class ExhibitTypeController : MonoBehaviour, IScreenManager
    {
        [Inject] private GlobalDataContainer _dataContainer;

        [SerializeField] private Button _paintButton;
        [SerializeField] private Button _sculptureButton;

        [SerializeField] private Button _backToMuseumsButton;
        [SerializeField] private Button _helpScreenButton;

        [SerializeField] private GameObject _helpScreen;

        private GlobalDataSO _globalData;
        private ScreenStateManager _screenManager;
        
        public bool IsActivated { get; private set; }

        private void Start()
        {
            _globalData = _dataContainer.GlobalData;
            _screenManager = _dataContainer.ScreenManager;
            
            _paintButton.onClick.AddListener(OnSelectPaint);
            _sculptureButton.onClick.AddListener(OnSelectSculpture);
            _backToMuseumsButton.onClick.AddListener(OnBackButtonClicked);
            _helpScreenButton.onClick.AddListener(OnHelpClicked);
        }

        private void OnDestroy()
        {
            _paintButton.onClick.RemoveListener(OnSelectPaint);
            _sculptureButton.onClick.RemoveListener(OnSelectSculpture);
            _backToMuseumsButton.onClick.RemoveListener(OnBackButtonClicked);
            _helpScreenButton.onClick.RemoveListener(OnHelpClicked);
        }

        private void OnSelectSculpture()
        {
            _globalData.SelectedExhibitType = ExhibitType.Sculpture;
            _screenManager.SetScreenState(ScreenState.Scan);
        }
        
        private void OnSelectPaint()
        {
            _globalData.SelectedExhibitType = ExhibitType.Paint;
            _screenManager.SetScreenState(ScreenState.Scan);
        }

        private void OnBackButtonClicked()
        {
            _screenManager.SetScreenState(ScreenState.Museums);
        }

        private void OnHelpClicked()
        {
            _helpScreen.gameObject.SetActive(true);
        }

        public void ActivateScreen()
        {
            IsActivated = true;
            gameObject.SetActive(true);
        }

        public void DeactivateScreen()
        {
            IsActivated = false;
            gameObject.SetActive(false);
        }
    }
}