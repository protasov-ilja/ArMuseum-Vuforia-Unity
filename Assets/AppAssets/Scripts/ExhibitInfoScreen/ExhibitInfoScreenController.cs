using System;
using System.Collections.Generic;
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

        [SerializeField] private ImageSliderController _imageSlider;
        
        private ExhibitDataSO _exhibitData;
        private ScreenStateManager _screenManager;

        public bool IsActivated { get; private set; }

        private void Start()
        {
            _backButton.onClick.AddListener(BackToSearch);
        }

        private void CustomStart()
        {
            _exhibitData = _dataContainer.GlobalData.SelectedExhibitData;
            _screenManager = _dataContainer.ScreenManager;
            
            _createPathButton.onClick.AddListener(() => _screenManager.SetScreenState(ScreenState.Navigation));

            _exhibitName.text = _exhibitData.ExhibitName;
            _exhibitHallName.text = _exhibitData.Hall.HallName;
            _exhibitInfoText.text = _exhibitData.Description;

            _imageSlider.Initialize();
        }

        private void BackToSearch()
        {
            _dataContainer.GlobalData.SelectedExhibitData = null;
            _screenManager.SetScreenState(ScreenState.Search);
        }

        private void OnDisable()
        {
            _createPathButton.onClick.RemoveAllListeners();
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