using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySense : MonoBehaviour
{
    // Start is called before the first frame update
    EnemyState enemyState;
    void Start()
    {
        enemyState = GetComponentInParent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        enemyState.StartAttack();
    }
    private void OnTriggerExit(Collider other)
    {
        enemyState.EndAttack();    }
}
