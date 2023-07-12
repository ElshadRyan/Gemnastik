using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneAfterBattle : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    public void ChangeSceneWorldMap()
    {
        SceneManager.LoadScene("WorldMap");
    }

    public void NewGameButton()
    {
        PlayerPrefs.SetInt("Stage",0);
        PlayerPrefs.SetInt("CutsceneEnd", 0);
        SceneManager.LoadScene("WorldMap");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("WorldMap");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
