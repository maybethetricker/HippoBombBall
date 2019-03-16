using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CrazyBallController : MonoBehaviour
{
    private Vector2 position;
    private Rigidbody2D rb2D;
    private CircleCollider2D circleCollider2D;
    public Animator animator;
    public Vector2 Speed;
    private float distance;
    private float accelerate;
    private Vector2 StartPosition = new Vector2 (-0.2f,3f);
    Vector2 aim = new Vector2(6f,-3f);
    Vector2 formeraim = new Vector2(6f,-3f);
    public AudioClip body;
    private List<Vector2> AimPositionList;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        AimPositionList=GameObject.Find("enemy").GetComponent<ememycontroller2>().AimPositionList;
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        ResetBall();
        StartBall();
        
    }

    // Update is called once per frame
    void Update()
    {
        AiHelper();
        

    }


    public void StartBall()
    {
        rb2D.constraints=RigidbodyConstraints2D.None;
        float ForceX = Random.Range(100,500);
        if((int)ForceX%2==1)
            ForceX *= -1;
        Vector2 StartForce = new Vector2(ForceX,0);
        rb2D.AddForce(StartForce);
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag ==("Player"))
        {
            HitBall();
        }
        if(other.gameObject.tag ==("Ground"))
        {
            rb2D.constraints=RigidbodyConstraints2D.FreezeAll;
            animator.SetTrigger("explode");
        }
    }

    void HitBall()
    {
        Vector2 velocity = rb2D.velocity;
        if(Mathf.Abs(position.x)<6f)//近距离球
        {
            velocity.x = Random.Range(5f,9f);
            velocity.y = -30f/(rb2D.position.y-2f);
        }
        else
        {
            velocity.x = Random.Range(8f,13f);
            velocity.y = -30f/(rb2D.position.y-2f);
        }
        if(position.x>0)
        {
            velocity.x *= -1;
        }
        
        rb2D.velocity = velocity;
        SoundController.instance.Play(body);
    }


    void ResetBall()
    {
        Vector2 StartSpeed = new Vector2 (0,0);
        rb2D.velocity = StartSpeed;
        transform.position = StartPosition;
    }

    //用于敌方ai的落点预测
    void AiHelper()
    {
        formeraim = aim;
        position = rb2D.position;
        Speed = rb2D.velocity;
        GetAim(ref aim);
        if(aim!=formeraim)
            AimPositionList.Add(aim);
        //AimPositionList.Sort( (a, b) => a.y.CompareTo(b.y) );
    }

    //预测落点
    void GetAim(ref Vector2 AimPosition)
    {
        AimPosition.y=-3f;

        accelerate=Physics2D.gravity.y;//求y方向加速度a=dv/dt
        if(Speed.y>0)
        {
            distance = Speed.x*(-Speed.y/accelerate-Mathf.Sqrt(Speed.y*Speed.y+2f*accelerate*(AimPosition.y-position.y))/accelerate);//x方向预测位移，x=v（t+t0）,t用s=vt+1/2at^2求出，t0=2(v/a)
            AimPosition.x = distance+position.x;
        }
        else//不考虑t0,v不取反
        {
            distance = Speed.x*(-Speed.y/accelerate-Mathf.Sqrt(Speed.y*Speed.y+2f*accelerate*(AimPosition.y-position.y))/accelerate);
            AimPosition.x = distance+position.x;
        }

    }

}

