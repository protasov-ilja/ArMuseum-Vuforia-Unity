using System.Collections.Generic;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.Scriptables;
using ARMuseum.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ARMuseum.ChooseMuseumScreen
{
    public class MuseumScreenController : MonoBehaviour, IScreenManager
    {
        [Inject] private GenericSceneManager _sceneManager;
        [Inject] private GlobalDataContainer _dataContainer;
        
        [SerializeField] private Transform _contentTransform;
        [SerializeField] private MuseumsDataSO _museumsData;
        [SerializeField] private MuseumCard _cardPrefab;

        private GlobalDataSO _globalData;
        private List<MuseumCard> _cards;

        private ScreenStateManager _screenManager;
        
        public bool IsActivated { get; private set; }
        
        private void Start()
        {
            _globalData = _dataContainer.GlobalData;
            _screenManager = _dataContainer.ScreenManager;
            
            _cards = new List<MuseumCard>();
            foreach (var museumData in _museumsData.Museums)
            {
                var card = Instantiate(_cardPrefab, _contentTransform); 
                card.Initialize(museumData, this);
                _cards.Add(card);
            }
        }

        public void SetMuseumData(MuseumDataSO data)
        {
            _globalData.SelectedMuseumData = data;
            _screenManager.SetScreenState(ScreenState.ExhibitType);
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