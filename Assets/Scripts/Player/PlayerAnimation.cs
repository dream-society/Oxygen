using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    private PlayerFireCompanion playerFire;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        playerFire = GetComponent<PlayerFireCompanion>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isIdle", playerMovement.MovementInput.x == 0 && playerMovement.Grounded);
        animator.SetBool("isWalking", Mathf.Abs(playerMovement.MovementInput.x) > 0 && playerMovement.Grounded);
        animator.SetFloat("yVelocity", playerMovement.CurrentVelocity.y);
        animator.SetBool("inAir", !playerMovement.Grounded);
        animator.SetBool("isShooting", playerFire.IsShooting);
        Debug.Log(playerMovement.CurrentVelocity.y);
    }
}
