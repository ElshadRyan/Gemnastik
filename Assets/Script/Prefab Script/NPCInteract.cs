using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteract : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    GameManager gm;
    public bool isInteract;
    public bool yesButton;
    public bool isPressed;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    private void Update()
    {
        if(isInteract)
        {
            ChangeScene();                        
        }
        if (yesButton)
        {
            SceneManager.LoadScene("Battle_Scene");
        }
    }

    public void ChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPressed = true;

            if (gm.cutsceneDone == 1)
            {
                
            }
            else
            {
                SceneManager.LoadScene("Cutscene");
            }

        }

        


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            isInteract = true;
        }
    }
}
