using AppAssets.Scripts.UI;
using UnityEngine;

namespace ARMuseum.ARImageTracking
{
    public class ScanScreenController : MonoBehaviour, IScreenManager
    {
        [SerializeField] 
        
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