using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector2 inputVector;
    private Vector2 lastMoveDirection = Vector2.down;

    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidBody.gravityScale = 0f;
        rigidBody.freezeRotation = true;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputVector = new Vector2(horizontal, vertical);
        if (inputVector.sqrMagnitude > 1f)
        {
            inputVector = inputVector.normalized;
        }

        bool isMoving = inputVector.sqrMagnitude > 0f;
        if (isMoving)
        {
            lastMoveDirection = inputVector;
        }

        animator.SetBool(IsMoving, isMoving);
        animator.SetFloat(MoveX, lastMoveDirection.x);
        animator.SetFloat(MoveY, lastMoveDirection.y);
    }

    private void FixedUpdate()
    {
        Vector2 targetPosition = rigidBody.position + inputVector * moveSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(targetPosition);
    }
}
