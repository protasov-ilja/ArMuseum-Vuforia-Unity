﻿using System;
using AppAssets.Scripts.ARNavigation.NavSystem;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AppAssets.Scripts.ARNavigation
{
    public class NavigationScreenController : MonoBehaviour, IScreenManager, IPositioningSystemSubscriber
    {
        [Inject] private GlobalDataContainer _dataContainer;
        
        [SerializeField] private Button _backToMuseumButton;
        [SerializeField] private Button _mapButton;

        [SerializeField] private GameObject _mapScreen;
        [SerializeField] private Button _backToNavigationButton;
        [SerializeField] private Button _confirmNavigationButton;

        [SerializeField] private GameObject _focusTargetIcon;
        [SerializeField] private TMP_Text _destinationNameText;
        [SerializeField] private GameObject _destinationInfoPanel;

        [SerializeField] private ARNavigationSystem _arNavigationSystem;
        
        private ScreenStateManager _screenManager;
        private GlobalDataSO _globalData;

        private bool _isUserPositioned;
        private bool _isNavigationConfirmed;
        private bool _isDestinationSet;
        
        public bool IsActivated { get; private set; }
        
        private void CustomStart()
        {
            _globalData = _dataContainer.GlobalData;
            _screenManager = _dataContainer.ScreenManager;
            
            _backToMuseumButton.onClick.AddListener(BackToMuseum);
            _mapButton.onClick.AddListener(OpenMapScreen);
            _backToNavigationButton.onClick.AddListener(() => _mapScreen.gameObject.SetActive(false));
            _confirmNavigationButton.onClick.AddListener(ConfirmNavigation);
            
            _arNavigationSystem.SetPositioningSystemSubscriber(this);
            _arNavigationSystem.OnDestinationSet += DestinationSet;
            
            Initialize();
            
            CheckToSetOfDestinationOnInit();
        }

        private void OnDestroy()
        {
            _backToMuseumButton.onClick.RemoveListener(BackToMuseum);
            _mapButton.onClick.RemoveListener(OpenMapScreen);
            _backToNavigationButton.onClick.RemoveAllListeners();
            _confirmNavigationButton.onClick.RemoveListener(ConfirmNavigation);
        }

        private void BackToMuseum()
        {
            _globalData.ClearCurrentData();
            
            _screenManager.SetScreenState(ScreenState.Museums);
        }

        private void Initialize()
        {
            _focusTargetIcon.gameObject.SetActive(true);
            _destinationInfoPanel.SetActive(false);
            _mapButton.gameObject.SetActive(false);
        }
        
        private void CheckToSetOfDestinationOnInit()
        {
            if (_globalData.SelectedExhibitData != null)
            {
                var hall = _globalData.SelectedExhibitData.Hall;
                
                _arNavigationSystem.SetDestinationManually(hall.HallName);
                _mapScreen.gameObject.SetActive(true);
                _mapButton.gameObject.SetActive(true);
            }
        }
        
        public void OnUserRelocated()
        {
            _focusTargetIcon.SetActive(false);
            _isUserPositioned = true;

            if (_isDestinationSet && _isNavigationConfirmed)
            {
                _arNavigationSystem.ConfirmDestination();
            }
            else
            {
                _mapButton.gameObject.SetActive(true);    
            }
        }
        
        private void DestinationSet(string destinationName)
        {
            _isDestinationSet = true;
            _confirmNavigationButton.gameObject.SetActive(true);

            _destinationNameText.text = destinationName;
        }

        private void ConfirmNavigation()
        {
            _isNavigationConfirmed = true;
            if (_isUserPositioned)
            {
                _arNavigationSystem.ConfirmDestination();
                _destinationInfoPanel.gameObject.SetActive(true);
            }
            
            _mapScreen.gameObject.SetActive(false);
        }

        private void OpenMapScreen()
        {
            _mapScreen.gameObject.SetActive(true);
            _confirmNavigationButton.gameObject.SetActive(false);
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