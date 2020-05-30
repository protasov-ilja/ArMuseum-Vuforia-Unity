using ARMuseum.Scriptables;
using UnityEngine;

namespace ARMuseum
{
    public class NavigationDestination : MonoBehaviour
    {
        [SerializeField] private GameObject _destinationPointer;
        [SerializeField] private HallDataSO _hallData;

        public HallDataSO HallData => _hallData;

        public void ActivateDestinationPointer(bool isActive)
        {
            _destinationPointer.SetActive(isActive);
        }
    }
}