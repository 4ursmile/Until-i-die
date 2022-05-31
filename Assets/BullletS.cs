using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullletS : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float BullletSpeed = 100;
    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime* BullletSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        BulletHitEvent.Instance.OnhitBullet(this.transform ,0);
        Enemy enemy =   collision.gameObject.GetComponent<Enemy>();
        enemy.TakeDamage(Random.Range(0,Player.Instance.CriticalRate) == 1? Player.Instance.damage*2: Player.Instance.damage);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        BulletHitEvent.Instance.OnhitBullet(this.transform, 0);
        this.gameObject.SetActive(false);

    }
}
