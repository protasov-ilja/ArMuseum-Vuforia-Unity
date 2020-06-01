using System;
using ARMuseum.ARImageTracking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private string _description = default;
        [SerializeField] private HallDataSO _hall = default;
        [SerializeField] private AudioClip _audioGuid = default;
        [SerializeField] private TrackableExhibitEventHandler _worldTargetPrefab = default;

        public bool Has3dModel => _worldTargetPrefab.Has3DModel;

        public string Id => _id;
        public string ExhibitName => _exhibitName;
        public Sprite[] Images => _images;
        public string Description => _description;
        public HallDataSO Hall => _hall;
        public AudioClip AudioGuid => _audioGuid;
        
        public TrackableExhibitEventHandler WorldTargetPrefab => _worldTargetPrefab;
    }
}