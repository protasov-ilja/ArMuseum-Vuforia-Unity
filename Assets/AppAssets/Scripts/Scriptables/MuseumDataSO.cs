using System;
using AppAssets.Scripts.ARNavigation.NavSystem;
using UnityEngine;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "MuseumData_New", menuName = "ARMuseum/MuseumData", order = 0)]
    public class MuseumDataSO : ScriptableObject
    {
        [ContextMenu("Generate Id")]
        void GenerateId()
        {
            _id = Guid.NewGuid().ToString();
        }
        
        [SerializeField] private string _id = default;
        [SerializeField] private string _museumName = default;
        [SerializeField, TextArea] private string _description = default;
        [SerializeField] private CityDataSO _city = default;
        [SerializeField] private Sprite _image = default;
        [SerializeField] private MapManager _navigationMapData = default;
        [SerializeField] private ExhibitDataSO[] _exhibits = default;

        public string MuseumName => _museumName;
        public string CityName => _city.CityName;

        public Sprite MuseumImage => _image;
        public CityDataSO City => _city;
        public MapManager NavigationMapData => _navigationMapData;
        public ExhibitDataSO[] Exhibits => _exhibits;
    }
}