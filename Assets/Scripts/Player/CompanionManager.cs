using System.Collections;
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

        var companion = collider.gameObject;
        companion.GetComponent<PlayerMovement>().enabled = false;
        companion.GetComponent<DestroyTimer>().ResetTimer();

        var joint = companion.GetComponent<SpringJoint2D>();
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
