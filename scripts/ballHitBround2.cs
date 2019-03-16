using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//新模式中炸弹击中地面时的判定
public class ballHitBround2 : MonoBehaviour
{
    public AudioClip ground;
    private GameObject player;
    private GameObject enemy;
    public bool WinOnce1;
    public bool WinOnce2;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        enemy = GameObject.Find("enemy");
    }


    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag ==("Ball"))
        {
            SoundController.instance.Play(ground);
            BombHit(other);
            Destroy(other.gameObject,.3f);
        }
    }

    void BombHit(Collision2D aBall)
    {
        if(aBall.transform.position.x<0&&Mathf.Abs(aBall.transform.position.x-player.transform.position.x)<4f)//in the range of the ball
        {
            BeforeDie(player);
            Invoke("PlayerDie",0.5f);
        }
        if(aBall.transform.position.x>0&&Mathf.Abs(aBall.transform.position.x-enemy.transform.position.x)<4f)//in the range of the ball
        {
            BeforeDie(enemy);
            Invoke("EnemyDie",0.5f);
        }
    }

    void BeforeDie(GameObject a)
    {
        a.gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeAll;
        a.GetComponent<Animator>().SetTrigger("die");
    }

    void PlayerDie()
    {
        player.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
        player.SetActive(false);
        WinOnce2 = true;
    }

    void EnemyDie()
    {
        enemy.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.FreezeRotation;
        enemy.SetActive(false);
        WinOnce1 = true;
    }
}
