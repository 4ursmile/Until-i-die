using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GunShoot : MonoBehaviour
{
    [SerializeField] Material headGun;
    [SerializeField] string headGunID;
    [SerializeField] ParticleSystem puzzel;
    [SerializeField] float strenghtP;
    [SerializeField] float time = 0.2f;
    
    void Start()
    {
        
    }
    public void Shoot()
    {
        headGun.DOFloat(strenghtP, headGunID, time);
        puzzel.Play();
    }
    public void StopShoot()
    {
        headGun.DOFloat(0, headGunID, time);
    }
}
