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

    // Start is called before the first frame update
    void Awake()
    {
        companionManager = GetComponent<CompanionManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (companionManager.Companions.Count == 0)
        {
            return;
        }

        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Fire1"))
        {
            readyToFire = true;
            playerMovement.enabled = false;
        }

        if (readyToFire)
        {
            var direction = DetermineFireDirection(movementInput);
            Debug.DrawLine(transform.position, transform.position + (Vector3)direction);
            playerMovement.MoveX(0);
            playerMovement.MoveY(0);
        }

        if (Input.GetButtonUp("Fire1") && readyToFire)
        {
            var direction = DetermineFireDirection(movementInput);

            var companion = companionManager.RemoveLastCompanion();
            companion.transform.position = firePoint.position;

            companion.GetComponent<SpringJoint2D>().enabled = false;
            companion.GetComponent<Rigidbody2D>().AddForce(movementInput * fireForce, ForceMode2D.Impulse);

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
        companion.GetComponent<PlayerMovement>().enabled = true;
    }

    public Vector2 DetermineFireDirection(Vector2 input)
    {
        input = input == Vector2.zero ? Vector2.right : input;
        float angle = Vector2.SignedAngle(Vector2.right, input) * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
