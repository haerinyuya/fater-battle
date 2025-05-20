using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FaceManager : MonoBehaviour
{
    public ARCameraManager arCameraManager;

    void Start()
    {
        if (arCameraManager == null)
        {
            arCameraManager = FindObjectOfType<ARCameraManager>();
        }

        if (arCameraManager != null)
        {
            arCameraManager.requestedFacingDirection = CameraFacingDirection.User;
        }
    }
}
