
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    [SerializeField]AudioSource audioSource;
    [SerializeField] AudioClip audioclip;
    void Start()
    {
    }
    public void playFootStep()
    {
        audioSource.PlayOneShot(audioclip);
    }
}
