using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        gm.enemyAttack = 4;
        gm.enemyHealth = 30;
    }

}
