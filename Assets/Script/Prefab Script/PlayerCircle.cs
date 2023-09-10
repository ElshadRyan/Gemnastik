using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
    private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D playerRigidbody;

    

    void Update()
    {
        PlayerMovement();
    }
    public void PlayerMovement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        float MoveDistance = moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + inputVector * MoveDistance);        
        
    }

}
