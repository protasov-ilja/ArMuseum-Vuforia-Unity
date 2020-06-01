using UnityEngine;

namespace AppAssets.Scripts.ARNavigation
{
    public class UserRelocationPoint : MonoBehaviour
    {
        [SerializeField] private string _pointName = default;

        public string PointName => _pointName;
    }
}