using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MinigameHanlder : MonoBehaviour
{
    GameManager gm;
    [SerializeField] private MinigameStageSO minigameStageSO;
    [SerializeField] private Transform spawnPathTransform;
    [SerializeField] private PlayerCircle playerCircle;
    [SerializeField] private PlayerCircleSpawn pathGameObject;
    [SerializeField] private TextMeshProUGUI textBehind;

    public bool minigameAdd;
    public int levelMinigame = 0;
    public int totalStringOnLevel;

    private bool pause;
    private bool destroy;
    private float pauseTime;
    void Start()
    {
        gm = GameManager.Instance;
        totalStringOnLevel = minigameStageSO.stageMinigameSO[gm.stage].letterPath.Length;
        pause = false;
        pauseTime = 1f;
        SpawnPath();
    }

    private void Update()
    {
        if(pause)
        {
            if(destroy)
            {
                DestroyPath();
                destroy = false;
            }
            Pause();
        }
    }
    public void NextLevel()
    {        
        if (levelMinigame < totalStringOnLevel - 1)
        {
            if (minigameAdd && !pause)
            {
                levelMinigame++;
                Debug.Log(levelMinigame);
                SpawnPath();
                minigameAdd = false;
            }
        }
        else if(levelMinigame == totalStringOnLevel - 1 && gm.stage == 0)
        {
            SceneManager.LoadScene("WorldMap");
        }
        else
        {
            SceneManager.LoadScene("Battle_Scene");
        }

    }

    public void SpawnPath()
    {
        Instantiate(minigameStageSO.stageMinigameSO[gm.stage].letterPath[levelMinigame], spawnPathTransform);
    }

    public void DestroyPath()
    {
        playerCircle = FindAnyObjectByType<PlayerCircle>();
        pathGameObject = FindAnyObjectByType<PlayerCircleSpawn>();
        Destroy(playerCircle.gameObject);
        Destroy(pathGameObject.gameObject);
    }

    public void TextApear()
    {
        pause = true;
        destroy = true;
        textBehind.gameObject.SetActive(true);
        textBehind.text = minigameStageSO.stageMinigameSO[gm.stage].hurufUI[levelMinigame].ToString();
    }

    public void TextDisapear()
    {
        textBehind.gameObject.SetActive(false);

    }

    public void Pause()
    {
        if(pauseTime >= 0)
        {
            pauseTime -= Time.deltaTime;
            Debug.Log(pauseTime);
        }
        else
        {
            pause = false;
            NextLevel();
            pauseTime = 1f;
            TextDisapear();
        }
    }
    
}
