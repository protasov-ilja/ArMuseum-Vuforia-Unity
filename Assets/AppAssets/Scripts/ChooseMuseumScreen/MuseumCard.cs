using System;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARMuseum.ChooseMuseumScreen
{
    public class MuseumCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _museumName;
        [SerializeField] private TMP_Text _cityName;
        [SerializeField] private Image _museumImage;
        [SerializeField] private Button _button;

        private MuseumDataSO _museumData;

        private Action<MuseumDataSO> OnButtonPressed;

        private void Start()
        {
            _button.onClick.AddListener(OnCardPressed);
        }
        
        public void Initialize(MuseumDataSO museumData, MuseumScreenController controller)
        {
            _museumData = museumData;
            OnButtonPressed += controller.SetMuseumData;
            InitUI();
        }

        private void InitUI()
        {
            _museumName.text = _museumData.MuseumName;
            _cityName.text = _museumData.CityName;
            _museumImage.sprite = _museumData.MuseumImage;
        }

        private void OnCardPressed()
        {
            OnButtonPressed?.Invoke(_museumData);
        }
    }
}