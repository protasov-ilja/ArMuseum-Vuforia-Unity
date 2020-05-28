using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace AppAssets.Scripts.ARNavigation
{
    public class NavigationSystem : MonoBehaviour
    {
        /// <summary>
        /// places on FirstPersonCamera
        /// </summary>
        public UserDirection ArrowDirection;

        // custom fields
        public GameObject UserObject;
        public ARPoseDriver PoseDriver;
        
        private Vector3 _prevUserPosition;
        private bool _isTracking = false;

        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            //set initial position
            _prevUserPosition = Vector3.zero;
        }
        
        public void Update()
        {
            // Move the person indicator according to position
            Vector3 currentARPosition = PoseDriver.transform.position;
            if (!_isTracking)
            {
                _isTracking = true;
                _prevUserPosition = Vector3.zero; //Frame.Pose.position;
            }
            
            // Remember the previous position so we can apply deltas
            Vector3 deltaPosition = currentARPosition - _prevUserPosition;
            _prevUserPosition = currentARPosition;
            if (UserObject != null)
            {
                // The initial forward vector of the sphere must be aligned with the initial camera direction in the XZ plane.
                // We apply translation only in the XZ plane.
                UserObject.transform.Translate(deltaPosition.x, 0.0f, deltaPosition.z);

                // Set the pose rotation to be used in the CameraFollow script
                // deprecated: FirstPersonCamera.GetComponent<ArrowDirection>().targetRot = Frame.Pose.rotation;
                // new:
                //Debug.Log($"Camera Direction { Frame.Pose.rotation.eulerAngles }");
                ArrowDirection.TargetRotation = PoseDriver.transform.rotation;
            }
        }
    }
}