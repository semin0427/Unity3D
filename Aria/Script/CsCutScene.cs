using UnityEngine;
using System.Collections;

public class CsCutScene : MonoBehaviour {
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
    public int iSceneNum = 0; //페이드 반복횟수용

    private bool sceneStarting = true;      // Whether or not the scene is still fading in.

    public float next = 0;
    public float ChangeImage = 10;
        
    public float fTime = 0;
    public int i = 0;

    //    public bool Fade = false;
    public GameObject Intro01;
    public GameObject Intro02;
    public GameObject Intro03;
    public GameObject Intro04;
    public GameObject Intro05;
    public GameObject Intro06;
    public GameObject Intro07;
    public GameObject Intro08;    

    public GameObject Main;
    //스테이지 클리어 체크
    static public bool ClearS1 = false;

    void Awake()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        next = 0;
    }

    void Start()
    {
        Intro01.SetActive(false);
        Intro02.SetActive(false);
        Intro03.SetActive(false);
        Main.SetActive(false);
       
    }

    public int count = 0;

    void Update()
    {
        fTime += Time.deltaTime;
        ClearS1 = true;
        if (fTime < ChangeImage)
            Scene01(); //StartCoroutine("Scene01");
        else if (fTime >= ChangeImage && fTime < ChangeImage*2)
            Scene02(); // StartCoroutine("Scene02");
        else if (fTime >= ChangeImage*2 && fTime < ChangeImage*3)
            Scene03(); // StartCoroutine("Scene02");
        else if (fTime >= ChangeImage*3)
            GameStart(); // StartCoroutine("GameStart");
        
    }

    void Fade()
    {
        next += Time.deltaTime;
 //       if (iSceneNum >= i)
        {
            if (next < ChangeImage*0.7)
                StartScene();
            else if (next >= ChangeImage*0.7 && next < ChangeImage)
                EndScene(); // StartCoroutine("EndScene");
            else
                next = 0;
      
        }
        Debug.Log(next);
    }


    // Update is called once per frame
    void Scene01()
    {
    //    yield return new WaitForSeconds(0);
        Intro01.SetActive(true);
        Intro02.SetActive(false);
        Intro03.SetActive(false);
        Main.SetActive(false);
        Fade();
    }

    void Scene02()
    {
   //     yield return new WaitForSeconds(5);
        Intro01.SetActive(false);
        Intro02.SetActive(true);
        Intro03.SetActive(false);
        Main.SetActive(false);
        Fade();
    }

    void Scene03()
    {
   //     yield return new WaitForSeconds(10);
        Intro01.SetActive(false);
        Intro02.SetActive(false);
        Intro03.SetActive(true);
        Main.SetActive(false);
        Fade();
    }

    void GameStart()
    {
   //     yield return new WaitForSeconds(15);
        Intro01.SetActive(false);
        Intro02.SetActive(false);
        Intro03.SetActive(false);
        Main.SetActive(true);
        StartScene();
    }

    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void FadeToWhite()
    {
        GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.white, fadeSpeed * Time.deltaTime);
    }

    /// <페이드 체크>
    public bool FadeIn = false;
    public int FadeCount = 0; //페이드 반복횟수용
    /// </summary>

    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

              // If the texture is almost clear...
              if (GetComponent<GUITexture>().color.a <= 0.05f)
              {
                  // ... set the colour to clear and disable the GUITexture.
                  GetComponent<GUITexture>().color = Color.clear;
                  GetComponent<GUITexture>().enabled = false;
                  // The scene is no longer starting.
                  //    sceneStarting = false;
              }
    }

    public int term = 0;

    public void EndScene()
    {
  //      yield return new WaitForSeconds(term);
        // Make sure the texture is enabled.
        GetComponent<GUITexture>().enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        if (GetComponent<GUITexture>().color.a >= 0.95f)
        {
            // ... reload the level.
            GetComponent<GUITexture>().color = Color.clear;
            GetComponent<GUITexture>().enabled = true;
        }
       // Application.LoadLevel(iSceneNum);
    }
}
