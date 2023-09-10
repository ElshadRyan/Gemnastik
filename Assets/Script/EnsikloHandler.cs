using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsikloHandler : MonoBehaviour
{
    GameManager gm;

    [SerializeField] private GameObject[] canvasEnsiklo;
    private int canvasCount;
    private int stage;
    bool canvasIsOff;

    private void Start()
    {
        gm = GameManager.Instance;

        canvasCount = 0;
    }
    public void PressedNext()
    {
        canvasEnsiklo[canvasCount].SetActive(false);
        canvasIsOff = true;
        if(canvasIsOff)
        {
            canvasCount++;
        }
        canvasEnsiklo[canvasCount].SetActive(true);
    }
    
    public void PressedBack()
    {
        canvasEnsiklo[canvasCount].SetActive(false);
        canvasIsOff = true;
        if (canvasIsOff)
        {
            canvasCount--;
        }
        canvasEnsiklo[canvasCount].SetActive(true);
    }

   
}
