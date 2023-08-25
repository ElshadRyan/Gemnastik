using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
    private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }
    public void PlayerMovement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        float MoveDistance = moveSpeed * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + inputVector * MoveDistance);
        /*if (Input.GetKey(KeyCode.W))
        {
            inputVector = new Vector2();
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.y = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.x = -moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.y = +moveSpeed;
        }*/

        
    }

}
