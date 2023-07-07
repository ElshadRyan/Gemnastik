using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHandler : MonoBehaviour
{
    GameManager gm;

    private Player player;
    private Sprite soalImages;
    public Sprite images;

    [SerializeField] private Transform enemyGameObject;
    [SerializeField] private Transform playerGameObject;
    [SerializeField] private StageSO[] stageSO;
    [SerializeField] private RandomInstantiate randomInstantiate;
    [SerializeField] private TextMeshProUGUI textSoal;
    [SerializeField] private SetUIPosition setUIPosition;
    [SerializeField] private string[] text = new string[3];


    private int comboCounter;
    private int levelLength;
    private bool isAttack;
    private bool spawn = true;
    private bool lastBattle;
    float timer = 20f;
    

    enum state
    {
        idle,
        attack,
        enemy_attack,
        after_attack,
        
    }
    [SerializeField] state playState;
    

    private void Start()
    {
        gm = GameManager.Instance;

        Spawn(true);
        Spawn(false);

        gm.stage = 1;
        gm.level = 0;
        levelLength = stageSO[gm.stage].levelSO.Length;
        levelLength -= 1;
        comboCounter = 0;

        gm.isBattle = true;
        gm.battleEnd = false;
        lastBattle = false;
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
                stageSO[gm.stage].levelSO[gm.level].timeIsUp = true;
                randomInstantiate.Invisible();                
            }
        }                
    }
    public void Spawn(bool IsPlayer)
    {
        Vector3 position;
        Vector3 rotation;

        if (IsPlayer)
        {
            position = new Vector3 (5, 0, 0);
            rotation = new Vector3(0, -90, 0);
            Instantiate(playerGameObject, position, Quaternion.Euler(rotation));

        }
        else
        {

            position = new Vector3(-5, 0, 0);
            rotation = new Vector3(0, 90, 0);
            Instantiate(enemyGameObject, position, Quaternion.Euler(rotation));

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
                    randomInstantiate.levelSO = stageSO[gm.stage].levelSO[gm.level];
                    AssigningAnswer();
                    AssigningImageSoal();
                    AssigningImage();
                    randomInstantiate.RandomSpawn();
                    randomInstantiate.InstantiateSpawn();
                    spawn = false;
                }
                stageSO[gm.stage].levelSO[gm.level].SetAllToFalse();
                playState = state.attack;
                break;
            case state.attack:
                isAttack = true;

                WhenClicked();

                break;

            case state.enemy_attack:
                gm.isDestroy = false;
                isAttack = false;

                AfterClicked();

                break;
            case state.after_attack:
                stageSO[gm.stage].levelSO[gm.level].SetAllToFalse();
                if (levelLength == gm.level)
                {
                    lastBattle = true;
                }

                if (!lastBattle)
                {
                    gm.level++;
                    playState = state.idle;
                }

                if(lastBattle || gm.enemyHealth <= 0 || gm.playerHealth<=0)
                {
                    gm.battleEnd = true;
                    gm.BattleEnd();
                }
                break; 
        }
    }


    public void PlayerAttackCalculate()
    {
        player.AttackAnimation(true);
        gm.EnemyDamage();
        if(stageSO[gm.stage].levelSO[gm.level].lastCombo)
        {
            gm.playerAttack /= comboCounter;
        }
        //StartCoroutine(AnimationTiming());



        playState = state.enemy_attack;
    }

    IEnumerator AnimationTiming()
    {
        yield return new WaitForSeconds(.5f);
        player.AttackAnimation(false);
    }

    public void AssigningImage()
    {
        Sprite sprite;
        
        
            sprite = stageSO[gm.stage].levelSO[gm.level].imageJawaban;
            images = sprite;
            randomInstantiate.buttonImage = images;
        
        
    }

    public void AssigningAnswer()
    {
        for(int i =  0; i<3; i++)
        {
            text[i] = stageSO[gm.stage].levelSO[gm.level].jawabanSingkat[i];
            randomInstantiate.text[i] = text[i];
        }
    }
    public void AssigningImageSoal()
    {
        soalImages = stageSO[gm.stage].levelSO[gm.level].imageSoal;
        setUIPosition.imageSoal = soalImages;
    }
    public void LastComboCalculate()
    {
        if(stageSO[gm.stage].levelSO[gm.level].lastCombo)
        {
            if (stageSO[gm.stage].levelSO[gm.level].button1 && stageSO[gm.stage].levelSO[gm.level].correctAnswer1)
            {
                comboCounter++;
            }
            if (stageSO[gm.stage].levelSO[gm.level].button2 && stageSO[gm.stage].levelSO[gm.level].correctAnswer2)
            {
                comboCounter++;
            }
            if (stageSO[gm.stage].levelSO[gm.level].button3 && stageSO[gm.stage].levelSO[gm.level].correctAnswer3)
            {
                comboCounter++;
            }
        }
    }

    public void Combo()
    {
        if (stageSO[gm.stage].levelSO[gm.level].combo)
        {
            if (stageSO[gm.stage].levelSO[gm.level].button1 && stageSO[gm.stage].levelSO[gm.level].correctAnswer1)
            {
                stageSO[gm.stage].levelSO[gm.level + 1] = stageSO[gm.stage].levelSO[gm.level].nextCombo[0];
                comboCounter++;
            }
            if (stageSO[gm.stage].levelSO[gm.level].button2 && stageSO[gm.stage].levelSO[gm.level].correctAnswer2)
            {
                stageSO[gm.stage].levelSO[gm.level + 1] = stageSO[gm.stage].levelSO[gm.level].nextCombo[1];
                comboCounter++;
            }
            if (stageSO[gm.stage].levelSO[gm.level].button3 && stageSO[gm.stage].levelSO[gm.level].correctAnswer3)
            {
                stageSO[gm.stage].levelSO[gm.level + 1] = stageSO[gm.stage].levelSO[gm.level].nextCombo[2];
                comboCounter++;
            }
           
        }
    }

    public void WhenClicked()
    {
        textSoal.text = stageSO[gm.stage].levelSO[gm.level].soal;
        if (gm.isDestroy)
        {

            Debug.Log(stageSO[gm.stage].levelSO[gm.level].wrongAnswer);
            if (stageSO[gm.stage].levelSO[gm.level].timeIsUp)
            {
                stageSO[gm.stage].levelSO[gm.level].wrongAnswer = true;
                playState = state.enemy_attack;
            }
            if (stageSO[gm.stage].levelSO[gm.level].combo)
            {
                if (!stageSO[gm.stage].levelSO[gm.level].wrongAnswer)
                {
                    Combo();
                    playState = state.enemy_attack;
                }
                if (stageSO[gm.stage].levelSO[gm.level].wrongAnswer)
                {
                    playState = state.enemy_attack;
                }
            }

            else if (stageSO[gm.stage].levelSO[gm.level].lastCombo)
            {
                if (!stageSO[gm.stage].levelSO[gm.level].wrongAnswer)
                {
                    LastComboCalculate();
                }
                if (comboCounter > 0)
                {
                    gm.playerAttack *= comboCounter;
                    PlayerAttackCalculate();
                    playState = state.enemy_attack;
                }

                else if (comboCounter <= 0)
                {
                    playState = state.enemy_attack;
                }

            }
            if (!stageSO[gm.stage].levelSO[gm.level].wrongAnswer && !stageSO[gm.stage].levelSO[gm.level].combo && !stageSO[gm.stage].levelSO[gm.level].lastCombo)
            {
                PlayerAttackCalculate();
            }
            else if (stageSO[gm.stage].levelSO[gm.level].wrongAnswer && !stageSO[gm.stage].levelSO[gm.level].combo && !stageSO[gm.stage].levelSO[gm.level].lastCombo)
            {
                playState = state.enemy_attack;
            }

        }
    }

    public void AfterClicked()
    {
        textSoal.text = stageSO[gm.stage].levelSO[gm.level].soal;
        if (stageSO[gm.stage].levelSO[gm.level].combo)
        {
            playState = state.after_attack;
        }
        else if (stageSO[gm.stage].levelSO[gm.level].lastCombo)
        {
            gm.PlayerDamage();
            playState = state.after_attack;
        }

        if (!stageSO[gm.stage].levelSO[gm.level].wrongAnswer && !stageSO[gm.stage].levelSO[gm.level].combo && !stageSO[gm.stage].levelSO[gm.level].lastCombo)
        {
            gm.enemyAttack /= 2;
            gm.PlayerDamage();
            gm.enemyAttack *= 2;
            playState = state.after_attack;

        }
        else if (stageSO[gm.stage].levelSO[gm.level].wrongAnswer && !stageSO[gm.stage].levelSO[gm.level].combo && !stageSO[gm.stage].levelSO[gm.level].lastCombo)
        {
            gm.PlayerDamage();
            playState = state.after_attack;
        }
    }

    
}
