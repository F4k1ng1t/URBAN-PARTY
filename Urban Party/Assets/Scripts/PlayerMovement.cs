using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float sidewaysForce;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float fallGravity;
    [SerializeField]
    private float airGravity;
    [SerializeField]
    private float groundGravity;
    [SerializeField]
    private Transform playerSprite;

    [SerializeField]
    private Transform GroundCheckPos;

    private bool isFalling;
    private bool atMaxSpeed;
    private bool isGrounded;
    //private float yVelocity;

    public Rigidbody2D rb;


    void FixedUpdate()
    {
        if (isGrounded)
            rb.AddForce(Vector3.down * groundGravity);
        else if (isFalling)
            rb.AddForce(Vector3.down * fallGravity);
        else
            rb.AddForce(Vector3.down * airGravity);
    }

    void Update()
    {

        bool notFalling = rb.velocity.y < 0;

        if (notFalling && CurrentlyGrounded())
        {
            isFalling = false;
            isGrounded = true;
            rb.velocity.Set(rb.velocity.x, 0);
        }
        else
        {
            isFalling = (rb.velocity.y >= 0) ? false : true;
            isGrounded = (CurrentlyGrounded()) ? true : false;
        }

        atMaxSpeed = (Mathf.Abs(rb.velocity.x) > maxSpeed) ? true : false;
    }

    public void Move(Direction direction)
    {

        bool movingRight = rb.velocity.x < 0;
        bool movingLeft = rb.velocity.x > 0;

        if (atMaxSpeed)
        {
            if (movingRight)
                if (direction == Direction.right)
                    rb.AddForce(sidewaysForce * Vector2.right, 0);
            if (movingLeft)
                if (direction == Direction.left)
                    rb.AddForce(sidewaysForce * Vector2.left, 0);
            return;
        }

        if (direction == Direction.left)
        {
            rb.AddForce(sidewaysForce * Vector2.left, 0);
            playerSprite.localScale = new Vector3(-1, 1, 1);
        }
        else if (direction == Direction.right)
        {
            rb.AddForce(sidewaysForce * Vector2.right, 0);
            playerSprite.localScale = new Vector3(1, 1, 1);
        }
    }

    public void TryJump()
    {


        if (CurrentlyGrounded())
        {
            DoJump();
        }
        else
        {
            isGrounded = false;

            if (rb.velocity.x < 0)
                isFalling = true;
        }

    }

    bool CurrentlyGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(GroundCheckPos.position.x, GroundCheckPos.position.y), Vector2.down, 0.2f);

        if (hit)
        {
            bool onPlayer = hit.transform.gameObject.CompareTag("Player");

            
            return true;
        }
        else
            return false;
    }

    void DoJump()
    {
        rb.velocity.Set(rb.velocity.x, 0);
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        isFalling = false;
        isGrounded = false;
    }
}
