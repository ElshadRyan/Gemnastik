using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] private Animator animationPlayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    private float moveSpeed = .01f;
    GameManager gm;
    NPCInteract npcInteract;

    

    private void Start()
    {
        gm = GameManager.Instance;
        gm.playerAttack = 4;
        gm.playerHealth = 20;
        npcInteract = FindAnyObjectByType<NPCInteract>();
    }

    private void Update()
    {
        if(gm.isBattle == false)
        {
            PlayerMovement();
        }

        if(npcInteract.isInteract)
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
            transform.LookAt(transform.position + new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.y = -moveSpeed;
            transform.LookAt(transform.position + new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.x = -moveSpeed;
            transform.LookAt(transform.position + new Vector3(0, 0, -1));
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.y = +moveSpeed;
            transform.LookAt(transform.position + new Vector3(1, 0, 0));
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

    }

    public void PlayAnimation(bool isAttack)
    {
        animationPlayer.SetBool("Attack", isAttack);
    }

}
