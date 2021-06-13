using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtomDead : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) => KillAtom(other);

    void KillAtom(Collider2D other)
    {
        var animator = other.gameObject.GetComponent<Animator>();
        var playerMovement = other.gameObject.GetComponent<PlayerMovement>();

        if (!(other.CompareTag("Player") || other.CompareTag("Companion")))
        {
            return;
        }

        if (playerMovement.enabled)
        {
            playerMovement.MoveX(0);
            playerMovement.MoveY(0);
            playerMovement.enabled = false;
        }

        animator.SetBool("isDead", true);
    }
}
