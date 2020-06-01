using System;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;

namespace ARMuseum
{
    public class NavigationDestination : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hallName = default;
        [SerializeField] private GameObject _destinationPointer = default;
        [SerializeField] private HallDataSO _hallData = default;

        public HallDataSO HallData => _hallData;

        private void Start()
        {
            _hallName.text = _hallData.HallName;
        }

        public void ActivateDestinationPointer(bool isActive)
        {
            _destinationPointer.SetActive(isActive);
        }
    }
}