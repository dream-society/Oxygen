using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompanionAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isIdle", Mathf.Abs(rb.velocity.x) == 0 || playerMovement.MovementInput.x == 0 && playerMovement.Grounded);
        animator.SetBool("isWalking", Mathf.Abs(rb.velocity.x) > 0 || (playerMovement.MovementInput.x == 0 && playerMovement.Grounded));
        animator.SetBool("inAir", playerMovement.enabled && !playerMovement.Grounded);
    }

    public void OnDeadAnimationEnd()
    {
  	    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
