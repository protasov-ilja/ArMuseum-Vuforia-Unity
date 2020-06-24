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
        [SerializeField] private TMP_Text _show3dModelButtonText;
        [ SerializeField] private Button _backButton;
        [SerializeField] private Button _exhibitHistoryButton;
        [SerializeField] private ARExhibitsTrackingSystem _trackingSystem;
        [SerializeField] private GameObject _helpScreen;
        [SerializeField] private TMP_Text _infoPanelText;
        [SerializeField] private GameObject _bottomPanel;
        [SerializeField] private GameObject _scanFocusIcon;
        [SerializeField] private GameObject _blockButtonPanel;


        public bool IsActivated { get; private set; }

        private bool _3DModelActivated = false;

        private bool _has3DModel;
        private ScreenStateManager _screenManger;

        private void CustomStart()
        {
            _screenManger = _dataContainer.ScreenManager;
            DisableExhibitButtons();
        }

        private void Start()
        {
            _helpButton.onClick.AddListener( () => _helpScreen.gameObject.SetActive( true ) );
            _show3dModelButton.onClick.AddListener( Show3DModelClicked );
            _backButton.onClick.AddListener( SetScreen );
            _exhibitHistoryButton.onClick.AddListener( SetScreenInfo );
            _trackingSystem.OnExhibitLost += OnExhibitLost;
            _trackingSystem.OnExhibitRecognized += OnExhibitRecognized;
        }

        private void SetScreen()
        {
            _screenManger.SetScreenState( ScreenState.ExhibitType );
        }

        private void SetScreenInfo()
        {
            _screenManger.SetScreenState( ScreenState.ExhibitInfo );
        }

        private void OnDestroy()
        {
            _helpButton.onClick.RemoveAllListeners();
            _show3dModelButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
            _exhibitHistoryButton.onClick.RemoveAllListeners();

            _trackingSystem.OnExhibitLost -= OnExhibitLost;
            _trackingSystem.OnExhibitRecognized -= OnExhibitRecognized;
        }

        private void Show3DModelClicked()
        {
            // change Button color
            if (!_3DModelActivated)
            {
                _3DModelActivated = true;
                _trackingSystem.Activate3DModel();
            }
            else
            {
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
            _blockButtonPanel.SetActive( !has3DModel );
        }

        public void OnExhibitLost()
        {
            DisableExhibitButtons();
        }

        private void DisableExhibitButtons()
        {
            _scanFocusIcon.gameObject.SetActive(true);
            _infoPanelText.text = "Сканирую...";
            _bottomPanel.gameObject.SetActive(false);
        }

        public void ActivateScreen()
        {
            IsActivated = true;
            gameObject.SetActive(true);
            CustomStart();
        }

        public void DeactivateScreen()
        {
            IsActivated = false;
            gameObject.SetActive(false);
        }
    }
}