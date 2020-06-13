using System.Collections.Generic;
using System.Linq;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ARMuseum.SearchScreen
{
    public class SearchScreenController : MonoBehaviour, IScreenManager
    {
        [Inject] private GlobalDataContainer _dataContainer;

        [SerializeField] private ExhibitCard _exhibitCardPrefab = default;
        [SerializeField] private Transform _contentTransform = default;

        [SerializeField] private TMP_InputField _inputField = default;
        [SerializeField] private Button _confirmInputButton = default; 
        
        public bool IsActivated { get; private set; }

        private GlobalDataSO _globalData;
        private ScreenStateManager _screenManager;
        private List<ExhibitCard> _cards = new List<ExhibitCard>();
        private List<string> _exhibitsNames = new List<string>();

        private List<ExhibitCard> _activeCards = new List<ExhibitCard>();

        private readonly SearchSystem _searchSystem = new SearchSystem();

        private void Start()
        {
            _confirmInputButton.onClick.AddListener(Search);
        }

        private void OnDestroy()
        {
            _confirmInputButton.onClick.RemoveListener(Search);
        }

        private void CustomStart()
        {
            _globalData = _dataContainer.GlobalData;
            _screenManager = _dataContainer.ScreenManager;
            PopulateContent();
        }

        private void PopulateContent()
        {
            var exhibits = _globalData.SelectedMuseumData.Exhibits;
            foreach (var exhibitData in exhibits)
            {
                var card = Instantiate(_exhibitCardPrefab, _contentTransform);
                card.Initialize(exhibitData, this);
                _exhibitsNames.Add(exhibitData.ExhibitName.ToLower());
                _cards.Add(card);
                _activeCards.Add(card);
            }
        }

        private void OnDisable()
        {
            _exhibitsNames.Clear();
            
            for (var i = _cards.Count - 1; i >= 0; --i)
            {
                Destroy(_cards[i].gameObject);    
            }
            
            _activeCards.Clear();
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
            CustomStart();
        }

        public void DeactivateScreen()
        {
            IsActivated = false;
            gameObject.SetActive(false);
        }

        private void ShowAllCards()
        {
            foreach (var card in _cards)
            {
                if (_activeCards.Contains(card)) continue;
                
                card.gameObject.SetActive(true);
                _activeCards.Add(card);
            }
        }

        private void Search()
        {
            if (_inputField.text == "")
            {
                ShowAllCards();
                
                return;
            }
            
            DisableActiveCards();

            string searchString = _inputField.text;
            var result = _searchSystem.Search( searchString, _exhibitsNames );

            foreach (var foundString in result)
            { 
                var exhibitCard = _cards.First(c => c.ExhibitName.ToLower() == foundString);
                exhibitCard.gameObject.SetActive(true);
                _activeCards.Add(exhibitCard);
            }
        }

        private void DisableActiveCards()
        {
            foreach (var activeCard in _activeCards)
            {
                activeCard.gameObject.SetActive(false);
            }
            
            _activeCards.Clear();
        }
    }
}