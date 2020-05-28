using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARMuseum.ARImageTracking
{
    public class ARImagesTrackingSystem : MonoBehaviour
    {
        [SerializeField] private ARTrackedImageManager _trackedImageManager;

        [SerializeField] private TMP_Text _imageNameText;

        [SerializeField] private GameObject[] _arObjectsToPlace;
        
        private Dictionary<string, GameObject> _arObjects = new Dictionary<string, GameObject>();

        private void Awake()
        {
            foreach (var arObject in _arObjectsToPlace)
            {
                var newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
                newARObject.name = arObject.name;
                _arObjects.Add(arObject.name, newARObject);
            }
        }

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
                _arObjects[trackedImage.name].SetActive(false);
            }
        }

        private void UpdateARImage(ARTrackedImage trackedImage)
        {
            _imageNameText.text = trackedImage.referenceImage.name;

            AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);
            
            Debug.Log($"tracked image reference name: { trackedImage.referenceImage.name }");
        }

        private void AssignGameObject(string referenceImageName, Vector3 newPosition)
        {
            if (_arObjectsToPlace != null)
            {
                _arObjects[referenceImageName].SetActive(true);
                _arObjects[referenceImageName].transform.position = newPosition;
                _arObjects[referenceImageName].transform.localScale = Vector3.one;

                foreach (var gO in _arObjects.Values)
                {
                    Debug.Log($"GameObject in arObjects.Values: { gO.name }");
                    if (gO.name != referenceImageName)
                    {
                        gO.SetActive(false);
                    }
                }
            }
        }
    }
}