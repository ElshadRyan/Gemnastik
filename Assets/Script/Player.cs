using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] private Animator animationPlayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float RotateSpeed = 10f;

    private float moveSpeed = .01f;
    GameManager gm;
    NPCInteract npcInteract;

    

    private void Start()
    {
        gm = GameManager.Instance;
        gm.playerAttack = 4;
        gm.playerHealth = 30;
        npcInteract = FindAnyObjectByType<NPCInteract>();
    }

    private void Update()
    {
        if(gm.isBattle == false)
        {
            PlayerMovement();
        }

        if(npcInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                npcInteract.ChangeScene();
            }
        }

    }

    public void PlayerMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.x = +moveSpeed;
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
        }

        Vector3 moveDirection = new Vector3(inputVector.y, 0f, inputVector.x);
        bool canMove = !Physics.BoxCast(playerTransform.position, transform.localScale / 2, moveDirection, Quaternion.identity, moveSpeed, LayerMask.GetMask("Default"));


        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f);
            canMove = moveDirection.x != 0 && !Physics.BoxCast(playerTransform.position, transform.localScale / 2, moveDirection, Quaternion.identity, moveSpeed, LayerMask.GetMask("Default"));

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z);
                canMove = moveDirection.z != 0 && !Physics.BoxCast(playerTransform.position, transform.localScale / 2, moveDirection, Quaternion.identity, moveSpeed, LayerMask.GetMask("Default"));

                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * RotateSpeed);
        if(moveDirection != Vector3.zero)
        {
            Walking(true);
        }
        else
        {
            Walking(false);
        }
    }

    

    public void AttackAnimation(bool isAttack)
    {
        animationPlayer.SetBool("Attack", isAttack);
    }

    public void Walking(bool walking)
    {
        animationPlayer.SetBool("IsWalking", walking);
    }

    

}
