using System;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ARMuseum.ARImageTracking
{
    public class ScanScreenController : MonoBehaviour, IScreenManager
    {
        [Inject] private GlobalDataContainer _dataContainer;
        
        [SerializeField] private Button _helpButton;
        [SerializeField] private Button _show3dModelButton;
        [SerializeField] private Image _show3DButtonImage;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exhibitHistoryButton;
        [SerializeField] private ARExhibitsTrackingSystem _trackingSystem;
        [SerializeField] private GameObject _helpScreen;
        [SerializeField] private TMP_Text _infoPanelText;
        [SerializeField] private GameObject _bottomPanel;
        [SerializeField] private GameObject _scanFocusIcon;
        
        public bool IsActivated { get; private set; }

        private bool _3DModelActivated = false;

        private bool _has3DModel;

        private void Start()
        {
            var screenManger = _dataContainer.ScreenManager;
            
            _trackingSystem.OnExhibitLost += OnExhibitLost;
            _trackingSystem.OnExhibitRecognized += OnExhibitRecognized;
            
            _helpButton.onClick.AddListener(() => _helpScreen.gameObject.SetActive(true));
            _show3dModelButton.onClick.AddListener(Show3DModelClicked);
            _backButton.onClick.AddListener(() => screenManger.SetScreenState(ScreenState.ExhibitType));
            _exhibitHistoryButton.onClick.AddListener(() => screenManger.SetScreenState(ScreenState.ExhibitInfo));
        }

        private void OnDestroy()
        {
            _helpButton.onClick.RemoveAllListeners();
            _show3dModelButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _exhibitHistoryButton.onClick.RemoveAllListeners();
        }

        private void Show3DModelClicked()
        {
            // change Button color
            if (!_3DModelActivated)
            {
                _show3DButtonImage.color = Color.green;
                _3DModelActivated = true;
                _trackingSystem.Activate3DModel();
            }
            else
            {
                _show3DButtonImage.color = Color.gray;
                _3DModelActivated = false;
                _trackingSystem.Deactivate3DModel();
            }
        }

        public void OnExhibitRecognized(string exhibitName, bool has3DModel)
        {
            _scanFocusIcon.gameObject.SetActive(false);
            _bottomPanel.gameObject.SetActive(true);
            _infoPanelText.text = exhibitName;
            _show3dModelButton.interactable = has3DModel;
        }

        public void OnExhibitLost()
        {
            _scanFocusIcon.gameObject.SetActive(true);
            _infoPanelText.text = "Сканирую...";
            _bottomPanel.gameObject.SetActive(false);
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