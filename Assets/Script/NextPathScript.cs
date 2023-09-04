using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPathScript : MonoBehaviour
{
    [SerializeField] private MinigameHanlder minigameHanlder;

    private void Start()
    {
        minigameHanlder = FindAnyObjectByType<MinigameHanlder>();

        minigameHanlder.minigameAdd = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            minigameHanlder.NextLevel();
        }
    }
}
