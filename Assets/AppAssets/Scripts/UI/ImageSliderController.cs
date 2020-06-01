using System.Collections.Generic;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AppAssets.Scripts.UI
{
    public class ImageSliderController : MonoBehaviour
    {
        [Inject] private GlobalDataContainer _dataContainer; 
        
        [SerializeField] private Transform _contentTransform = default;

        [SerializeField] private Image _imageCardPrefab = default;

        private ExhibitDataSO _exhibitData;

        private List<GameObject> _imagesCards = new List<GameObject>();

        public void Initialize()
        {
            _exhibitData = _dataContainer.GlobalData.SelectedExhibitData;
            
            foreach (var spriteData in _exhibitData.Images)
            {
                var imageCard = Instantiate(_imageCardPrefab, _contentTransform);
                imageCard.sprite = spriteData;
                _imagesCards.Add(imageCard.gameObject);
                imageCard.gameObject.SetActive(true);
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