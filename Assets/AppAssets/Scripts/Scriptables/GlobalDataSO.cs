using System.Collections.Generic;
using AppAssets.Scripts.UI;
using AppAssets.Scripts.UI.Enums;
using ARMuseum.ChooseMuseumScreen;
using ARMuseum.ExhibitTypeScreen.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "GlobalData_New", menuName = "ARMuseum/GlobalData", order = 0)]
    public class GlobalDataSO : SerializedScriptableObject
    {
        public ScreenStateManager ScreenManager { get; set; }

        public MuseumDataSO SelectedMuseumData { get; set; }
        public ExhibitDataSO SelectedExhibitData { get; set; }
        public ExhibitType SelectedExhibitType { get; set; }
    }
}