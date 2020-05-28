using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARMuseum.RaycastThrowImageTesting
{
    [RequireComponent(typeof(RawImage))]
    public class RenderTexture2DRayCaster : MonoBehaviour, IPointerClickHandler
    {
        //Top Down Camera
        public Camera miniMapCam;
        public LayerMask rayCastLayers;

        private Texture _imageTeture;
        private RawImage _rawImage;

        public event Action<string> OnRayCastHit;

        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            _imageTeture = GetComponent<RawImage>().texture;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Vector2 localCursor = new Vector2(0, 0);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform, eventData.pressPosition, eventData.pressEventCamera, out localCursor))
            {
                Rect imageRect = _rawImage.rectTransform.rect;

                //Using the size of the texture and the local cursor, clamp the X,Y coords between 0 and width - height of texture
                float coordX = Mathf.Clamp(0, (((localCursor.x - imageRect.x) * _imageTeture.width) / imageRect.width), _imageTeture.width);
                float coordY = Mathf.Clamp(0, (((localCursor.y - imageRect.y) * _imageTeture.height) / imageRect.height), _imageTeture.height);

                //Convert coordX and coordY to % (0.0-1.0) with respect to texture width and height
                float recalcX = coordX / _imageTeture.width;
                float recalcY = coordY / _imageTeture.height;

                localCursor = new Vector2(recalcX, recalcY);

                CastMiniMapRayToWorld(localCursor);
            }
        }

        private void CastMiniMapRayToWorld(Vector2 localCursor)
        {
            Ray miniMapRay = miniMapCam.ScreenPointToRay(new Vector2(localCursor.x * miniMapCam.pixelWidth, localCursor.y * miniMapCam.pixelHeight));
            RaycastHit miniMapHit;
            
            if (Physics.Raycast(miniMapRay, out miniMapHit, Mathf.Infinity, rayCastLayers))
            {
                OnRayCastHit?.Invoke(miniMapHit.collider.gameObject.name);
                Debug.DrawLine(miniMapRay.origin, miniMapHit.point, Color.blue, 4);
                Debug.Log("miniMapHit: " + miniMapHit.collider.gameObject);
            }
            else
            {
                Debug.DrawRay(miniMapRay.origin, miniMapRay.direction * 100, Color.red, 4);
            }
        }
    }
}