using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    public Vector2 CurrentVelocity { get; private set; }
    public Vector2 MovementInput { get; private set; }
    public int NormMovementX { get; private set; }
    public int NormMovementY { get; private set; }
    public int Facing { get; set; }
    public bool Grounded { get; private set; }

    [SerializeField][Range(1, 10)]private float speed = 5f;
    [SerializeField][Range(1, 15)]private float jumpForce = 15f;

    public Transform groundCheck;
    [SerializeField][Range(0.1f, 1f)]private float groundCheckRadius = 0.5f;
    [SerializeField] LayerMask whatIsGround;

    [SerializeField] public AudioClip[] clips;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        Facing = 1; // Right
    }

    // Update is called once per frame
    void Update()
    {
        CurrentVelocity = rb.velocity;

        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        NormMovementX = Mathf.Abs(MovementInput.x) > 0.5f ? (int)(MovementInput * Vector2.right).normalized.x : 0;
        NormMovementY = Mathf.Abs(MovementInput.y) > 0.5f ? (int)(MovementInput * Vector2.up).normalized.y : 0;

        CheckIfShouldFlip(NormMovementX);
        Grounded = CheckIfGrounded();

        if (Input.GetButtonDown("Jump") && Grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpForce);

            MoveY(jumpForce);

            // audioSource.clip = clips[2];
            if (CompareTag("Player"))
                audioSource.PlayOneShot(clips[2]);
        }
    }

    void FixedUpdate()
    {
        MoveX(speed * NormMovementX);
    }

    public void MoveX(float moveX)
    {
        var workspace = new Vector2(moveX, CurrentVelocity.y);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void MoveY(float moveY)
    {
        var workspace = new Vector2(CurrentVelocity.x, moveY);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != Facing)
        {
            Facing *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f); // SpriteRenderer flip
        }
    }

    bool CheckIfGrounded() => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

    void OnDrawGizmos() => Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
}
