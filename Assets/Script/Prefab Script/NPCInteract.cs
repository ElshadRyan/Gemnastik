using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteract : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    GameManager gm;
    public bool isInteract;

    private void Start()
    {
        gm = GameManager.Instance;
    }
    private void Update()
    {
        if(isInteract)
        {
            Debug.Log("Masuk");
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Battle_Scene");
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isInteract = true;
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
