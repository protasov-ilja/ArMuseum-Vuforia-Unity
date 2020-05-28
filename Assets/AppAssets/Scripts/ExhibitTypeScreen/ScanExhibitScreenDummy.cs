using AppAssets.Scripts.UI;
using ARMuseum.Utils;
using UnityEngine;
using Zenject;

namespace ARMuseum.ExhibitTypeScreen
{
    public class ScanExhibitScreenDummy : MonoBehaviour, IScreenManager
    {
        [Inject] private GenericSceneManager _sceneManager; 
        
        public bool IsActivated { get; private set; }
        
        public void ActivateScreen()
        {
            IsActivated = true;
            gameObject.SetActive(true);
            _sceneManager.LoadSceneAsync("ObjectsScanScene");
        }

        public void DeactivateScreen()
        {
            IsActivated = false;
            gameObject.SetActive(false);
        }
    }
}