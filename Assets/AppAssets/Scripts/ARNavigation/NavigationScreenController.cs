using AppAssets.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace AppAssets.Scripts.ARNavigation
{
    public class NavigationScreenController : MonoBehaviour, IScreenManager
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _helpButton;
        [SerializeField] private Button _mapButton;
        
        public bool IsActivated { get; private set; }
        public void ActivateScreen()
        {
            IsActivated = true;
            gameObject.SetActive(true);
        }

        public void DeactivateScreen()
        {
            IsActivated = false;
            gameObject.SetActive(false);
        }
    }
}