using System;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARMuseum.SearchScreen
{
    public class ExhibitCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _exhibitNameText = default;
        [SerializeField] private TMP_Text _hallNameText = default;
        [SerializeField] private TMP_Text _shortDescription = default;
        [SerializeField] private Image _exhibitImage = default;

        [SerializeField] private Button _button = default; 
        
        private ExhibitDataSO _exhibitData;

        private SearchScreenController _controller;

        public event Action<ExhibitDataSO> OnExhibitChosen;
        public string ExhibitName => _exhibitData.ExhibitName;

        private void Start()
        {
            _button.onClick.AddListener(ExhibitCardClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ExhibitCardClicked);
            OnExhibitChosen -= _controller.ChooseExhibit;
        }

        public void Initialize(ExhibitDataSO exhibitData, SearchScreenController controller)
        {
            _controller = controller;
            _exhibitData = exhibitData;
            _exhibitNameText.text = exhibitData.ExhibitName;
            _hallNameText.text = exhibitData.Hall.HallName;
            _shortDescription.text = exhibitData.Description;
            _exhibitImage.sprite = exhibitData.Images[0];
            OnExhibitChosen += _controller.ChooseExhibit;
        }

        private void ExhibitCardClicked()
        {
            OnExhibitChosen?.Invoke(_exhibitData);
        }
    }
}