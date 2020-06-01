using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace AppAssets.Scripts.ARNavigation
{
    public class NavigationMarkersScanner : MonoBehaviour
    {
        [SerializeField] private ARTrackedImageManager _trackedImageManager = default;

        public event Action<string> OnImageRecognized;
        
        private void OnEnable()
        {
            _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
        
        private void OnDisable()
        {
            _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
        
        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
        {
            foreach (var trackedImage in args.added)
            {
                UpdateARImage(trackedImage);
            }
            
            foreach (var trackedImage in args.updated)
            {
                UpdateARImage(trackedImage);
            }
            
            foreach (var trackedImage in args.removed)
            {
                // remove tracked objects
            }
        }
        
        private void UpdateARImage(ARTrackedImage trackedImage)
        {
            var imageRelocationPointName = trackedImage.referenceImage.name;
            OnImageRecognized?.Invoke(imageRelocationPointName);

            Debug.Log($"tracked image reference name: { trackedImage.referenceImage.name }");
        }
    }
}