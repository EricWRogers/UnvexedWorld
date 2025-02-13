using System.Collections;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

using SuperPupSystems.Helper;
using Scripts.HUDScripts.MessageSystem;
public class CameraShake : MonoBehaviour
{
public float amplitudeGain;
public float frequemcyGain;
public CinemachineFreeLook cmFreeCam;
public float shakeDuration;

public GameObject player;

public void Start()
{
  player = GameObject.FindGameObjectWithTag("Player");
}

public void DoShake()
{
    StartCoroutine(Shake());
}

public void DoShake(HealthChangedObject healthChangedObject)
{
    StartCoroutine(Shake());
}

public IEnumerator Shake()
{
    Noise(amplitudeGain, frequemcyGain);
    yield return new WaitForSeconds(shakeDuration);
    Noise(0,0);
}

void Noise(float amplitude,float frequency)
{
    cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;

    cmFreeCam.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
    cmFreeCam.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
    cmFreeCam.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

}

   public void Update()
   {
        
         player.GetComponent<SuperPupSystems.Helper.Health>()?.healthChanged.AddListener(DoShake);
   }

}
 