using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHandler : MonoBehaviour
{
    GameManager gm;

    private Player player;

    public Sprite[] images;

    [SerializeField] private Transform playerGameObject;
    [SerializeField] private Transform enemyGameObject;
    [SerializeField] private LevelSO[] levelSO;
    [SerializeField] private RandomInstantiate randomInstantiate;

    private int comboCounter;
    private bool isAttack;
    private bool spawn = true;
    public int stage = 0;
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

        comboCounter = 0;

        gm.isBattle = true;        
        player = FindAnyObjectByType<Player>();

    }
    private void Update()
    {
        //Timer();
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
                if (spawn)
                {
                    randomInstantiate.levelSO = levelSO[stage];

                    AssigningImage();
                    randomInstantiate.RandomSpawn();
                    randomInstantiate.InstantiateSpawn();
                    spawn = false;
                }
                playState = state.attack;
                break;
            case state.attack:
                isAttack = true;                
                if (gm.isDestroy)
                {

                    if (!levelSO[stage].wrongAnswer)
                    {
                        PlayerAttackCalculate();
                    }
                    else if (levelSO[stage].wrongAnswer && levelSO[stage].combo)
                    {
                        playState = state.enemy_attack;
                    }
                    else if (levelSO[stage].wrongAnswer)
                    {
                        playState = state.enemy_attack;
                    }

                    if (levelSO[stage].combo)
                    {
                        playState = state.enemy_attack;
                    }

                    if(levelSO[stage].lastCombo && !levelSO[stage].wrongAnswer)
                    {
                        LastComboCalculate();
                        gm.playerAttack *= comboCounter;
                        PlayerAttackCalculate();
                        playState = state.enemy_attack;
                    }

                }
                break;

            case state.enemy_attack:

                gm.isDestroy = false;
                isAttack = false;
                stage++;

                if(!levelSO[stage].wrongAnswer)
                {
                    gm.enemyAttack /= 2;
                    gm.PlayerDamage();
                    gm.enemyAttack *= 2;
                    playState = state.idle;

                }
                if (!levelSO[stage].combo && levelSO[stage].wrongAnswer)
                {
                    gm.PlayerDamage();
                    playState = state.idle;
                }
                else 
                {
                    Combo();
                    playState = state.idle;
                }
                
                
                break;
        }
    }


    public void PlayerAttackCalculate()
    {
        player.PlayAnimation(true);
        gm.EnemyDamage();
        if(levelSO[stage].lastCombo)
        {
            gm.playerAttack /= comboCounter;
        }
        StartCoroutine(AnimationTiming());



        playState = state.enemy_attack;
    }

    IEnumerator AnimationTiming()
    {
        yield return new WaitForSeconds(.5f);
        player.PlayAnimation(false);
    }

    public void AssigningImage()
    {
        Sprite sprite;
        for(int i = 0; i<images.Length; i++)
        {
            sprite = levelSO[stage].imageJawaban[i];
            images[i] = sprite;
            randomInstantiate.buttonImage[i] = images[i];
        }
        
    }

    public void LastComboCalculate()
    {
        if(levelSO[stage].lastCombo)
        {
            if (levelSO[stage].button1 && levelSO[stage].correctAnswer1)
            {
                comboCounter++;
            }
            if (levelSO[stage].button2 && levelSO[stage].correctAnswer2)
            {
                comboCounter++;
            }
            if (levelSO[stage].button3 && levelSO[stage].correctAnswer3)
            {
                comboCounter++;
            }
        }
    }

    public void Combo()
    {
        if (levelSO[stage].combo)
        {
            if(levelSO[stage].button1 && levelSO[stage].correctAnswer1)
            {
                levelSO[stage] = levelSO[stage].nextCombo[0];
                comboCounter++;
            }
            if (levelSO[stage].button2 && levelSO[stage].correctAnswer2)
            {
                levelSO[stage] = levelSO[stage].nextCombo[1];
                comboCounter++;
            }
            if (levelSO[stage].button3 && levelSO[stage].correctAnswer3)
            {
                levelSO[stage] = levelSO[stage].nextCombo[2];
                comboCounter++;
            }
        }
    }
}
