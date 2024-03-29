﻿using System;
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
        
        [SerializeField] private TMP_Text _exhibitName = default;
        [SerializeField] private TMP_Text _exhibitHallName = default;
        [SerializeField] private TMP_Text _exhibitInfoText = default;

        [SerializeField] private Button _createPathButton = default;
        [SerializeField] private Button _backToSearch = default;
        [SerializeField] private Button _backToScan = default;

        [SerializeField] private ImageSliderController _imageSlider = default;
        
        private ExhibitDataSO _exhibitData;
        private ScreenStateManager _screenManager;

        public bool IsActivated { get; private set; }

        private void Start()
        {
            if (_backToScan != null)
            {
                _backToScan.onClick.AddListener(BackToScan);
            }

            if (_backToSearch != null)
            {
                _backToSearch.onClick.AddListener(BackToSearch);
            }
        }

        private void OnDestroy()
        {
            if (_backToSearch != null)
            {
                _backToScan.onClick.RemoveListener(BackToScan);
            }
            
            if (_backToSearch != null)
            {
                _backToSearch.onClick.RemoveListener(BackToSearch);
            }
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

        private void BackToScan()
        {
            _dataContainer.GlobalData.SelectedExhibitData = null;
            _screenManager.SetScreenState(ScreenState.Scan);
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