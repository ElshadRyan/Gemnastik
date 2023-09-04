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
    void Start()
    {
        gm = GameManager.Instance;
        totalStringOnLevel = minigameStageSO.stageMinigameSO[gm.stage].letterPath.Length;
        SpawnPath();
    }

    public void NextLevel()
    {
        if(levelMinigame < totalStringOnLevel - 1)
        {
            if (minigameAdd)
            {
                DestroyPath();
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
        textBehind.text = minigameStageSO.stageMinigameSO[gm.stage].hurufUI[levelMinigame].ToString();
    }

    public void DestroyPath()
    {
        playerCircle = FindAnyObjectByType<PlayerCircle>();
        pathGameObject = FindAnyObjectByType<PlayerCircleSpawn>();
        Destroy(playerCircle.gameObject);
        Destroy(pathGameObject.gameObject);
    }

}
