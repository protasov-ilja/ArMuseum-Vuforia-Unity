using System;
using UnityEngine;

namespace ARMuseum
{
    public class LocationRecognizer : MonoBehaviour
    {
        private string _currentLocation;

        public event Action<string> OnEnterLocation;
        public event Action<string> OnExitLocation;
        
        private void OnTriggerEnter(Collider other)
        {
            // if it is a navTrigger then calculate angle and spawn a new AR arrow
            if (other.gameObject.TryGetComponent<NavigationDestination>(out var locationObject))
            {
                _currentLocation = locationObject.HallData.HallName;
                
                OnEnterLocation?.Invoke(_currentLocation);

                Debug.Log($"current location: { _currentLocation }");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            // if it is a navTrigger then calculate angle and spawn a new AR arrow
            if (other.gameObject.TryGetComponent<NavigationDestination>(out var locationObject))
            {
                if (_currentLocation == locationObject.HallData.HallName)
                {
                    OnExitLocation?.Invoke(_currentLocation);
                    _currentLocation = "";

                    Debug.Log($"current location changed");
                }
            }
        }
    }
}