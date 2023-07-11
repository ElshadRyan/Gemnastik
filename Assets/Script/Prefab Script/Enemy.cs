using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gm;
    BattleHandler battleHandler;

    [SerializeField] private Animator animator;


    private void Start()
    {
        gm = GameManager.Instance;
        gm.enemyAttack = 4;
        gm.enemyHealth = 1000;
        gm.enemyMaxHealth = gm.enemyHealth;
    }

    public void InBattle(bool inBattle)
    {
        animator.SetBool("IsInBattle", inBattle);
    }

    public void IsAttack(bool isAttack)
    {
        animator.SetBool("IsAttack", isAttack);
    }

    public void IsDeath(bool isDeath)
    {
        animator.SetBool("IsDeath", isDeath);
    }

    public void Damage(bool Damage)
    {
        animator.SetBool("Damage", Damage);   
    }    
}
