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
        
        [SerializeField] private string _id;
        [SerializeField] private string _exhibitName;
        [SerializeField] private Image[] _images;
        [SerializeField] private string _description;
        [SerializeField] private HallDataSO _hall;
        [SerializeField] private AudioClip _audioGuid;
        [SerializeField] private TrackableExhibitEventHandler _worldTargetPrefab;

        public bool Has3dModel => _worldTargetPrefab.Has3DModel;

        public string Id => _id;
        public string ExhibitName => _exhibitName;
        public Image[] Images => _images;
        public string Description => _description;
        public HallDataSO Hall => _hall;
        public AudioClip AudioGuid => _audioGuid;
        
        public TrackableExhibitEventHandler WorldTargetPrefab => _worldTargetPrefab;
    }
}