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

        private ExhibitDataSO[] _exhibits;

        private List<TrackableExhibitEventHandler> Targets = new List<TrackableExhibitEventHandler>();

        private string _recognizedExhibitId;
        
        private void Start()
        {
            _exhibits = _dataContainer.GlobalData.SelectedMuseumData.Exhibits;

            foreach (var exhibitData in _exhibits)
            {
                var exhibitTarget = Instantiate(exhibitData.WorldTargetPrefab);
                exhibitTarget.ExhibitId = exhibitData.Id;
                exhibitTarget.OnExhibitRecognised += OnTargetRecognized;
                exhibitTarget.OnExhibitLost += OnTargetLost;
                Targets.Add(exhibitTarget);
            }
        }

        private void OnTargetRecognized(string recognizedExhibitId)
        {
            var exhibit = _exhibits.First(x => x.Id == recognizedExhibitId);
            _recognizedExhibitId = exhibit.Id;
        }

        private void OnTargetLost(string lostExhibitId)
        {
            // clear all data;
        }
    }
}