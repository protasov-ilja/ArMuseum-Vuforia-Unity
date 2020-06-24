﻿using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace AppAssets.Scripts.ARNavigation
{
    public class UserPositioningSystem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textDebug;

        [SerializeField] private NavigationMarkersScanner _markersScanner = default;
        
        /// <summary>
        /// places on FirstPersonCamera
        /// </summary>
        [SerializeField] private UserDirectionController _userDirectionController = default;

        // custom fields
        [SerializeField] private GameObject _userObject = default;
        [SerializeField] private ARPoseDriver _poseDriver = default;
        
        [SerializeField] private TMP_Text _text = default;
        
        private Vector3 _prevUserPosition;
        private bool _isStartTracking = true;

        private bool _isRecognized;
        private List<UserRelocationPoint> _relocationPoints = new List<UserRelocationPoint>();

        private float _timeoutTime = 5f;
        private float _timeoutTimer = 5f;

        public event Action OnUserRelocated;

        public bool Room1;
        private bool NextRun;

        public void Awake()
        {
            // Enable ARCore to target 60fps camera capture frame rate on supported devices.
            // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            _textDebug.text = _timeoutTimer.ToString();
            _timeoutTime = 0;
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
            if (Room1)
            {
                RelocateUser( "Room1");
            }

            if ( _timeoutTime < 0)
            {
                _timeoutTime -= Time.deltaTime;
            }

            if ( !_isRecognized )
            {
                return;
            }

            if (!NextRun)
            {
                _prevUserPosition = _poseDriver.transform.position;
                NextRun = true;
                return;
            }
            
            // Move the person indicator according to position
            Vector3 currentUserPosition = _poseDriver.transform.position; // changed to local pos!!!
            if (_isStartTracking)
            {
                _isStartTracking = false;
                _prevUserPosition = _poseDriver.transform.position; // changed to local pos!!!
            }

            // Remember the previous position so we can apply deltas
            
            Vector3 deltaPosition = currentUserPosition - _prevUserPosition;
            _textDebug.text = currentUserPosition + " : " + _prevUserPosition + " : " + deltaPosition;
            _prevUserPosition = _poseDriver.transform.position;
            if (_userObject != null)
            {
                // The initial forward vector of the sphere must be aligned with the initial camera direction in the XZ plane.
                // We apply translation only in the XZ plane.
                _userObject.transform.position = new Vector3(_userObject.transform.position.x + deltaPosition.x,
                    _userObject.transform.position.y, _userObject.transform.position.z + deltaPosition.z);/*(deltaPosition.x, 0.0f, deltaPosition.z);*/

                // Set the pose rotation to be used in the CameraFollow script
                //Debug.Log($"Camera Direction { Frame.Pose.rotation.eulerAngles }");
                _userDirectionController.TargetRotation = _poseDriver.transform.rotation;
            }
        }

        public void Initialize(List<UserRelocationPoint> relocationPoints)
        {
            _relocationPoints = relocationPoints;
        }

        public void ResetTimer()
        {
            _timeoutTime = 0;
        }

        // move to person indicator to the new spot where placed marker
        private void RelocateUser(string imageRelocationPointName)
        {
            if ( IsTimeout() ) return;
            _textDebug.text = _timeoutTimer.ToString() + "Relocate!";
            Debug.Log("Relocate!");
            // find the correct location scanned and move the person to its position

            
            var relocationPoint = _relocationPoints.First(p => p.PointName == imageRelocationPointName);
            if (relocationPoint != null)
            {
                //var userRotationEuler = _userObject.transform.rotation.eulerAngles;
                //_userObject.transform.rotation = relocationPoint.transform.rotation;
                var relocationPointPosition = relocationPoint.transform.position;
                _userObject.transform.position = new Vector3(relocationPointPosition.x, 
                    _userObject.transform.position.y, relocationPointPosition.z);
                
                OnUserRelocated?.Invoke();
            }
        }

        public void StartPositioning()
        {
            _isRecognized = true;
        }

        private bool IsTimeout()
        {
            if ( _timeoutTime > 0 )
            {
                return true;
            }

            _timeoutTime = _timeoutTimer;
            return false;
        }
    }
}