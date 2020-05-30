using System;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARMuseum.SearchScreen
{
    public class ExhibitCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _exhibitNameText;
        [SerializeField] private TMP_Text _hallNameText;
        [SerializeField] private TMP_Text _shortDescription;
        [SerializeField] private Image _exhibitImage;

        [SerializeField] private Button _button; 
        
        private ExhibitDataSO _exhibitData;

        public Action<ExhibitDataSO> OnExhibitChosen;
        public string ExhibitName => _exhibitData.ExhibitName;

        private void Start()
        {
            _button.onClick.AddListener(ExhibitCardClicked);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(ExhibitCardClicked);
        }

        public void Initialize(ExhibitDataSO exhibitData, SearchScreenController controller)
        {
            _exhibitData = exhibitData;
            _exhibitNameText.text = exhibitData.ExhibitName;
            _hallNameText.text = exhibitData.Hall.HallName;
            _shortDescription.text = exhibitData.Description;
            _exhibitImage.sprite = exhibitData.Images[0];
            OnExhibitChosen += controller.ChooseExhibit;
        }

        private void ExhibitCardClicked()
        {
            OnExhibitChosen?.Invoke(_exhibitData);
        }
    }
}