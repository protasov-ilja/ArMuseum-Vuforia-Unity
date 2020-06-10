using System;
using UnityEngine;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "ExhibitData_New", menuName = "ARMuseum/ExhibitData", order = 0)]
    public class ExhibitDataSO : ScriptableObject
    {
        [ContextMenu("Generate Id")]
        void GenerateId()
        {
            _id = Guid.NewGuid().ToString();
        }
        
        [SerializeField] private string _id = default;
        [SerializeField] private string _exhibitName = default;
        [SerializeField] private Sprite[] _images = default;
        [SerializeField, TextArea] private string _shortDescription = default;
        [SerializeField, TextArea] private string _description = default;
        [SerializeField] private HallDataSO _hall = default;
        [SerializeField] private AudioClip _audioGuid = default;
        [SerializeField] private bool _has3DModel;

        public bool Has3dModel => _has3DModel;

        public string Id => _id;
        public string ExhibitName => _exhibitName;
        public Sprite[] Images => _images;
        public string ShortDescription => _shortDescription; 
        public string Description => _description;
        public HallDataSO Hall => _hall;
        public AudioClip AudioGuid => _audioGuid;
    }
}