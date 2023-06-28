using UnityEngine;

[RequireComponent(typeof(GroundCheck))]
public class InputMovable : MonoBehaviour
{
    private GroundCheck groundCheck;

    private ICanMove mover;
    private ICanJump jumper;

    private float horizontal;
    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
     
        mover = GetComponent<ICanMove>();
        jumper = GetComponent<ICanJump>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw(Axis.Horizontal);

        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.IsGrounded())
        {
            jumper.Jump();
        }

        mover.Move(horizontal);
    }
}