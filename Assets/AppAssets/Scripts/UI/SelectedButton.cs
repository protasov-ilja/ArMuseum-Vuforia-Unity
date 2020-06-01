using System;
using UnityEngine;
using UnityEngine.UI;

namespace ARMuseum.UI
{
    [RequireComponent(typeof(Button))]
    public class SelectedButton : MonoBehaviour
    {
        [SerializeField] private GameObject _selectedImage = default;
        [SerializeField] private Button _button = default;
        
        public int Id { get; set; }
        
        private ButtonsSelector _selector;

        private event Action<int> OnButtonClicked;

        private void Awake()
        {
            _selectedImage.SetActive(false);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ButtonClicked);
            if (_selector != null) OnButtonClicked -= _selector.DeactivateOthers;
        }

        public void Subscribe(ButtonsSelector selector, int id)
        {
            Id = id;
            _selector = selector;
            if (_selector != null) OnButtonClicked += _selector.DeactivateOthers;
        }

        private void ButtonClicked()
        {
            _selectedImage.SetActive(true); 
            OnButtonClicked?.Invoke(Id);
        }

        public void DeactivateButton()
        {
            _selectedImage.SetActive(false); 
        }
    }
}