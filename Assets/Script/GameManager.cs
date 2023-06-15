using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHealth;
    public int playerAttack;
    public int enemyHealth;
    public int enemyAttack;

    public bool isBattle = false;
    public bool isDestroy = false;


    private void Awake()
    {
        Instance = this;
    }

    public void EnemyDamage()
    {
        enemyHealth -= playerAttack;
        Debug.Log(enemyHealth);
        if (enemyHealth <= 0)
        {
            Debug.Log("You Win");
        }
    }

    public void PlayerDamage()
    {
        playerHealth -= enemyAttack;
        Debug.Log(playerHealth);
        if (playerHealth <= 0)
        {
            Debug.Log("You Lose");
        }
    }

}
