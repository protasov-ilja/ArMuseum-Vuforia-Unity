using System.Collections.Generic;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ARMuseum.ChooseMuseumScreen
{
    public class AppRunner : SerializedMonoBehaviour
    {
        [Inject] private GlobalDataContainer _dataContainer;
        
        [SerializeField] private Dictionary<ScreenState, IScreenManager> _screensBindings;

        private void Awake()
        {
            _dataContainer.ScreenManager = new ScreenStateManager(_screensBindings);
            _dataContainer.ScreenManager.LoadCurrentScreenState();
        }
    }
}