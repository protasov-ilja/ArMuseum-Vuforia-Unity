using UnityEngine;

namespace AppAssets.Scripts.ARNavigation
{
    public class UserRelocationPoint : MonoBehaviour
    {
        [SerializeField] private string _pointName;

        public string PointName => _pointName;
    }
}