using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewModeManager : MonoBehaviour
{
    public AudioClip click;
    public ballHitBround2 aBall;
    public GameObject instance;
    private GameObject player;
    private GameObject enemy;
    private GameObject winner1;
    private GameObject winner2;
    public Button restart;
    public Button tomenu;
    private int deltacounter;
    public int BombNumber = 0;    
    private Vector2 StartPosition = new Vector2 (-0.2f,3f);
    private bool InGame = true;
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
        winner1 = GameObject.Find("winner1");
        winner2 = GameObject.Find("winner2");
        TotalData = GameObject.Find("TotalData").GetComponent<Text>();
        GameNumber = PlayerPrefs.GetInt("TotalGame",0);
        WinNumber = PlayerPrefs.GetInt("TotalWin",0);
        ContinueWin = PlayerPrefs.GetInt("ContinueWin",0);
        TotalData.text = "Tip: Don't let the ball hit the ground near you";
        Invoke("SetText",3f);

        winner1.SetActive(false);
        winner2.SetActive(false);
        restart.gameObject.SetActive(false);
        tomenu.gameObject.SetActive(false);

        deltacounter = 0;
        Instantiate(instance,StartPosition,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        BombCopy();
        WinTest();
    }

    void BombCopy()
    {
        deltacounter++;
        if(deltacounter == 200&&InGame)
        {
            Instantiate(instance,StartPosition,Quaternion.identity);
            deltacounter = 0;
            BombNumber++;
            
        }
    }


    void WinTest()
    {
        if(aBall.WinOnce1&&InGame)
        {
            InGame = false;
            winner1.SetActive(true);
            WinNumber++;
            PlayerPrefs.SetInt("TotalWin",WinNumber);
            ContinueWin++;
            PlayerPrefs.SetInt("ContinueWin",ContinueWin);
            SetText();
            ToRestart();

        }
        if(aBall.WinOnce2&&InGame)
        {
            InGame = false;
            winner2.SetActive(true);
            ContinueWin = 0;
            PlayerPrefs.SetInt("ContinueWin",ContinueWin);
            SetText();
            ToRestart();
        }
    }

    void ToRestart()
    {
        GameNumber++;
        PlayerPrefs.SetInt("TotalGame",GameNumber);
        SetText();
        restart.gameObject.SetActive(true);
        tomenu.gameObject.SetActive(true);
        deltacounter = 201;
        player.SetActive(false);
        enemy.SetActive(false);
        restart.onClick.AddListener(Restart);
        tomenu.onClick.AddListener(Tomenu);
    }

    void Restart()
    {
        aBall.WinOnce2 = false;
        aBall.WinOnce1 = false;
        InGame = true;
        winner1.SetActive(false);
        winner2.SetActive(false);
        deltacounter = 190;
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
