using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rigit;
    private bool jumpPressed;
    private bool isGrounded = true;

    private InputAction moveAction;
    private InputAction jumpAction;
    private Animator animator;


    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isRunning", false);
        animator.SetBool("isGround", true);

        rigit = GetComponent<Rigidbody2D>();

        moveAction = new InputAction("Move", type: InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.Enable();

        jumpAction = new InputAction("Jump", type: InputActionType.Button, binding: "<Keyboard>/space");
        jumpAction.performed += ctx => jumpPressed = true;
        jumpAction.Enable();
    }


    void FixedUpdate()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        rigit.linearVelocity = new Vector2(moveInput.x * speed, rigit.linearVelocity.y);
      
        if (jumpPressed)
        {
            if (isGrounded)
            {
                rigit.linearVelocity = new Vector2(rigit.linearVelocity.x, jumpForce);
                isGrounded = false;
            }

            jumpPressed = false;
        }
     


        if (moveInput.x != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(moveInput.x) * Mathf.Abs(scale.x);
            transform.localScale = scale;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (isGrounded)
        {
            animator.SetBool("isGround", true);
        }
        else
        {
            animator.SetBool("isGround", false);
            animator.SetBool("isRunning", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
    public void Die()
    {
        //Time.timeScale = 0; 
        Debug.Log("Player died!");
    }
}
