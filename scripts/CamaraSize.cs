using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSize : MonoBehaviour
{

    public float devWidth = 2f;

    // Start is called before the first frame update
    void Start()//使屏幕宽度充满，高度留白
    {
        if (true)
        {
            this.GetComponent<Camera>().orthographicSize = devWidth*Screen.height/Screen.width/2;
        }
    } 

}
