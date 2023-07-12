using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldMapHandler : MonoBehaviour
{
    GameManager gm;

    public GameObject ensikloCanvas;
    public GameObject chatBox;
    public NPCInteract npcInteract;
    public TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI textMeshProUGUI1;
    public string word;
    public string word1;

    private void Start()
    {
        gm = GameManager.Instance;
        EnsikloIconApear();
    }

    private void Update()
    {
        ChatBoxApear();
    }

    public void EnsikloIconApear()
    {
        if(gm.cutsceneDone == 1)
        {
            textMeshProUGUI.text = word;
            textMeshProUGUI1.text = word1;
            ensikloCanvas.SetActive(true);
        }
        else
        {
            ensikloCanvas.SetActive(false);
        }
    }

    public void ChatBoxApear()
    {
        if(npcInteract.isPressed)
        {
            chatBox.SetActive(true);
        }
    }

    public void NoButtonIsPressed()
    {
        npcInteract.isInteract = false;
        npcInteract.isPressed = false;
        chatBox.SetActive(false);
    }

    public void YesButtonIsPressed()
    {
        npcInteract.isInteract = false;
        npcInteract.isPressed = false;
        npcInteract.yesButton = true;
    }
}
