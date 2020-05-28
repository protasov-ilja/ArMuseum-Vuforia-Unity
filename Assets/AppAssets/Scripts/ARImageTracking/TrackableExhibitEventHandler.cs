using System;
using UnityEngine;
using Vuforia;

namespace ARMuseum.ARImageTracking
{
    public class TrackableExhibitEventHandler : MonoBehaviour, ITrackableEventHandler
    {
        protected TrackableBehaviour mTrackableBehaviour;
        protected TrackableBehaviour.Status m_PreviousStatus;
        protected TrackableBehaviour.Status m_NewStatus;

        public Action<string> OnExhibitRecognised;
        public Action<string> OnExhibitLost;

        [SerializeField] private ImageTargetBehaviour _targetBehaviour;
        [SerializeField] private TurnOffBehaviour _offBehaviour;
        [SerializeField] private GameObject _3DModel;

        public void Initialize()
        {
            _targetBehaviour.enabled = true;
            _offBehaviour.enabled = true;
            gameObject.SetActive(true);
        }

        public bool Has3DModel => _3DModel != null;
        public string ExhibitId { get; set; }

        protected virtual void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        protected virtual void OnDestroy()
        {
            if (mTrackableBehaviour)
                mTrackableBehaviour.UnregisterTrackableEventHandler(this);
        }

        public void Activate3DModel()
        {
            _3DModel.gameObject.SetActive(true);
        }

        public void Deactivate3DModel()
        {
            _3DModel.gameObject.SetActive(false);
        }

        /// <summary>
        ///     Implementation of the ITrackableEventHandler function called when the
        ///     tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
            TrackableBehaviour.Status previousStatus,
            TrackableBehaviour.Status newStatus)
        {
            m_PreviousStatus = previousStatus;
            m_NewStatus = newStatus;

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
                      " " + mTrackableBehaviour.CurrentStatus +
                      " -- " + mTrackableBehaviour.CurrentStatusInfo);

            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                     newStatus == TrackableBehaviour.Status.NO_POSE)
            {
                OnTrackingLost();
            }
            else
            {
                // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
                // Vuforia is starting, but tracking has not been lost or found yet
                // Call OnTrackingLost() to hide the augmentations
                OnTrackingLost();
            }
        }

        protected virtual void OnTrackingFound()
        {
            if (mTrackableBehaviour)
            {
                var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
                var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
                var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

                // Enable rendering:
                foreach (var component in rendererComponents)
                    component.enabled = true;

                // Enable colliders:
                foreach (var component in colliderComponents)
                    component.enabled = true;

                // Enable canvas':
                foreach (var component in canvasComponents)
                    component.enabled = true;
            }
        }


        protected virtual void OnTrackingLost()
        {
            if (mTrackableBehaviour)
            {
                var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
                var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
                var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

                // Disable rendering:
                foreach (var component in rendererComponents)
                    component.enabled = false;

                // Disable colliders:
                foreach (var component in colliderComponents)
                    component.enabled = false;

                // Disable canvas':
                foreach (var component in canvasComponents)
                    component.enabled = false;
            }
        }
    }
}
