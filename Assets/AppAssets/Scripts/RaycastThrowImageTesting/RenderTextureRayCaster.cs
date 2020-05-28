using UnityEngine;
using UnityEngine.EventSystems;

namespace ARMuseum.RaycastThrowImageTesting
{
    /// <summary>
    /// This assumes some things.
    /// First i use EventSystems to get mouse clicks. This is no requirement, it can be changed easily (by you though ^^).
    /// To set it up correctly, you need an EventSystem in your scene (if you have a UI, you probably have one already).
    /// Second you need to attach a PhysicsRaycaster component to your main camera (the camera the player sees through).
    /// Lastly, on the GameObject which contains the renderer for the render texture (i used a simple quad)
    /// you apply the below script and assign the corresponding camera.
    /// edit: above maybe is a bit verbose, you could reduce it easily if you like one liners,
    /// its just to show how it works.
    /// also please only consider this a proof of concept, its not thoroughly tested
    /// </summary>

    [RequireComponent(typeof(BoxCollider))]
    public class RenderTextureRayCaster : MonoBehaviour, IPointerDownHandler
    {
        //assign in inspector
        public Camera portalExit;

        BoxCollider portal;
        Vector3 portalExitSize;

        void Start() 
        {
            portal = GetComponent<BoxCollider>();
            //this is the target camera resolution, idk if there is another way to get it.
            portalExitSize = new Vector3(portalExit.targetTexture.width, portalExit.targetTexture.height, 0);
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            //the click in world space
            Vector3 worldClick = eventData.pointerCurrentRaycast.worldPosition;
            //transformed into local space
            Vector3 localClick = transform.InverseTransformPoint(worldClick);
            //since the origin of the collider is in its center, we need to offset it by half its size to get it realtive to bottom left
            Vector3 textureClick = localClick + portal.size / 2;
            //now we scale it up by the actual texture size which equals to the "camera resoution"
            Vector3 rayOriginInCameraSpace = Vector3.Scale(textureClick, portalExitSize);

            //with this knowledge we can creata a ray.
            Ray portaledRay = portalExit.ScreenPointToRay(rayOriginInCameraSpace );
            RaycastHit raycastHit;

            //and cast it.
            if (Physics.Raycast(portaledRay, out raycastHit)) 
            {
                Debug.DrawLine(portaledRay.origin, raycastHit.point, Color.blue, 4);
                Debug.Log(raycastHit.collider.name);
            }
            else 
            {
                Debug.DrawRay(portaledRay.origin, portaledRay.direction * 100, Color.red, 4);
            }
        }
    }
}