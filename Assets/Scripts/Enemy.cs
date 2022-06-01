using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]public bool isDie;
    public int maxHp;
    private int hp;
    public int damage;
    EnemyState enemyState;
    public float speed = 5;
    [HideInInspector] public bool isMove, isAttack;
    public bool canAttack;

    private void Start()
    {
        Refresh();
        enemyState = GetComponent<EnemyState>();
    }
    public void Refresh()
    {
        canAttack = true;
        isAttack = false;
        isMove = true;
        hp = maxHp;
        isDie = false;
    }
    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        enemyState.TakeDamgeAction();
        enemyState.ChangeHealthBar(Mathf.Clamp( hp*1.0f / maxHp,0,1));
        if (hp <= 0) Die();
    }
    public void Die()
    {
        isDie = true;
        enemyState.DieAction();
        Player.Instance.Scoreget++;
        Player.Instance.UpdateScore();
        Invoke("EnQueueEnemy", 1.7f);
    }
    void EnQueueEnemy()
    {
        EnemyManager.Instance.EnQueueEnemy(this.gameObject);
    }
    public void StrenghtUpdate()
    {
        maxHp += 5;
        damage += 2;
        hp = maxHp;
    }
}
