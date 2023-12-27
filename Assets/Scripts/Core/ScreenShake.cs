using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance; // Singleton instance

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        // Ensure there is only one instance of ScreenShake
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Get the CinemachineVirtualCamera component
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Function to trigger the screen shake
    public void Shake(float intensity, float duration)
    {
        if (noise != null)
        {
            // Set the noise parameters for the given duration
            noise.m_AmplitudeGain = intensity;
            noise.m_FrequencyGain = intensity / 2f;
            Invoke("StopShake", duration);
        }
    }

    // Function to stop the screen shake
    private void StopShake()
    {
        if (noise != null)
        {
            // Reset the noise parameters
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
        }
    }
}