using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    // [SerializeField][Range(1,5)] private float distanceFromPlayer = 1;

    public List<GameObject> Companions = new List<GameObject>();
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;
    private LineController lineController;
    private PlayerMovement playerMovement;
    private AudioSource audioSource;

    void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineController = GetComponentInChildren<LineController>();

        playerMovement = GetComponent<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
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

        DisableElectricity();
    }

    public GameObject RemoveLastCompanion()
    {
        var companion = Companions[Companions.Count-1];
        Companions.RemoveAt(Companions.Count-1);
        return companion;
    }

    public void DisableElectricity()
    {
        lineRenderer.enabled = false;
        lineController.enabled = false;

        if (CompareTag("Player"))
        {
            audioSource.loop = false;
            audioSource.PlayOneShot(playerMovement.clips[1]);
        }
    }
}
