using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletHitEvent : MonoBehaviour
{
    public static BulletHitEvent Instance;
    public List<LayerMask> masks;
    public List<AudioClip> audioClips;
    [SerializeField] ParticleSystem puzzel;
    [SerializeField] ParticleSystem Blood;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void OnhitBullet(Transform bull,int index)
    {
        EventManager.Instance.Shake(2, 0.2f);
        puzzel.transform.position = bull.position;
        puzzel.transform.rotation = bull.localRotation;
        puzzel.Play();
        Blood.transform.position = bull.position;
        Blood.transform.rotation = bull.localRotation;
        Blood.Play();
        audioSource.PlayOneShot(audioClips[index]);
    }
}
