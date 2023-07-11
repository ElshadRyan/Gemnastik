using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleHandler : MonoBehaviour
{
    GameManager gm;

    private Player player;
    private Sprite soalImages;
    private WaitAnimation enemyWaitAnim;
    private WaitAnimation playerWaitAnim;
    public Sprite images;

    [SerializeField] private Transform playerGameObject;
    [SerializeField] private HPFill enemyHealthBar;
    [SerializeField] private HPFill playerHealthBar;
    [SerializeField] private HPFill timerBar;
    [SerializeField] private Enemy enemy;
    [SerializeField] private StageSO[] stageSO;
    [SerializeField] private GameObject[] prefabEnemy;
    [SerializeField] private RandomInstantiate randomInstantiate;
    [SerializeField] private TextMeshProUGUI textSoal;
    [SerializeField] private TextMeshProUGUI pressSpace;
    [SerializeField] private SetUIPosition setUIPosition;
    [SerializeField] private string[] text = new string[3];


    private int comboCounter;
    private int stageLength;
    private int levelLength;
    private bool isClicked;
    private bool afterIsClicked;
    private bool isAttack;
    private bool spawn = true;
    private bool lastBattle;
    private bool stageCount;
    private bool enemyAttack;
    public bool waitAnimation;
    

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

        player = FindAnyObjectByType<Player>();
        enemyWaitAnim = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<WaitAnimation>();
        playerWaitAnim = player.GetComponentInChildren<WaitAnimation>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        gm.level = 0;
        levelLength = stageSO[gm.stage].levelSO.Length;
        levelLength -= 1;
        stageLength = prefabEnemy.Length;
        comboCounter = 0;
        gm.stagecount = stageSO.Length;

        gm.isBattle = true;
        afterIsClicked = false;
        gm.battleEnd = false;
        lastBattle = false;

        pressSpace.gameObject.SetActive(false);
        enemy.InBattle(gm.isBattle);
        player.InBattle(gm.isBattle);

        Debug.Log(gm.stage);
    }
    private void Update()
    {
        if(!waitAnimation && !gm.battleEnd)
        {
            Timer();
        }
        AttackSequence();
    }

    public void Timer()
    {

        if(gm.timer >= 0 && !waitAnimation)
        {
            timerBar.TimerBar(gm.timer);
            gm.timer -= Time.deltaTime;
        }
        else
        {
            if (isAttack)
            {
                stageSO[gm.stage].levelSO[gm.level].timeIsUp = true;
                gm.timer = 20f;
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
            Instantiate(prefabEnemy[gm.stage], position, Quaternion.Euler(rotation));

        }
    }

    public void AttackSequence()
    {
        
        switch (playState)
        {
            case state.idle:

                spawn = true;
                enemyAttack = false;


                player.Damage(false);
                enemy.IsAttack(false);
                

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

                player.IsAttack(false);
                enemy.Damage(false);

                if(gm.enemyHealth <= 0 || gm.playerHealth <= 0)
                {
                    if (isClicked)
                    {
                        enemyHealthBar.EnemyHealthBar();
                        isClicked = false;

                    }
                    playState = state.after_attack;
                }

                if (!playerWaitAnim.wait)
                {
                    AfterClicked();
                }


                break;
            case state.after_attack:
                if(gm.stage < stageLength)
                {
                    stageSO[gm.stage].levelSO[gm.level].SetAllToFalse();
                }
                if (!enemyWaitAnim.wait)
                {
                    AfterAttack();
                }
                break; 
        }
    }


    public void PlayerAttackCalculate()
    {
        player.IsAttack(true);
        gm.EnemyDamage();
        if(stageSO[gm.stage].levelSO[gm.level].lastCombo)
        {
            gm.playerAttack /= comboCounter;
        }
        //StartCoroutine(AnimationTiming());



        playState = state.enemy_attack;
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

            if(!stageSO[gm.stage].levelSO[gm.level].combo)
            {
                playerWaitAnim.wait = true;

                player.IsAttack(true);
                enemy.Damage(true);
            }
            isClicked = true;


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

        if (isClicked)
        {
            enemyHealthBar.EnemyHealthBar();
            isClicked = false;

        }

        afterIsClicked = true;

        if (!stageSO[gm.stage].levelSO[gm.level].combo)
        {
            enemyWaitAnim.wait = true;
            enemyAttack = true;
            player.Damage(true);
            enemy.IsAttack(true);

        }


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

    public void AfterAttack()
    {

        if (afterIsClicked)
        {
            playerHealthBar.PlayerHealthBar();
            afterIsClicked = false;
            gm.timer = 20f;
        }
        if (levelLength == gm.level && !lastBattle || gm.enemyHealth <= 0 && !lastBattle || gm.playerHealth <= 0 && !lastBattle)
        {

            lastBattle = true;
            stageCount = true;
        }

        if (!lastBattle)
        {

            gm.level++;
            playState = state.idle;
        }

        if (lastBattle || gm.enemyHealth <= 0 || gm.playerHealth <= 0)
        {
            if(gm.stage < gm.stagecount && stageCount)
            {
                if(gm.stage < stageLength)
                {
                    gm.stage++;
                }
                PlayerPrefs.SetInt("Stage", gm.stage);
                stageCount = false;
                Debug.Log("Masuk");
            }
            gm.battleEnd = true;
            gm.level = 0;
            gm.BattleEnd();
            textSoal.text = gm.WinLose;
            pressSpace.gameObject.SetActive(true);
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("WorldMap");
            }
            else
            {
                playState = state.after_attack;
            }

        }


    }

     
}
