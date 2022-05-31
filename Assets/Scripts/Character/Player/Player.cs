using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :MonoBehaviour
{
    public int hp;
    public int damage;
    public int maxHp;
    static public Player Instance;
    public bool isDie;
    public bool canMove;
    public int Scoreget = 0;
    public int CriticalRate = 10;
    public float FireRate = 0.15f;
    public int MaxHp { get => maxHp; set => maxHp = value; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateScore();
        isDie = false;
        canMove = true;
        hp = maxHp;
        damage = 1;
    }
    public float stunTime = 0.2f;
    public void TakeDamage(int Damage)
    {
        if (hp <= 0) return;
        PlayerController.Instance.HitReaction();
        hp-=Damage;
        PlayerUI.Instance.SetSizeHealthBar(Mathf.Clamp( 1.0f * hp / maxHp,0,1));
        if (hp<=0) Die();
        EventManager.Instance.Shake(3, 0.25f);
        Invoke("ResetMove", stunTime);
    }
    public void UpdateScore()
    {
        if (Scoreget % 10 == 0 && Scoreget > 0)
        {
            PlayerController.Instance.timeCountDown = Mathf.Clamp(PlayerController.Instance.timeCountDown - 0.004f, 0.09f, 1);
            maxHp += 1;
            PlayerController.Instance.moveSpeed = Mathf.Clamp(PlayerController.Instance.moveSpeed + 0.07f, 3,7);
            hp = Mathf.Clamp(hp + 7, 0, maxHp);
            if (Scoreget > 10 && Scoreget % 100 == 0)
            {
                damage += 1;
                maxHp += 5;
            }
            PlayerUI.Instance.SetSizeHealthBar(hp*1.0f/maxHp);
            EnemyManager.Instance.UpDateEnemy();
        }
        PlayerUI.Instance.SetScore();
    }
    void ResetMove()
    {
        canMove = true;
    }
    public void Die()
    {
        SaveSystem.SaveBestScore(PlayerUI.Instance.MaxScore);
        isDie = true;
        PlayerController.Instance.DieAction();
        GameManager.Instance.DeathUI();
    }
}
