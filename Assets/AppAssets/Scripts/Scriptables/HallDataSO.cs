using System;
using UnityEngine;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "HallData_New", menuName = "ARMuseum/HallData", order = 0)]
    public class HallDataSO : ScriptableObject
    {
        [ContextMenu("Generate Id")]
        void GenerateId()
        {
            _id = Guid.NewGuid().ToString();
        }
        
        [SerializeField] private string _id = default;
        [SerializeField] private string _hallName = default;
        [SerializeField] private string _description = default;
        
        public string HallName => _hallName;
        public string Description => _description;
    }
}