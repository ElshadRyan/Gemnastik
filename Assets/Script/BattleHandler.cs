using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    GameManager gm;

    private Player player;
    private Battle battle;
    private Enemy enemy;
    private RandomInstantiate randomInstantiate;

    [SerializeField] private Transform playerGameObject;
    [SerializeField] private Transform enemyGameObject;

    private bool isAttack;
    private bool spawn = true;
    float timer = 20f;
    enum state
    {
        idle,
        attack,
        enemy_attack,
        
    }
    [SerializeField] state playState;
    

    private void Start()
    {
        gm = GameManager.Instance;

        Spawn(true);
        Spawn(false);

        gm.isBattle = true;

        randomInstantiate = FindAnyObjectByType<RandomInstantiate>();
        enemy = FindAnyObjectByType<Enemy>();
        battle = FindAnyObjectByType<Battle>();
        player = FindAnyObjectByType<Player>();

    }
    private void Update()
    {
        Timer();
        AttackSequence();
    }

    public void Timer()
    {

        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (isAttack)
            {
                playState = state.enemy_attack;
            }
        }                
    }
    public void Spawn(bool IsPlayer)
    {
        Vector3 position;

        if (IsPlayer)
        {
            position = new Vector3 (5, 0, 0);
            Instantiate(playerGameObject, position, Quaternion.identity);

        }
        else
        {
            position = new Vector3(-5, 0, 0);
            Instantiate(enemyGameObject, position, Quaternion.identity);

        }
    }

    public void AttackSequence()
    {
        
        switch (playState)
        {
            case state.idle:
                timer = 5f;
                spawn = true;
                playState = state.attack;              
                break;
            case state.attack:
                isAttack = true;

                if(spawn)
                {
                    randomInstantiate.RandomSpawn();
                    randomInstantiate.InstantiateSpawn();
                    spawn = false;
                }
                if (gm.isDestroy)
                {
                    PlayerAttackCalculate();
                }
                break;
            case state.enemy_attack:
                gm.isDestroy = false;
                isAttack = false;

                gm.PlayerDamage();
                gm.enemyAttack = gm.enemyAttack * 2;                               
                playState = state.idle;                
                break;
        }
    }


    public void PlayerAttackCalculate()
    {
        player.PlayAnimation(true);
        gm.enemyAttack = gm.enemyAttack / 2;
        gm.EnemyDamage();
        StartCoroutine(AnimationTiming());



        playState = state.enemy_attack;
    }

    IEnumerator AnimationTiming()
    {
        yield return new WaitForSeconds(.5f);
        player.PlayAnimation(false);
    }

}
