using System;
using ARMuseum.Scriptables;
using UnityEngine;

namespace ARMuseum.ChooseMuseumScreen
{
    public class GlobalDataContainer : MonoBehaviour
    {
        [SerializeField] private GlobalDataSO _globalData = default;
        public GlobalDataSO GlobalData => _globalData;
        public ScreenStateManager ScreenManager
        {
            get => _globalData.ScreenManager;
            set => _globalData.ScreenManager = value;
        }
    }
}