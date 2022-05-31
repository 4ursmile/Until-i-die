using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
public class EventManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CameraEffect;
    static public EventManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        shakeEffect = CameraEffect.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    CinemachineBasicMultiChannelPerlin shakeEffect;
    public void Shake(float intensity, float duration)
    {
        shakeEffect.m_AmplitudeGain = intensity;
        Invoke("StopShake", duration);
    }
    public void StopShake()
    {
        shakeEffect.m_AmplitudeGain = 0;
    }
}
