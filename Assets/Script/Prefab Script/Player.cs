using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] private Animator animationPlayer;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float RotateSpeed = 10f;

    private float moveSpeed = 5f;
    GameManager gm;
    NPCInteract npcInteract;

    

    private void Start()
    {
        gm = GameManager.Instance;
        gm.playerAttack = 4;
        gm.playerHealth = 30;
        gm.playerMaxHealth = gm.playerHealth;
        npcInteract = FindAnyObjectByType<NPCInteract>();
    }

    private void Update()
    {

        if (gm.isBattle == false)
        {
            PlayerMovement();
        }

    }

    public void PlayerMovement()
    {
        Vector2 inputVector = new Vector2();
        Vector3 colliderSize = new Vector3( 1, 1, .5f);
        float MoveDistance = moveSpeed * Time.deltaTime;

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

        Vector3 moveDirection = new Vector3(inputVector.y, 0f, inputVector.x).normalized;
        bool canMove = !Physics.BoxCast(playerTransform.position, transform.localScale / 2, moveDirection, Quaternion.identity, MoveDistance, LayerMask.GetMask("Default"));


        if (!canMove)
        {
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = moveDirection.x != 0 && !Physics.BoxCast(playerTransform.position, transform.localScale / 2, moveDirection, Quaternion.identity, MoveDistance, LayerMask.GetMask("Default"));

            if (canMove)
            {
                moveDirection = moveDirectionX;
            }
            else
            {
                Vector3 moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;
                canMove = moveDirection.z != 0 && !Physics.BoxCast(playerTransform.position, transform.localScale / 2, moveDirection, Quaternion.identity, MoveDistance, LayerMask.GetMask("Default"));

                if (canMove)
                {
                    moveDirection = moveDirectionZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDirection * MoveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * RotateSpeed);
        if (moveDirection != Vector3.zero)
        {
            Walking(true);
        }
        else
        {
            Walking(false);
        }
    }

    


    public void Walking(bool walking)
    {
        animationPlayer.SetBool("IsWalking", walking);
    }   

    public void InBattle(bool isInBattle)
    {
        animationPlayer.SetBool("IsInBattle", isInBattle);
    }

    public void IsAttack(bool isAttack)
    {
        animationPlayer.SetBool("IsAttack", isAttack);
    }

    public void Damage(bool damage)
    {
        animationPlayer.SetBool("Damage", damage);
    }

    public void IsDeath(bool isDeath)
    {
        animationPlayer.SetBool("IsDeath", isDeath);
    }

}
