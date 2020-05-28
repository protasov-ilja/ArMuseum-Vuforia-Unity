using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ARMuseum.ExhibitInfoScreen
{
    public class ExhibitInfoScreenController : MonoBehaviour, IScreenManager
    {
        [Inject] private GlobalDataContainer _dataContainer; 
        
        [SerializeField] private TMP_Text _exhibitName;
        [SerializeField] private TMP_Text _exhibitHallName;
        [SerializeField] private TMP_Text _exhibitInfoText;

        [SerializeField] private Button _createPathButton;
        [SerializeField] private Button _backButton;
        
        private ExhibitDataSO _exhibitData;
        private ScreenStateManager _screenManager;
        
        private void Awake()
        {
            _exhibitData = _dataContainer.GlobalData.SelectedExhibitData;
            _screenManager = _dataContainer.ScreenManager;
        }
        
        private void Start()
        {
            _createPathButton.onClick.AddListener(() => _screenManager.SetScreenState(ScreenState.Navigation));
            _backButton.onClick.AddListener(() => _screenManager.SetScreenState(ScreenState.Scan));
        }
        
        private void OnEnable()
        {
            _exhibitName.text = _exhibitData.ExhibitName;
            _exhibitHallName.text = _exhibitData.Hall.HallName;
            _exhibitInfoText.text = _exhibitData.Description;
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