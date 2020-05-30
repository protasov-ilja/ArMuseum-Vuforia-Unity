using System.Collections.Generic;
using ARMuseum;
using UnityEngine;

namespace AppAssets.Scripts.ARNavigation.NavSystem
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private List<NavigationDestination> _destinations;
        [SerializeField] private List<UserRelocationPoint> _relocationPoints;

        public List<NavigationDestination> Destinations => _destinations;
        public List<UserRelocationPoint> RelocationPoints => _relocationPoints;
    }
}