using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtomDead : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private CompanionManager companionManager;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        companionManager = GetComponent<CompanionManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Lethal"))
        {
            return;
        }

        playerMovement.MoveX(0);
        playerMovement.MoveY(0);
        playerMovement.enabled = false;

        animator.SetBool("isDead", true);
        // partire animazione dead
        // partire animazione dead companion
    }

    public void OnDeadAnimationEnd()
    {
  	    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
