﻿using UnityEngine;

public class UserDirectionController : MonoBehaviour
{
    public Quaternion TargetRotation;
    public GameObject ArrowObject;

    public float rotationSmoothingSpeed = 4f;
        
    private void LateUpdate()
    {
        Vector3 targetEulerAngles = TargetRotation.eulerAngles;
        float rotationToApplyAroundY = targetEulerAngles.y;

        float newCamRotAngleY = Mathf.LerpAngle(ArrowObject.transform.localRotation.eulerAngles.y, rotationToApplyAroundY,
            rotationSmoothingSpeed * Time.deltaTime);
        Quaternion newCamRotYQuaternion = Quaternion.Euler(0, newCamRotAngleY, 0);
        ArrowObject.transform.localRotation = newCamRotYQuaternion;
    }
}
