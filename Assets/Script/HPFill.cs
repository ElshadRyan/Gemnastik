using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPFill : MonoBehaviour
{
    GameManager gm;

    [SerializeField] Image imageBar;
    [SerializeField] RectTransform rectTransform;
    private float imageBarProgress = 0f;
    private int maxTimer = 20;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    
    public void PlayerHealthBar()
    {
        imageBarProgress = gm.playerHealth / gm.playerMaxHealth;
        imageBar.fillAmount = imageBarProgress;
    }

    public void EnemyHealthBar()
    {
        imageBarProgress = gm.enemyHealth / gm.enemyMaxHealth;
        imageBar.fillAmount = imageBarProgress;
    }

    public void TimerBar(float barProgress)
    {
        imageBarProgress = barProgress / maxTimer;
        imageBar.fillAmount = imageBarProgress;
    }
}
