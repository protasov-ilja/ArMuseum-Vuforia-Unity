using System;
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
        
        [SerializeField] private string _id;
        [SerializeField] private string _museumName;
        [SerializeField, TextArea] private string _description;
        [SerializeField] private CityDataSO _city;
        [SerializeField] private Sprite _image;
        [SerializeField] private GameObject _navigationMapData;
        [SerializeField] private ExhibitDataSO[] _exhibits;

        public string MuseumName => _museumName;
        public string CityName => _city.CityName;

        public Sprite MuseumImage => _image;
        public CityDataSO City => _city;
        public ExhibitDataSO[] Exhibits => _exhibits;
    }
}