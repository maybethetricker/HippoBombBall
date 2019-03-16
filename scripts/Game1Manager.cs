using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game1Manager : MonoBehaviour
{
    public AudioClip click;
    public BallController other;
    private GameObject player;
    private GameObject enemy;
    private GameObject ball;
    private Rigidbody2D ballbody;
    private GameObject[] score = new GameObject[10];
    private GameObject panel1;
    private GameObject panel2;
    private GameObject winner1;
    private GameObject winner2;
    public Button restart;
    public Button tomenu;
    private int score1 = 0;
    private int score2 = 0;
    private int GameNumber;
    private int WinNumber;
    private int ContinueWin;
    private Text TotalData;



    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        player = GameObject.Find("player");
        enemy = GameObject.Find("enemy");
        ball = GameObject.Find("bomb");
        ballbody = ball.GetComponent<Rigidbody2D>();
        panel1 = GameObject.Find("Panel1");
        panel2 = GameObject.Find("Panel2");
        winner1 = GameObject.Find("winner1");
        winner2 = GameObject.Find("winner2");
        TotalData = GameObject.Find("TotalData").GetComponent<Text>();

        int i=0;
        foreach(Transform child in panel1.transform)
        {
            score[i] = child.gameObject;
            i++;
        }
        foreach(Transform child in panel2.transform)
        {
            score[i] = child.gameObject;
            i++;
        }
        foreach(GameObject a in score)
        {
            a.SetActive(false);
        }
        GameNumber = PlayerPrefs.GetInt("TotalGame",0);
        WinNumber = PlayerPrefs.GetInt("TotalWin",0);
        ContinueWin = PlayerPrefs.GetInt("ContinueWin",0);
        SetText();
        winner1.SetActive(false);
        winner2.SetActive(false);
        restart.gameObject.SetActive(false);
        tomenu.gameObject.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        ShowScore(ref other.WinOnce1,ref other.WinOnce2);
    }


    void ShowScore(ref bool add1,ref bool add2)//计分
    {
        if(add1)
        {
            score1++;
            score[score1-1].SetActive(true);
            add1 = false;
        }
        if(add2)
        {
            score2++;
            score[4+score2].SetActive(true);
            add2 = false;
        }
        if(score1 == 5)
        {
            score1 = 0;
            score2 = 0;
            winner1.SetActive(true);
            ResetScorelist();
            WinNumber++;
            PlayerPrefs.SetInt("TotalWin",WinNumber);
            ContinueWin++;
            PlayerPrefs.SetInt("ContinueWin",ContinueWin);
            SetText();

        }
        if(score2 == 5)
        {
            score1 = 0;
            score2 = 0;
            winner2.SetActive(true);
            ResetScorelist();
            ContinueWin = 0;
            PlayerPrefs.SetInt("ContinueWin",ContinueWin);
            SetText();
        }
    }

    void ResetScorelist()
    {
        GameNumber++;
        PlayerPrefs.SetInt("TotalGame",GameNumber);
        SetText();
        foreach(GameObject singlescore in score)
        {
            singlescore.SetActive(false);
        }
        restart.gameObject.SetActive(true);
        tomenu.gameObject.SetActive(true);
        ball.SetActive(false);
        player.SetActive(false);
        enemy.SetActive(false);
        restart.onClick.AddListener(Restart);
        tomenu.onClick.AddListener(Tomenu);
    }

    void Restart()
    {
        winner1.SetActive(false);
        winner2.SetActive(false);
        ball.SetActive(true);
        other.StartBall();
        player.SetActive(true);
        enemy.SetActive(true);
        restart.gameObject.SetActive(false);
        tomenu.gameObject.SetActive(false);
        restart.onClick.RemoveListener(Restart);
        tomenu.onClick.RemoveListener(Tomenu);
        SoundController.instance.Play(click);
    }

    void Tomenu()
    {
        SceneManager.LoadScene("Menu");
        SoundController.instance.Play(click);
    }

    void SetText()
    {
        TotalData.text = "Total Game: "+GameNumber.ToString()+"   Total Win: "+WinNumber.ToString()+"   Continious Win: "+ContinueWin.ToString();
    }

}
