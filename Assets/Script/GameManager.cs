using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private ButtonValue[] buttonValues; 

    public int playerHealth;
    public int playerAttack;
    public int enemyHealth;
    public int enemyAttack;
    public int stage = 0;
    public int level = 0;

    public bool battleEnd = false;
    public bool isBattle = false;
    public bool isDestroy = false;

    
    
    
    private void Awake()
    {
        Instance = this;
    }

    
    public void BattleEnd()
    {
        if (enemyHealth < playerHealth && battleEnd)
        {
            isBattle = false;
            Debug.Log("You Win");
            SceneManager.LoadScene("WorldMap");
        }

        else if (enemyHealth > playerHealth && battleEnd)
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
