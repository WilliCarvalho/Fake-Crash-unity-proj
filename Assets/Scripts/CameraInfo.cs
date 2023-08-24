using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInfo
{
    public static Vector3 ConvertToCameraSpace(Vector3 vectorToRotate)
    {
        float currentYValue = vectorToRotate.y;
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        Vector3 cameraForwardZproduct = cameraForward * vectorToRotate.z;
        Vector3 cameraRightXProduct = cameraRight * vectorToRotate.x;

        Vector3 directionToMovePlayer = cameraForwardZproduct + cameraRightXProduct;
        directionToMovePlayer.y = currentYValue;
        return directionToMovePlayer;
    }
}
