using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    [SerializeField] AudioClip audioclip;
    [SerializeField] AudioClip hitReactSound;
    void Start()
    {
    }
    [SerializeField][Range(0, 1)] float weightSound = 0.45f; 
    public void playFootStep(AnimationEvent animationEventinfor)
    {
        if (animationEventinfor.animatorClipInfo.weight>weightSound)
            audioSource.PlayOneShot(audioclip);
    }
    public void playHitRaction()
    {
        audioSource.PlayOneShot(hitReactSound);
    }
}
