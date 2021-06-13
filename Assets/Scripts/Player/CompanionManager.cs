﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    // [SerializeField][Range(1,5)] private float distanceFromPlayer = 1;

    public List<GameObject> Companions = new List<GameObject>();
    private Rigidbody2D rb;

    void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
    }

    void OnTriggerEnter2D(Collider2D other) => AddCompanion(other);

    public void AddCompanion(Collider2D collider)
    {
        collider.enabled = false;

        if (collider.CompareTag("Door") || collider.CompareTag("Lethal"))
        {
            //
            return;
        }

        var companion = collider.transform.parent.gameObject;
        companion.GetComponentInParent<PlayerMovement>().enabled = false;
        companion.GetComponentInParent<DestroyTimer>().ResetTimer();

        var joint = companion.GetComponentInParent<SpringJoint2D>();
        joint.enabled = true;
        joint.connectedBody = rb;
        joint.distance = 1 + Companions.Count * 1.5f;

        Companions.Add(companion);
    }

    public GameObject RemoveLastCompanion()
    {
        var companion = Companions[Companions.Count-1];
        Companions.RemoveAt(Companions.Count-1);
        return companion;
    }
}
