using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private ButtonValue[] buttonValues; 

    public int playerHealth;
    public float playerMaxHealth;
    public int playerAttack;
    public int enemyHealth;
    public float enemyMaxHealth;
    public int enemyAttack;
    public int stage = 0;
    public int level = 0;

    public bool battleEnd = false;
    public bool isBattle = false;
    public bool isDestroy = false;
    public float timer = 20f;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        stage = PlayerPrefs.GetInt("Stage");
    }

    public void BattleEnd()
    {
        if (enemyHealth < playerHealth && battleEnd || enemyHealth <= 0)
        {
            isBattle = false;
            Debug.Log("You Win");
            SceneManager.LoadScene("WorldMap");
        }

        else if (enemyHealth > playerHealth && battleEnd || playerHealth <= 0)
        {
            isBattle = false;
            Debug.Log("You Lose");
            SceneManager.LoadScene("WorldMap");
        }
    }

    public void EnemyDamage()
    {
        enemyHealth -= playerAttack;
        Debug.Log(enemyHealth);
        
    }

    public void PlayerDamage()
    {
        playerHealth -= enemyAttack;
        Debug.Log(playerHealth);
        
    }
}
