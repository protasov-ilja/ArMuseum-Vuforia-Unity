using UnityEngine;

namespace ARMuseum.Scriptables
{
    [CreateAssetMenu(fileName = "MuseumsData_New", menuName = "ARMuseum/MuseumsData", order = 0)]
    public class MuseumsDataSO : ScriptableObject
    {
        [SerializeField] private MuseumDataSO[] _museumDataList;

        public MuseumDataSO[] Museums => _museumDataList;
    }
}