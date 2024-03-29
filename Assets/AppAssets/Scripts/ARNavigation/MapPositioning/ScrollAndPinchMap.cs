﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppAssets.Scripts.ARNavigation.MapPositioning
{
    public class ScrollAndPinchMap : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        Vector3 touchStart;
        public float zoomOutMin = 1;
        public float zoomOutMax = 8;
        
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = _camera.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                Zoom(difference * 0.01f);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - _camera.ScreenToWorldPoint(Input.mousePosition);
                _camera.transform.position += direction;
            }

            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        void Zoom(float increment)
        {
            _camera.orthographicSize =
                Mathf.Clamp(_camera.orthographicSize - increment, zoomOutMin, zoomOutMax);
        }
    }
}