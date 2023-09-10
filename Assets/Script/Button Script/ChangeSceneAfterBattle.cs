using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneAfterBattle : MonoBehaviour
{
    GameManager gm;

    public Image newGameButtonImage;
    public Image quitGameButtonImage;
    public Image continueButtonImage;

    public Sprite newGameButtonSpriteHover;
    public Sprite quitGameButtonSpriteHover;
    public Sprite continueButtonSpriteHover;

    public Sprite newGameButtonSprite;
    public Sprite quitGameButtonSprite;
    public Sprite continueButtonSprite;



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


    public void QuitGameHover()
    {
        quitGameButtonImage.sprite = quitGameButtonSpriteHover;
    }
    public void ContinueHover()
    {
        continueButtonImage.sprite = continueButtonSpriteHover;
    }
    public void NewGameHover()
    {
        newGameButtonImage.sprite = newGameButtonSpriteHover;
    }

    public void QuitGameExit()
    {
        quitGameButtonImage.sprite = quitGameButtonSprite;
    }
    public void ContinueExit()
    {
        continueButtonImage.sprite = continueButtonSprite;
    }
    public void NewGameExit()
    {
        newGameButtonImage.sprite = newGameButtonSprite;
    }

}
