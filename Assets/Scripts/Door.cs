using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool Active { get; set; }
    public LayerMask[] masks;

    void FixedUpdate()
    {
        Active = Physics2D.OverlapCircle(transform.position, 0.9f, masks[0] | masks[1]);
    }

    void OnDrawGizmos() => Gizmos.DrawWireSphere(transform.position, 1f);
}
