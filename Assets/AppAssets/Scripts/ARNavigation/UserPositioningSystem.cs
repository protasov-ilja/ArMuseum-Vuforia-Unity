using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;

namespace AppAssets.Scripts.ARNavigation
{
    public class UserPositioningSystem : MonoBehaviour
    {
        [SerializeField] private NavigationMarkersScanner _markersScanner;
        [SerializeField] private List<UserRelocationPoint> _relocationPoints = new List<UserRelocationPoint>();
        /// <summary>
        /// places on FirstPersonCamera
        /// </summary>
        [SerializeField] private UserDirectionController _userDirectionController;

        // custom fields
        [SerializeField] private GameObject _userObject;
        [SerializeField] private ARPoseDriver _poseDriver;
        
        private Vector3 _prevUserPosition;
        private bool _isTracking = false;

        [SerializeField] private TMP_Text _text;

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
            _markersScanner.OnImageRecognized += RelocateUser;
        }

        private void OnDestroy()
        {
            _markersScanner.OnImageRecognized -= RelocateUser;
        }

        public void Update()
        {
            // Move the person indicator according to position
            Vector3 currentUserPosition = _poseDriver.transform.position;
            if (!_isTracking)
            {
                _isTracking = true;
                _prevUserPosition = _poseDriver.transform.position;
            }
            
            // Remember the previous position so we can apply deltas
            Vector3 deltaPosition = currentUserPosition - _prevUserPosition;
            _text.text = $"{currentUserPosition.ToString()} \n delta{deltaPosition.ToString()}"; 
            if (_userObject != null)
            {
                // The initial forward vector of the sphere must be aligned with the initial camera direction in the XZ plane.
                // We apply translation only in the XZ plane.
                _userObject.transform.position = new Vector3(_userObject.transform.position.x + deltaPosition.x, _userObject.transform.position.y, _userObject.transform.position.z + deltaPosition.z);/*(deltaPosition.x, 0.0f, deltaPosition.z);*/

                // Set the pose rotation to be used in the CameraFollow script
                // deprecated: FirstPersonCamera.GetComponent<ArrowDirection>().targetRot = Frame.Pose.rotation;
                // new:
                //Debug.Log($"Camera Direction { Frame.Pose.rotation.eulerAngles }");
                _userDirectionController.TargetRotation = _poseDriver.transform.rotation;
            }
        }
        
        // move to person indicator to the new spot
        private void RelocateUser(string imageRelocationPointName)
        {
            Debug.Log("Relocate!");
            // find the correct location scanned and move the person to its position

            var relocationPoint = _relocationPoints.First(p => p.PointName == imageRelocationPointName);
            if (relocationPoint != null)
            {
                var userRotationEuler = _userObject.transform.rotation.eulerAngles;
                _userObject.transform.rotation = relocationPoint.transform.rotation;
                Debug.Log($"text: { imageRelocationPointName }, Location: { relocationPoint.PointName }");
                var relocationPointPosition = relocationPoint.transform.position;
                _userObject.transform.position = new Vector3(relocationPointPosition.x, 
                    _userObject.transform.position.y, relocationPointPosition.z);
            }
        }
    }
}