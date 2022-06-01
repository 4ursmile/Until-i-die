using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bullet;
    [SerializeField] Transform startPoint;
    Queue<GameObject> pool;
    [SerializeField] int Capacity;
    Queue<GameObject> queuePool;
    [SerializeField] List<AudioClip> audioClips;
    AudioSource audioSource;
    PlayerController playerController;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        pool = new Queue<GameObject> ();
        audioSource = GetComponent<AudioSource>();
        queuePool = new Queue<GameObject> ();
        for(int i = 0; i<Capacity; i++)
        {
            GameObject bull = Instantiate(bullet, startPoint);
            bull.SetActive(false);
            pool.Enqueue(bull);
        }
    }
    public void Shoot()
    {
        GameObject bull = pool.Dequeue();
        bull.SetActive(true);
        
        bull.transform.position = startPoint.position;
        audioSource.PlayOneShot(audioClips[0]);
        bull.transform.rotation = startPoint.rotation;
        bull.transform.SetParent(null);
        queuePool.Enqueue(bull);
        Invoke("Repull", 1);
    }
    void Repull()
    {
        GameObject bull = queuePool.Dequeue();
        bull.SetActive(false);
        pool.Enqueue(bull);
    }
}
