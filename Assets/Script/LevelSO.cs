using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{
    public LevelSO[] nextCombo = new LevelSO[3];
    public Sprite imageJawaban;
    public Sprite imageSoal;

    public bool correctAnswer1;
    public bool correctAnswer2;
    public bool correctAnswer3;
    public bool wrongAnswer;
    public string soal;
    public string soalTemp;
    public string[] jawabanPanjang = new string[3];
    public string[] jawabanSingkat = new string[3];
    public bool button1 = false;
    public bool button2 = false;
    public bool button3 = false;
    public bool combo;
    public bool lastCombo;
    public bool timeIsUp;
    public bool isPressed;
    
    public void Button1()
    {
        button1 = true;
        isPressed = true;
        if (button1)
        {
            soal = jawabanPanjang[0];
            if (!correctAnswer1)
            {
                wrongAnswer = true;
            }
        }
    }

    public void Button2()
    {
        button2 = true;
        isPressed = true;
        if (button2)
        {
            soal = jawabanPanjang[1];
            if (!correctAnswer2)
            {
                wrongAnswer = true;
            }
        }
    }

    public void Button3()
    {
        button3 = true;
        isPressed = true;
        if (button3)
        {
            soal = jawabanPanjang[2];
            if (!correctAnswer3)
            {
                wrongAnswer = true;
            }
        }
    }

    public void SetAllToFalse()
    {
        
        button1 = false;
        button2 = false;
        button3 = false;
        wrongAnswer = false;
        timeIsUp = false;
        if(isPressed)
        {
            soal = soalTemp;
        }
        else if(!isPressed)
        {
            Debug.Log("Masuk");
            soalTemp = soal;
        }
        isPressed = false;
    }
}
