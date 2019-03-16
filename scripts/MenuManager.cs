using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance = null;
    public AudioClip click;
    private GameObject title;

    public Button start;
    public Button CrazyMode;


    void InitGame()
    {
        DontDestroyOnLoad(gameObject);
        start.onClick.AddListener(StartGame);
        CrazyMode.onClick.AddListener(ToCrazymode);

        title = GameObject.Find("title");
        


    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        InitGame();
        title.SetActive(true);
        start.gameObject.SetActive(true);
        CrazyMode.gameObject.SetActive(true);
    }

    void StartGame()
    {
        SceneManager.LoadScene("game1");
        title.SetActive(false);
        start.gameObject.SetActive(false);
        CrazyMode.gameObject.SetActive(false);
        SoundController.instance.Play(click);
    }

    void ToCrazymode()
    {
        SceneManager.LoadScene("New Mode");
        title.SetActive(false);
        start.gameObject.SetActive(false);
        CrazyMode.gameObject.SetActive(false);
        SoundController.instance.Play(click);
    }



}
