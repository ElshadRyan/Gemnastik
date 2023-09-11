using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCutscene;
    [SerializeField] private GameObject[] canvas;
    [SerializeField] private CutsceneWordSO[] cutsceneWordSO;
    [SerializeField] private string[] cutsceneWord;
    [SerializeField] private float textSpeed;
    [SerializeField] private TextMeshProUGUI bisindoText;
    [SerializeField] private TextMeshProUGUI chalkText;
    [SerializeField] private Canvas worldCanvas;
    [SerializeField] private string[] text;

    private int index;
    private int canvasCount;
    private int indexCountInSO;
    private bool canvasCountIndex;

    private void Start()
    {
        textCutscene = canvas[canvasCount].GetComponentInChildren<TextMeshProUGUI>();
        StartDialogue();
    }

    private void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            if(textCutscene.text == cutsceneWord[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textCutscene.text = cutsceneWord[index];
            }
        }
        TextOnBoard();
    }

    public void StartDialogue()
    {
        CopyWordInSO();
        index = 0;
        textCutscene.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in cutsceneWord[index].ToCharArray())
        {
            textCutscene.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < cutsceneWord.Length - 1)
        {
            index++;
            textCutscene.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if(index == cutsceneWord.Length - 1)
        {
            if (canvasCount < canvas.Length - 1)
            {
                canvasCountIndex = true;
                canvas[canvasCount].gameObject.SetActive(false);
                canvasCount++;
                canvas[canvasCount].gameObject.SetActive(true);
            }
            else
            {
                PlayerPrefs.SetInt("CutsceneEnd", 1);
                SceneManager.LoadScene("MinigameScene");
            }

            textCutscene = canvas[canvasCount].GetComponentInChildren<TextMeshProUGUI>();
            StartDialogue();
        }

    }

    public void CopyWordInSO()
    {
        indexCountInSO = cutsceneWordSO[canvasCount].cutsceneWord.Length;
        cutsceneWord = new string[indexCountInSO];


        for (int i = 0; i < cutsceneWordSO[canvasCount].cutsceneWord.Length; i++)
        {
            cutsceneWord[i] = cutsceneWordSO[canvasCount].cutsceneWord[i];
        }
    }

    public void TextOnBoard()
    {
        if(canvasCount > 0)
        {
            worldCanvas.gameObject.SetActive(true);
        }

        if(canvasCount > 5)
        {
            worldCanvas.gameObject.SetActive(false);
        }

        if (canvasCountIndex && canvasCount <= 5) 
        {
            bisindoText.text = text[canvasCount];
            chalkText.text = text[canvasCount];
            canvasCountIndex = false;
        }

    }
}
