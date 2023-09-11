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

    private int index;
    private int canvasCount;
    private int indexCountInSO;

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
}
