using System;
using UnityEngine;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "CityData_New", menuName = "ARMuseum/CityData", order = 0)]
    public class CityDataSO : ScriptableObject
    {
        [ContextMenu("Generate Id")]
        void GenerateId()
        {
            _id = Guid.NewGuid().ToString();
        }
        
        [SerializeField] private string _id = default;
        [SerializeField] private string _cityName = default;

        public string CityName => _cityName;
    }
}