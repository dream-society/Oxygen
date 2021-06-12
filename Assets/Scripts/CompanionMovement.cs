// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CompanionMovement : MonoBehaviour
// {
//     // public Transform target;
//     // [SerializeField][Range(1, 10)] public float speed;
//     // [SerializeField][Range(1, 10)] public float distance;
//     // [SerializeField][Range(1, 10)] public float followRange;
//     [SerializeField][Range(1, 10)] private int jumpForce;

//     public Vector2 MovementInput { get; private set; }
//     public int NormMovementX { get; private set; }
//     public int NormMovementY { get; private set; }
//     public Vector2 CurrentVelocity { get; private set; }

//     private Rigidbody2D rb;

//     public int Facing { get; private set; }

//     // Start is called before the first frame update
//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();

//         Facing = 1;
//     }

//    void Update()
//     {
//         CurrentVelocity = rb.velocity;

//         MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
//         NormMovementX = Mathf.Abs(MovementInput.x) > 0.5f ? (int)(MovementInput * Vector2.right).normalized.x : 0;
//         NormMovementY = Mathf.Abs(MovementInput.y) > 0.5f ? (int)(MovementInput * Vector2.up).normalized.y : 0;

//         CheckIfShouldFlip(NormMovementX);
//         Grounded = CheckIfGrounded();

//         if (Input.GetButtonDown("Jump") && Grounded)
//         {
//             rb.velocity = new Vector2(rb.velocity.x, -jumpForce);

//             MoveY(jumpForce);
//         }
//     }

//     void FixedUpdate()
//     {
//         MoveX(speed * NormMovementX);
//     }

//     public void MoveX(float moveX)
//     {
//         var workspace = new Vector2(moveX, CurrentVelocity.y);
//         rb.velocity = workspace;
//         CurrentVelocity = workspace;
//     }

//     public void MoveY(float moveY)
//     {
//         var workspace = new Vector2(CurrentVelocity.x, moveY);
//         rb.velocity = workspace;
//         CurrentVelocity = workspace;
//     }

//     public void CheckIfShouldFlip(int xInput)
//     {
//         if (xInput != 0 && xInput != Facing)
//         {
//             Facing *= -1;
//             transform.Rotate(0.0f, 180.0f, 0.0f); // SpriteRenderer flip
//         }
//     }
// }
