using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteract : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    public bool isInteract { get;  private set; }
    

    private void Update()
    {
    }

    public void ChangeScene()
    {
        Debug.Log("Masuk");
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isInteract = true;
        }
    }
}
