using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    // public Transform target;
    // [SerializeField][Range(1, 10)] public float speed;
    // [SerializeField][Range(1, 10)] public float distance;
    // [SerializeField][Range(1, 10)] public float followRange;
    [SerializeField][Range(1, 10)] private int jumpForce;

    public Vector2 MovementInput { get; private set; }
    public int NormMovementX { get; private set; }
    public int NormMovementY { get; private set; }
    private Rigidbody2D rb;

    public int Facing { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        Facing = 1;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        NormMovementX = Mathf.Abs(MovementInput.x) > 0.5f ? (int)(MovementInput * Vector2.right).normalized.x : 0;
        NormMovementY = Mathf.Abs(MovementInput.y) > 0.5f ? (int)(MovementInput * Vector2.up).normalized.y : 0;
        
        CheckIfShouldFlip(NormMovementX);

        // // if (Vector3.Distance(transform.position, target.position) < followRange)
        // // {
        // //     transform.LookAt(target.position);
        // // }

        // if (Vector2.Distance(transform.position, target.position) < followRange)
        // {   //move towards the player
        //     if (Vector2.Distance(transform.position, target.position) > distance)
        //     {
        //         transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        //     }
        // }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
        }
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != Facing)
        {
            Facing *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f); // SpriteRenderer flip
        }
    }
}
