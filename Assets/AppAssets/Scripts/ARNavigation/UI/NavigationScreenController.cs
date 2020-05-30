using System.Security;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace AppAssets.Scripts.ARNavigation
{
    public class NavigationScreenController : MonoBehaviour, IScreenManager
    {
        //[Inject] private GlobalDataContainer _dataContainer;
        
        [SerializeField] private Button _backToMuseumButton;
        [SerializeField] private Button _mapButton;

        [SerializeField] private GameObject _mapScreen;
        [SerializeField] private Button _backToNavigationButton;
        [SerializeField] private Button _confirmNavigationButton;

        [SerializeField] private TMP_Text _destinationNameText;

        [FormerlySerializedAs("_navigationSystem")] [SerializeField] private ARNavigationSystem arNavigationSystem;
        
        private ScreenStateManager _screenManager;
        private GlobalDataSO _globalData;
        
        private void Start()
        {
            //_globalData = _dataContainer.GlobalData;
            //_screenManager = _dataContainer.ScreenManager;

            //_backToMuseumButton.onClick.AddListener(() => _screenManager.SetScreenState(ScreenState.Museums));
            _mapButton.onClick.AddListener(() => _mapScreen.gameObject.SetActive(true));
            _backToNavigationButton.onClick.AddListener(() => _mapScreen.gameObject.SetActive(false));
            _confirmNavigationButton.onClick.AddListener(ConfirmNavigation);
        }

        private void ConfirmNavigation()
        {
            _mapScreen.gameObject.SetActive(false);
        }

        public bool IsActivated { get; private set; }
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