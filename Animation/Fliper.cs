using UnityEngine;

public class Fliper : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private bool isFacingRight = true;
    private float horizontal;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        horizontal = Input.GetAxis(Axis.Horizontal);

        if (Mathf.Round(rigidbody.velocity.x) != 0)  Flip();
    }

    private void Flip()
    {
        if (isFacingRight != horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}