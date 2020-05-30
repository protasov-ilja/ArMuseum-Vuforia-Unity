using System;
using System.Collections.Generic;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using Zenject;

namespace ARMuseum.SearchScreen
{
    public class SearchScreenController : MonoBehaviour, IScreenManager
    {
        [Inject] private GlobalDataContainer _dataContainer; 
        
        [SerializeField] private ExhibitCard _exhibitCardPrefab;
        [SerializeField] private Transform _contentTransform;

        [SerializeField] private TMP_InputField _inputField;
        
        public bool IsActivated { get; private set; }

        private GlobalDataSO _globalData;
        private ScreenStateManager _screenManager;
        private List<ExhibitCard> _cards = new List<ExhibitCard>();

        private void Start()
        {
            _globalData = _dataContainer.GlobalData;

            foreach (var exhibitData in _globalData.SelectedMuseumData.Exhibits)
            {
                var card = Instantiate(_exhibitCardPrefab, _contentTransform);
                card.Initialize(exhibitData, this);
            }
        }

        private void OnEnable()
        {
            for (var i = _cards.Count - 1; i >= 0; --i)
            {
                Destroy(_cards[i].gameObject);    
            }
            
            _cards.Clear();
        }

        public void ChooseExhibit(ExhibitDataSO data)
        {
            _globalData.SelectedExhibitData = data;
            _screenManager.SetScreenState(ScreenState.ExhibitInfo);
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