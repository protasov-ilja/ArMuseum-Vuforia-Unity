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
        
        [SerializeField] private string _id;
        [SerializeField] private string _hallName;
        [SerializeField] private string _description;
    }
}