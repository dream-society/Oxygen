using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireCompanion : MonoBehaviour
{
    private CompanionManager companionManager;
    private PlayerMovement playerMovement;
    private Vector2 movementInput;
    private bool readyToFire;
    [SerializeField][Range(1,10)] private float fireForce = 1f;

    public Transform firePoint;

    public bool IsShooting { get; private set; }

    private LineRenderer lineRenderer;
    private LineController lineController;

    // Start is called before the first frame update
    void Awake()
    {
        companionManager = GetComponent<CompanionManager>();
        playerMovement = GetComponent<PlayerMovement>();

        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineController = GetComponentInChildren<LineController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (companionManager.Companions.Count == 0)
        {
            return;
        }

        movementInput = new Vector2(Input.GetAxis("MouseX"), Input.GetAxis("MouseY"));

        if (Input.GetButtonDown("Fire1") && playerMovement.Grounded)
        {
            readyToFire = true;
            playerMovement.enabled = false;
        }

        if (readyToFire)
        {
            var direction = MouseDetermineFireDirection(movementInput);
            Debug.DrawLine(transform.position, transform.position + (Vector3)direction);
            playerMovement.MoveX(0);
            playerMovement.MoveY(0);
        }

        if (Input.GetButtonUp("Fire1") && readyToFire)
        {
            var direction = MouseDetermineFireDirection(movementInput);

            var companion = companionManager.RemoveLastCompanion();

            IsShooting = true;

            companion.GetComponent<SpringJoint2D>().enabled = false;
            companion.transform.position = firePoint.position;

            companion.GetComponent<Rigidbody2D>().AddForce(direction * fireForce, ForceMode2D.Impulse);

            companion.GetComponent<DestroyTimer>().StartTimer();

            // enable player movement
            playerMovement.enabled = true;
            readyToFire = false;

            // enable companion movement
            StartCoroutine(EnableCompanionMovement(companion));
        }
    }

    IEnumerator EnableCompanionMovement(GameObject companion)
    {
        yield return new WaitForSeconds(2f);
        companion.GetComponentInChildren<CircleCollider2D>().enabled = true;
        companion.GetComponent<PlayerMovement>().enabled = true;
        EnableElectricity();
    }

    public Vector2 DetermineFireDirection(Vector2 input)
    {
        input = input == Vector2.zero ? Vector2.right : input;
        float angle = Vector2.SignedAngle(Vector2.right, input) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public Vector2 MouseDetermineFireDirection(Vector2 stub)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos -= objPos;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public void DoneShooting() => IsShooting = false;

    public void EnableElectricity()
    {
        lineRenderer.enabled = true;
        lineController.enabled = true;
    }


}
