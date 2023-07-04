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
    public int stage = 1;
    public int subStage = 1;

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
            isBattle = false;
            Debug.Log("You Win");
            SceneManager.LoadScene("WorldMap");
        }
    }

    public void PlayerDamage()
    {
        playerHealth -= enemyAttack;
        Debug.Log(playerHealth);
        if (playerHealth <= 0)
        {
            isBattle = false;
            Debug.Log("You Lose");
            SceneManager.LoadScene("WorldMap");
        }
    }
}
