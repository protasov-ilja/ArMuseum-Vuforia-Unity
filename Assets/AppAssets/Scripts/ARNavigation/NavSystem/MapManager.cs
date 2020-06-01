using System;
using System.Collections.Generic;
using ARMuseum;
using UnityEngine;
using UnityEngine.AI;

namespace AppAssets.Scripts.ARNavigation.NavSystem
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private List<NavigationDestination> _destinations = default;
        [SerializeField] private List<UserRelocationPoint> _relocationPoints = default;
        [SerializeField] private NavMeshSurface _navSurface = default;

        public List<NavigationDestination> Destinations => _destinations;
        public List<UserRelocationPoint> RelocationPoints => _relocationPoints;

        private void Awake()
        {
            if (_navSurface != null)
            {
                _navSurface.BuildNavMesh();
            }
        }
    }
}