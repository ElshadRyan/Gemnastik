using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{
    public LevelSO[] nextCombo = new LevelSO[3];
    public bool correctAnswer1;
    public bool correctAnswer2;
    public bool correctAnswer3;
    public bool wrongAnswer;
    public string soal;
    public Sprite[] imageJawaban = new Sprite[3];
    public string[] jawabanPanjang = new string[3];
    public char[] jawabanSingkat = new char[3];
    public bool button1 = false;
    public bool button2 = false;
    public bool button3 = false;
    public bool combo;
    public bool lastCombo;
    public void Button1()
    {
        button1 = true;
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
        if (button2)
        {
            soal = jawabanPanjang[1];
            if(!correctAnswer2)
            {
                wrongAnswer = true;
            }
        }


    }

    public void Button3()
    {
        button3 = true;
        if (button3)
        {
            soal = jawabanPanjang[2];
            if (!correctAnswer3)
            {
                wrongAnswer = true;
            }
        }
    }       
        
}
