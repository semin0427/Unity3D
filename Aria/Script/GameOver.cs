using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{

    public bool isOver;
    public bool isClear;

    public float PlayTime;

    public GameObject gameoverCanvas;
    public GameObject gameclearCanvas;

    GameObject Canvas;
    CsButton Button;
    void Start()
    {
        Canvas = GameObject.Find("MainCanvas");
        Button = GameObject.Find("MainCanvas").GetComponent<CsButton>();
        isOver = false;
        isClear = false;
        PlayTime = 0;
       
    }
    void Update()
    {
        
        gameoverCanvas.SetActive(false);
        gameclearCanvas.SetActive(false);

        if (CsPlayer.Energy == 0)
        {
            isOver = true;
        }
        if (isOver == true)
        {
//            gameoverCanvas.SetActive(true);
//            Time.timeScale = Time.timeScale == 0? 1:0;
            Application.LoadLevel(6);
        }
        else
        {
            gameoverCanvas.SetActive(false);
       //     Time.timeScale = 1.0f;
            PlayTime += Time.deltaTime;
            Debug.Log(PlayTime);
        }

        if (isOver == false)
        {
            if (PlayTime >= 50.0f)
            {
                isClear = true;
            }
        }
        if (isClear)
        {
            Debug.Log("Clear!");
            Destroy(Canvas);
            //Time.timeScale = 0;

            switch (Button.M_1)
            {
                case 1:
                    Application.LoadLevel(4);
                    break;
                case 2:
                    Application.LoadLevel(7);
                    break;
            }

            //}
            //if(Button.M_1 == 1)
            //    Application.LoadLevel(4);
            //else if(Button.M_1 == 2)
            //    Application.LoadLevel(7);
        }
    }

    public void ReStart()
    {
        CsPlayer.Energy = 5;
        CsPlayer.Gyohwa = 0.0f;
        CsPlayer.B_Gyohwa = false;
        CsPlayer.B_Delstate = false;
        CsPlayer.B_Colled = false;
        CsPlayer.B_GyohwaUpdate = false;
        CsPlayer.F_ColTime = 0;
        Time.timeScale = 1f;
        Application.LoadLevel(5);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(1);
        CsPlayer.Energy = 5;
        CsPlayer.Gyohwa = 0.0f;
        CsPlayer.B_Gyohwa = false;
        CsPlayer.B_Delstate = false;
        CsPlayer.B_Colled = false;
        CsPlayer.B_GyohwaUpdate = false;
        CsPlayer.F_ColTime = 0;
        Destroy(Canvas);
    }
}
