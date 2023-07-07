using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBima : MonoBehaviour
{
    GameManager gm;
    BattleHandler battleHandler;

    private void Start()
    {
        gm = GameManager.Instance;
        gm.enemyAttack = 4;
        gm.enemyHealth = 30;
    }

}
