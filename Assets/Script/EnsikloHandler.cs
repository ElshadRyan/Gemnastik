using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsikloHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] canvasEnsiklo;
    private int canvasCount;
    bool canvasIsOff;

    private void Start()
    {
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
