using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerHealth;
    public float playerMaxHealth;
    public int playerAttack;
    public int enemyHealth;
    public float enemyMaxHealth;
    public int enemyAttack;
    public int stage = 0;
    public int stagecount;
    public int level = 0;

    public bool battleEnd = false;
    public bool isBattle = false;
    public bool isDestroy = false;
    public float timer = 20f;
    public string WinLose;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerPrefs.SetInt("Stage", 0);
        stage = PlayerPrefs.GetInt("Stage");
    }

    public void BattleEnd()
    {
        if (enemyHealth < playerHealth && battleEnd || enemyHealth <= 0)
        {
            isBattle = false;
            WinLose = "You Win";
        }

        else if (enemyHealth > playerHealth && battleEnd || playerHealth <= 0)
        {
            isBattle = false;
            WinLose = "You Lose";
        }
    }

    public void EnemyDamage()
    {
        enemyHealth -= playerAttack;        
    }

    public void PlayerDamage()
    {
        playerHealth -= enemyAttack;        
    }
}
