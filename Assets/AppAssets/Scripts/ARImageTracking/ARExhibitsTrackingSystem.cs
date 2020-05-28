using System;
using System.Collections.Generic;
using System.Linq;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.Scriptables;
using ARMuseum.Utils;
using UnityEngine;
using Zenject;

namespace ARMuseum.ARImageTracking
{
    public class ARExhibitsTrackingSystem : MonoBehaviour
    {
        [Inject] private GenericSceneManager _sceneManager;
        [Inject] private GlobalDataContainer _dataContainer;

        [SerializeField] private List<TrackableExhibitEventHandler> _trackingHandlers;
        
        private ExhibitDataSO[] _exhibits;

        private List<TrackableExhibitEventHandler> Targets = new List<TrackableExhibitEventHandler>();

        private string _recognizedExhibitId;
        private TrackableExhibitEventHandler _currentTrackable;

        public Action<string, bool> OnExhibitRecognized;
        public Action OnExhibitLost;
        
        private void Start()
        {
            _exhibits = _dataContainer.GlobalData.SelectedMuseumData.Exhibits;

            foreach (var exhibitData in _exhibits)
            {
                var exhibitTarget = _trackingHandlers.First(handler => handler.ConnectedExhibitId == exhibitData.Id);
                exhibitTarget.OnExhibitRecognised += OnTargetRecognized;
                exhibitTarget.OnExhibitLost += OnTargetLost;
                Targets.Add(exhibitTarget);
                exhibitTarget.Initialize();
            }
        }

        private void OnTargetRecognized(string recognizedExhibitId, TrackableExhibitEventHandler handler)
        {
            _currentTrackable = handler;
            var exhibit = _exhibits.First(x => x.Id == recognizedExhibitId);
            _dataContainer.GlobalData.SelectedExhibitData = exhibit;
            _recognizedExhibitId = exhibit.Id;
            OnExhibitRecognized?.Invoke(exhibit.ExhibitName, _currentTrackable.Has3DModel);
            Debug.Log("FOUND");
        }

        private void OnTargetLost(string lostExhibitId)
        {
            _currentTrackable = null;
            Debug.Log("LOST");
            _recognizedExhibitId = "";
            OnExhibitLost?.Invoke();
        }

        public void Activate3DModel()
        {
            _currentTrackable.Activate3DModel();
        }

        public void Deactivate3DModel()
        {
            _currentTrackable.Deactivate3DModel();
        }
    }
}