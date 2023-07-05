using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class LevelSO : ScriptableObject
{

    [SerializeField] private LevelSO[] nextCombo = new LevelSO[3];
    [SerializeField] private bool correctAnswer1;
    [SerializeField] private bool correctAnswer2;
    [SerializeField] private bool correctAnswer3;

    public string soal;
    public Sprite[] imageJawaban = new Sprite[3];
    public string[] jawabanPanjang = new string[3];
    public char[] jawabanSingkat = new char[3];
    public bool button1 = false;
    public bool button2 = false;
    public bool button3 = false;
    public bool combo;
    public int comboCounter;

    public void Button1()
    {
        button1 = true;
        Debug.Log("Button1 Masuk");
        /*if (button1)
        {
            soal = jawabanPanjang[0];
            if (correctAnswer1)
            {
                if (combo)
                {
                    comboCounter++;
                    //nextCombo[0]
                }
            }

        }*/
    }

    public void Button2()
    {
        Debug.Log("Button2 Masuk");
        button2 = true;
        /*if (button2)
        {
            soal = jawabanPanjang[1];
            if (correctAnswer2)
            {
                if (combo)
                {
                    comboCounter++;
                    //nextCombo[0]
                }

            }
        }*/


    }

    public void Button3()
    {
        Debug.Log("Button3 Masuk");
        button3 = true;
       /* if (button3)
        {
            soal = jawabanPanjang[2];
            if (correctAnswer3)
            {
                if (combo)
                {
                    comboCounter++;
                    //nextCombo[0]
                }
            }
        }*/
    }       
        
}
