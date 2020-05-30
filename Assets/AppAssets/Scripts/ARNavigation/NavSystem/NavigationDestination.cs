using System;
using ARMuseum.Scriptables;
using TMPro;
using UnityEngine;

namespace ARMuseum
{
    public class NavigationDestination : MonoBehaviour
    {
        [SerializeField] private TMP_Text _hallName;
        [SerializeField] private GameObject _destinationPointer;
        [SerializeField] private HallDataSO _hallData;

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