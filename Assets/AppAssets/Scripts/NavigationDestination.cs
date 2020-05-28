using UnityEngine;

namespace ARMuseum
{
    public class NavigationDestination : MonoBehaviour
    {
        [SerializeField] private GameObject _destinationPointer;

        public void ActivateDestinationPointer(bool isActive)
        {
            _destinationPointer.SetActive(isActive);
        }
    }
}