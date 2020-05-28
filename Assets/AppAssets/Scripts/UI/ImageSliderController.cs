using System;
using System.Collections.Generic;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AppAssets.Scripts.UI
{
    public class ImageSliderController : MonoBehaviour
    {
        [Inject] private GlobalDataContainer _dataContainer; 
        
        [SerializeField] private Transform _contentTransform;

        [SerializeField] private Image _imageCardPrefab;

        private ExhibitDataSO _exhibitData;

        private List<GameObject> _imagesCards = new List<GameObject>();
        
        private void Awake()
        {
            _exhibitData = _dataContainer.GlobalData.SelectedExhibitData;
        }

        private void OnEnable()
        {
            foreach (var spriteData in _exhibitData.Images)
            {
                var imageCard = Instantiate(_imageCardPrefab, _contentTransform);
                imageCard.sprite = spriteData;
                _imagesCards.Add(imageCard.gameObject);
            }
        }

        private void OnDisable()
        {
            for (var i = _imagesCards.Count - 1; i >= 0; --i)
            {
                Destroy(_imagesCards[i].gameObject);
            }
            
            _imagesCards.Clear();
        }
    }
}