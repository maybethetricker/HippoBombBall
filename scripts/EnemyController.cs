using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public BallController other;
    public AudioClip jump;
    public Rigidbody2D rb2D;
    public float speed = 50f;
    private bool OffGround = false;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb2D=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        MistakeSet(other.position,ref other.AimPosition);
        MoveX(other.AimPosition);
        if((other.position.y-rb2D.position.y)< 3f&&Mathf.Abs(other.position.x-rb2D.position.x)< 2f)//球在正上方
        {
            MoveY();
        }
    }

    public void MoveX(Vector2 Aim)//ai move in XDir
    {
        Vector2 ForceX = new Vector2(speed,0.00f);
        if(rb2D.position.x<Aim.x)
        {
            rb2D.AddForce(ForceX);
        }
        else
        {
            rb2D.AddForce(-ForceX);
        }
        animator.SetTrigger("Move");
    }

    public void MistakeSet(Vector2 Position,ref Vector2 Aim)
    {
        float OnComing = -Position.x+13f;//以屏幕右端为零点，左侧为正方向
        OnComing = Random.Range(OnComing,OnComing+3f);//越往左误差越大
        Aim.x += Random.Range(-OnComing,OnComing)-2f;//修正击球点为身子正中
    }

    public void MoveY()//ai jump
    {
        Vector2 ForceY = new Vector2(0f,400f);
        if(!OffGround)
        {
            rb2D.AddForce(ForceY);
        }
        animator.SetTrigger("Jump");
        SoundController.instance.Play(jump);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag=="Ground")
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
        if(other.gameObject.tag=="Ground")
        {
            OffGround = true;
        }        
    }
}
