using System;
using System.Collections.Generic;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ARMuseum.ChooseMuseumScreen
{
    public class AppRunner : SerializedMonoBehaviour
    {
        [Inject] private GlobalDataContainer _dataContainer;
        
        [SerializeField] private Dictionary<ScreenState, IScreenManager> _screensBindings;

        private bool _isDataLoaded;

        private void Awake()
        {
            if (_dataContainer != null) return;

            LoadData();
        }

        private void OnEnable()
        {
            if (_isDataLoaded) return;
            if (_dataContainer == null) return;
            
            LoadData();
        }

        private void LoadData()
        {
            Debug.Log("<color=red> DATA LOADED</color>");
            _isDataLoaded = true;
            if (_dataContainer.ScreenManager == null)
            {
                _dataContainer.ScreenManager = new ScreenStateManager(_screensBindings);
            }
            else
            {
                _dataContainer.ScreenManager.ChangeScreenBindings(_screensBindings);
            }
            
            _dataContainer.ScreenManager.LoadCurrentScreenState();
        }

        private void Start()
        {
            if (_isDataLoaded) return;

            LoadData();
        }
    }
}