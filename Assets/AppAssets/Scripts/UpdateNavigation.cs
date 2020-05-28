using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARMuseum
{
    //used to update AR stuff using colliders
    public class UpdateNavigation : MonoBehaviour
    {
        //[SerializeField] private TextMeshProUGUI _testText;

        public ARAnchorManager anchorManager;
        public GameObject trigger; // collider to change arrows
        public GameObject indicator; // arrow prefab to spawn
        public GameObject arCoreDeviceCam; // ar camera
        public GameObject arrowHelper; // box facing the arrow of person indicator used to calculate spawned AR arrow direction
        public LineRenderer line; // line renderer used to calculate spawned ARarrow direction
        
        private ARAnchor _anchor; //spawned anchor when putting somthing AR on screen
        private bool _hasEntered; //used for onenter collider, make sure it happens only once
        private bool _hasExited; //used for onexit collider, make sure it happens only once
        
        [SerializeField] // used to collect spawned triggers
        private TriggerCollector _triggersCollector;

        [SerializeField] private GameObject _detinationPointerPrefab;

        private void Start()
        {
            _hasEntered = false;
            _hasExited = false;
        }

        private void Update()
        {
            _hasEntered = false;
            _hasExited = false;
        }

        // what to do when entering a collider
        private void OnTriggerEnter(Collider other)
        {
            // if it is a navTrigger then calculate angle and spawn a new AR arrow
            if (other.name.Equals("NavTrigger(Clone)") && line.positionCount > 0)
            {
                if (_hasEntered)
                {
                    return;
                }
                
                _hasEntered = true;

                //logic to calculate arrow angle
                var userUnityPosition = transform.position;
                Vector2 personPos = new Vector2(userUnityPosition.x, 
                    userUnityPosition.z);
                Vector2 personHelp = new Vector2(arrowHelper.transform.position.x, 
                         arrowHelper.transform.position.z);
                Vector3 node3D = line.GetPosition(1);
                Vector2 node2D = new Vector2(node3D.x, node3D.z);

                float angle = Mathf.Rad2Deg * (Mathf.Atan2(personHelp.y - personPos.y, 
                         personHelp.x - personPos.x) - Mathf.Atan2(node2D.y - personPos.y, 
                         node2D.x - personPos.x));

                // position arrow a bit before the camera and a bit lower
                Vector3 pos = arCoreDeviceCam.transform.position
                              + arCoreDeviceCam.transform.forward * 2.5f
                              + arCoreDeviceCam.transform.up * -0.5f;

                // rotate arrow a bit
                Quaternion rot = arCoreDeviceCam.transform.rotation * 
                         Quaternion.Euler(20, 180, 0);

                // create new anchor
                _anchor = anchorManager.AddAnchor(new Pose(pos, rot));
                
                var connectedTrigger = other.gameObject.GetComponent<NavigationTrigger>(); // move script on NavTrigger prefab before start
                connectedTrigger.NavigationArrow = _anchor;
                
                //spawn arrow
                if (line.positionCount > 0)
                {
                    var destinationPosition = line.GetPosition(line.positionCount - 1);
                    var dist = Vector2.Distance(new Vector2(destinationPosition.x, destinationPosition.z), personPos);
                    //_testText.text = dist.ToString();
                    if (dist <= 1.1f)
                    {
                        GameObject destSpawned = Instantiate(_detinationPointerPrefab, 
                            _anchor.transform.position, _anchor.transform.rotation, 
                            _anchor.transform);
                        
                        // use calculated angle on spawned arrow
                        destSpawned.transform.Rotate(0, angle, 0, Space.Self);
                        Debug.Log($"destination spawned { destSpawned.transform.rotation }");
                        
                        return;
                    }
                }
                
                GameObject spawned = Instantiate(indicator, 
                    _anchor.transform.position, _anchor.transform.rotation, 
                    _anchor.transform);
                // use calculated angle on spawned arrow
                spawned.transform.Rotate(0, angle, 0, Space.Self);

                Debug.Log($"arrow spawned { spawned.transform.rotation }");
            }
        }

        //what to do when exiting a collider
        private void OnTriggerExit(Collider other)
        {
            //if it is a navTrigger then delete Anchor and arrow and create a new trigger
            if (other.name.Equals("NavTrigger(Clone)"))
            {
                if (_hasExited)
                {
                    return;
                }
                
                _hasExited = true;
                Destroy(_anchor.gameObject);

                _triggersCollector.ClearList();

                _triggersCollector.AddTrigger(Instantiate(trigger, transform.position, transform.rotation));
            }
        }
    }
}