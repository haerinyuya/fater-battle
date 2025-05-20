using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class LightEstimate : MonoBehaviour
{
    [SerializeField] ARCameraManager cameraManager;
    private Light lighting;

    void Start()
    {
        this.lighting = GetComponent<Light>();
        cameraManager.frameReceived += OnCameraFrameReceived;
    }

    void OnCameraFrameReceived(ARCameraFrameEventArgs e)
    {
        Color color = Color.white;
        float intensity = 1.0f;

        if (e.lightEstimation.averageBrightness.HasValue)
        {
            intensity = e.lightEstimation.averageBrightness.Value;
            intensity *= 2.0f;
            if (intensity > 1) intensity = 1.0f;
        }
        if (e.lightEstimation.averageColorTemperature.HasValue)
        {
            color = Mathf.CorrelatedColorTemperatureToRGB(e.lightEstimation.averageColorTemperature.Value);
        }

        Color c = color * intensity;
        lighting.color = c;
        RenderSettings.ambientSkyColor = c;
    }

    private void OnDisable()
    {
        cameraManager.frameReceived -= OnCameraFrameReceived;
    }
}