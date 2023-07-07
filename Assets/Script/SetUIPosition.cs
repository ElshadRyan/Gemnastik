using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUIPosition : MonoBehaviour
{
    GameManager gm;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;
    private int height1;
    private int height2;
    private int height;


    private void Start()
    {
        gm = GameManager.Instance;
        height1 = 235;
        height2 = 350;
    }

    private void Update()
    {
        SetPosition();
    }

    private void SetPosition()
    {
        Vector2 position = new Vector2(0, height);
        if (gm.stage == 0)
        {
            image.gameObject.SetActive(false);
            height = height2;
            rectTransform.anchoredPosition = position;
        }
        else
        {
            image.gameObject.SetActive(true);
            height = height1;
            rectTransform.anchoredPosition = position;
        }
    }

}
