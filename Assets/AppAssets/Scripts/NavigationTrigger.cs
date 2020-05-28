using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ARMuseum
{
    public class NavigationTrigger : MonoBehaviour
    {
        public string TriggerId { get; set; }

        public ARAnchor NavigationArrow { get; set; }
        
        private void OnDestroy()
        {
            if (NavigationArrow != null)
            {
                Destroy(NavigationArrow.gameObject);
            }
        }
    }
}