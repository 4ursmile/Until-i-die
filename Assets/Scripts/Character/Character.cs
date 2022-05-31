using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Character : MonoBehaviour
{
    
    protected int hp;
    protected int damage;
    public Character(int HP = 1, int Damagae = 1)
    {
        hp = HP;
        damage = Damagae;
    }
    public virtual void TakeDamage(int Damage)
    {
        hp-=Damage;
        if (hp<=0) Die();
    }
    public virtual void Die()
    {

    }
}
