using System.Collections.Generic;
using UnityEngine;

namespace ARMuseum
{
    public class TriggerCollector : MonoBehaviour
    {
        public List<GameObject> NavTriggers { get; set; } = new List<GameObject>();

        public void ClearList()
        {
            for (var i = NavTriggers.Count - 1; i >= 0; --i)
            {
                Destroy(NavTriggers[i]);
            }
            
            NavTriggers.Clear();
        }

        public void AddTrigger(GameObject trigger)
        {
            NavTriggers.Add(trigger);
        }
    }
}