using TMPro;
using UnityEngine;

namespace ARMuseum
{
    public class LocationRecognizer : MonoBehaviour
    {
        public TextMeshProUGUI _currentDestinationText;

        private string _currentLocation;
        
        private void OnTriggerEnter(Collider other)
        {
            // if it is a navTrigger then calculate angle and spawn a new AR arrow
            if (other.gameObject.TryGetComponent<NavigationDestination>(out var locationObject))
            {
                _currentLocation = other.gameObject.name;
                _currentDestinationText.text = _currentLocation;

                Debug.Log($"current location: { _currentLocation }");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            // if it is a navTrigger then calculate angle and spawn a new AR arrow
            if (other.gameObject.TryGetComponent<NavigationDestination>(out var locationObject))
            {
                if (_currentLocation == other.gameObject.name)
                {
                    _currentLocation = "";
                    _currentDestinationText.text = "";
                    
                    Debug.Log($"current location changed");
                }
            }
        }
    }
}