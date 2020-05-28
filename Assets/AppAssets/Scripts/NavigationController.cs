using System;
using System.Linq;
using ARMuseum.RaycastThrowImageTesting;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace ARMuseum
{
    public class NavigationController : MonoBehaviour
    {
        [Header("UI")] public TextMeshProUGUI whereText;
        
        [Header("AppComponents")]
        public GameObject trigger; // trigger to spawn and despawn AR arrows
        public NavigationDestination[] destinations; // list of destination positions
        [FormerlySerializedAs("person")] public GameObject _userTarget; // person indicator
        
        // used to hit rays throw minimap to unity world
        [FormerlySerializedAs("_textureRayCaster")] public RenderTexture2DRayCaster _textureMapRayCaster;
        
        [SerializeField]
        private LineRenderer _line; // line renderer to display path

        [SerializeField] // used to collect spawned triggers
        private TriggerCollector _triggersCollector;
        
        private NavigationDestination _navTarget; // current chosen destination
        private NavMeshPath _path; // current calculated path
        private bool _destinationSet; // bool to say if a destination is set

        private GameObject _destination;
        private GameObject _destinationAnchor;
        
        //create initial path, get linerenderer.
        private void Start()
        {
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
        public void SetDestination(string destinationName)
        {
            _triggersCollector.ClearList();
            
            if (_navTarget != null)
            {
                _navTarget.ActivateDestinationPointer(false);
            }

            var targetTransform = destinations.Where(d => d.gameObject.name == destinationName).ToList();
            
            var prevTrigger = GameObject.Find("NavTrigger(Clone)");
            if (prevTrigger != null)
            {
                Destroy(prevTrigger);
            }
            
            _navTarget = targetTransform[0];
            _navTarget.ActivateDestinationPointer(true);

            var obj = Instantiate(trigger, _userTarget.transform.position, _userTarget.transform.rotation);
            Debug.Log($"NavigationControllerFound! { destinationName } position: { obj.transform.position }");
            
            _triggersCollector.AddTrigger(obj);
            
            _destinationSet = true;
        }
    }
}