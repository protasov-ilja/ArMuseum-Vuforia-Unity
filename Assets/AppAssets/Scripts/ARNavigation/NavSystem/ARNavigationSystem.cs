using System;
using System.Collections.Generic;
using System.Linq;
using AppAssets.Scripts.ARNavigation;
using AppAssets.Scripts.ARNavigation.NavSystem;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.RaycastThrowImageTesting;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Zenject;

namespace ARMuseum
{
    public class ARNavigationSystem : MonoBehaviour
    {
        [Inject] private GlobalDataContainer _dataContainer;
        
        [Header("AppComponents")]
        [SerializeField] private GameObject _triggerPrefab = default; // trigger to spawn and despawn AR arrows
        
        [SerializeField] private GameObject _userTarget = default; // person indicator
        [SerializeField] private UserPositioningSystem _positioningSystem = default;

        // used to hit rays throw minimap to unity world
        [SerializeField] private RenderTexture2DRayCaster _textureMapRayCaster = default;
        
        [SerializeField] private LineRenderer _line = default; // line renderer to display path
        [SerializeField] private TriggerCollector _triggersCollector = default; // used to collect spawned triggers
        
        private List<NavigationDestination> _destinations; // list of destination positions
        private NavigationDestination _navTarget; // current chosen destination
        private NavMeshPath _path; // current calculated path
        private bool _destinationSet; // bool to say if a destination is set

        private GameObject _destination;
        private GameObject _destinationAnchor;

        private GlobalDataSO _globalData;
        private MapManager _mapManager;

        public event Action<string> OnDestinationSet;

        private bool _isDataAlreadySet;

        private IPositioningSystemSubscriber _subscriber;
        
        private void Start()
        {
            if (_isDataAlreadySet) return;
            
            _globalData = _dataContainer.GlobalData;
            _mapManager = Instantiate(_globalData.SelectedMuseumData.NavigationMapData);
            
            _destinations = _mapManager.Destinations;

            _positioningSystem.Initialize(_mapManager.RelocationPoints);

            // create path
            _path = new NavMeshPath();
            _destinationSet = false; // reset destination
        }

        private void OnEnable()
        {
            _textureMapRayCaster.OnRayCastHit += SetDestination;
        }

        private void OnDisable()
        {
            _textureMapRayCaster.OnRayCastHit -= SetDestination;
        }

        private void Update()
        {
            //if a target is set, calculate and update path
            if (_navTarget != null)
            {
                var isFound = NavMesh.CalculatePath(_userTarget.transform.position, _navTarget.transform.position, 
                    NavMesh.AllAreas, _path);

                //lost path due to standing above obstacle (drift)
                if(!isFound)
                {
                    Debug.Log("Try moving away for obstacles (optionally recalibrate)");
                }
                
                Debug.Log("Path calculated!");
                _line.positionCount = _path.corners.Length;
                _line.SetPositions(_path.corners);
                _line.enabled = true;
            }
        }

        //set current destination and create a trigger for showing AR arrows
        private void SetDestination(string destinationName)
        {
            _triggersCollector.ClearList();
            
            if (_navTarget != null)
            {
                _navTarget.ActivateDestinationPointer(false);
            }

            var targetTransform = _destinations.Where(d => d.gameObject.name == destinationName).ToList();
            
            var prevTrigger = GameObject.Find("NavTrigger(Clone)");
            if (prevTrigger != null)
            {
                Destroy(prevTrigger.gameObject);
            }
            
            _navTarget = targetTransform[0];
            _navTarget.ActivateDestinationPointer(true);
            OnDestinationSet?.Invoke(_navTarget.HallData.HallName);
        }

        public void SetDestinationManually(string destinationName)
        {
            _isDataAlreadySet = true;
            _globalData = _dataContainer.GlobalData;
            _mapManager = Instantiate(_globalData.SelectedMuseumData.NavigationMapData);
            
            _destinations = _mapManager.Destinations;

            _positioningSystem.Initialize(_mapManager.RelocationPoints);

            // create path
            _path = new NavMeshPath();
            _destinationSet = false; // reset destination
            
            _triggersCollector.ClearList();
            
            if (_navTarget != null)
            {
                _navTarget.ActivateDestinationPointer(false);
            }

            var targetTransform = _destinations.Where(d => d.HallData.HallName == destinationName).ToList();
            
            var prevTrigger = GameObject.Find("NavTrigger(Clone)");
            if (prevTrigger != null)
            {
                Destroy(prevTrigger.gameObject);
            }
            
            _navTarget = targetTransform[0];
            _navTarget.ActivateDestinationPointer(true);
            OnDestinationSet?.Invoke(_navTarget.HallData.HallName);
        }

        public void SetPositioningSystemSubscriber(IPositioningSystemSubscriber subscriber)
        {
            _subscriber = subscriber;
            _positioningSystem.OnUserRelocated += _subscriber.OnUserRelocated;
        }

        private void OnDestroy()
        {
            _positioningSystem.OnUserRelocated += _subscriber.OnUserRelocated;
        }

        public void ConfirmDestination()
        {
            var obj = Instantiate(_triggerPrefab, _userTarget.transform.position, _userTarget.transform.rotation);
            Debug.Log($"NavigationControllerFound! { _navTarget.name } position: { obj.transform.position }");
            
            _triggersCollector.AddTrigger(obj);
            
            _destinationSet = true;
        }
    }
}