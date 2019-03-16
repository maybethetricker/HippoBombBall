using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip jump;
    public float Speed = 50f;
    private Animator animator;
    public Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;
    private float MoveHorizontal;
    private bool OffGround = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        MoveHorizontal = Input.GetAxis("Horizontal");
        Vector2 ForceX = new Vector2(MoveHorizontal*Speed,0.00f);
        Vector2 ForceY = new Vector2(0.0f, 400.0f);
        if(!OffGround)
        {
            rb2D.AddForce(ForceX);
            if(MoveHorizontal!=0)
            {
                animator.SetTrigger("Move");
            }
            if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.W))
            {
                rb2D.AddForce(ForceY);
                animator.SetTrigger("Jump");
                SoundController.instance.Play(jump);
            }
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag ==("Ground"))
        {
            OffGround = false;
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag ==("Ground"))
        {
            OffGround = true;
        }
    }


}
