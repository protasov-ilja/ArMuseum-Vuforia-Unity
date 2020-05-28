using AppAssets.Scripts.UI;
using UnityEngine;

namespace ARMuseum.SearchScreen
{
    public class SearchScreenController : MonoBehaviour, IScreenManager
    {
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