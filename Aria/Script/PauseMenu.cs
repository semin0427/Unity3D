using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public bool isPaused;

    public GameObject pauseMenuCanvas;
    public GameObject PausedMenu;
    public GameObject OptionMenu;
    GameObject Canvas;
    void Start()
    {
        Canvas = GameObject.Find("MainCanvas");
        OptionMenu.SetActive(false);
        PausedMenu.SetActive(false);
    }
	void Update () {

        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            PausedMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            PausedMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        
	}
    public void Option()
    {
        GetComponent<AudioSource>().Play();
        PausedMenu.SetActive(false);
        OptionMenu.SetActive(true);
       // Application.LoadLevel(3);
    }
    public void Option_Exit()
    {
        GetComponent<AudioSource>().Play();
        PausedMenu.SetActive(true);
        OptionMenu.SetActive(false);

    }
    public void Retry()
    {
        GetComponent<AudioSource>().Play();
        isPaused = false;
    }
    public void Pause()
    {
        //GetComponent<AudioSource>().Play();
        isPaused = !isPaused;
    }
    public void ReStart()
    {
        GetComponent<AudioSource>().Play();
        CsPlayer.Energy = 5;
        CsPlayer.Gyohwa = 0.0f;
        CsPlayer.B_Delstate = false;
        CsPlayer.B_Colled = false;
        CsPlayer.B_GyohwaUpdate = false;
        CsPlayer.F_ColTime = 0;
        Time.timeScale = 1f;
        Application.LoadLevel(5);
    }
    public void MainMenu()
    {
        GetComponent<AudioSource>().Play();
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
    public void Exit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
  
}
