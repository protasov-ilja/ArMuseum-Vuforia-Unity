using AppAssets.Scripts.UI;
using ARMuseum.Utils;
using UnityEngine;
using Zenject;

namespace ARMuseum.ChooseMuseumScreen
{
    public class MuseumScreenControllerDummy : MonoBehaviour, IScreenManager
    {
        [Inject] private GenericSceneManager _sceneManager;
        
        public bool IsActivated { get; private set; }
        public void ActivateScreen()
        {
            IsActivated = true;
            gameObject.SetActive(true);
            _sceneManager.LoadSceneAsync("MainScene");
        }

        public void DeactivateScreen()
        {
            gameObject.SetActive(false);
            IsActivated = false;
        }
    }
}