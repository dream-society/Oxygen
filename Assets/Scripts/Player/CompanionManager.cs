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

    public void AddCompanion(GameObject companion)
    {
        var joint = companion.GetComponent<SpringJoint2D>();
        joint.enabled = true;

        // if (companionManager.Companions.Count == 0)
        // {
        //     joint.connectedBody = rb;
        // }
        // else
        // {
        //     joint.connectedBody = companionManager.Companions[companionManager.Companions.Count-1].GetComponent<Rigidbody2D>();
        // }
        
        joint.connectedBody = rb;
        joint.distance = 1 + Companions.Count * 1.5f;
        // joint.frequency = 0.6f + Companions.Count * 0.8f;
        Companions.Add(companion);
    }
}
