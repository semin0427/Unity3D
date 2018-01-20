using UnityEngine;
using System.Collections;

public class TutorialPlay : MonoBehaviour {

    public GameObject menualUI;
    public GameObject[] menualTxt;

    public GameObject clickUI;
    public GameObject[] idles;
    public GameObject[] lCliks;

    public GameObject npcUI;
    public GameObject[] npcTxt;

    public static int chapterNum;
    public static bool bMouseUI;
    public float speachTerm;

    public static bool nextChapter;
    public static bool nextTutorial;

    public GameObject blindBtn;
    bool temp;

    // Use this for initialization
    void Start () {
        chapterNum = 1;
        bMouseUI = false;
        speachTerm = 0;
        nextChapter = false;
        nextTutorial = false;
        temp = false;

        menualUI.SetActive(false);
        npcUI.SetActive(false);
        clickUI.SetActive(false);

        foreach (var npcTxts in npcTxt)
            npcTxts.SetActive(false);

        foreach (var manualTxts in menualTxt)
            manualTxts.SetActive(false);

        foreach (var idle in idles)
            idle.SetActive(false);

        foreach (var lClick in lCliks)
            lClick.SetActive(false);

        blindBtn.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        switch (chapterNum)
        {
            case 1:
                chapter1();
                break;
            case 2:
                chapter2();
                break;
            case 3:
                chapter3();
                break;
            case 4:
                chapter4();
                break;
            case 5:
                chapter5();
                break;
            case 6:
                chapter6();
                break;
            case 7:
                chapter7();
                break;
            case 8:
                chapter8();
                break;
            case 9:
                chapter9();
                break;
            case 10:
                chapter10();
                break;
            case 11:
                chapter11();
                break;
            default:
                break;
        }
	}

    void npcTxtDisaper(int num)
    {
        npcTxt[num].SetActive(false);
    }

    void manualTxtDisapear(int num)
    {
        menualTxt[num].SetActive(false);
    }

    void mouseUIDisapear(int num)
    {
        idles[num].SetActive(false);
        lCliks[num].SetActive(false);
    }

    void changeDialogue(float TermTime, int sentence, int startNum, int nextChapter)
    {
        speachTerm += Time.deltaTime;

        for (int i = 0; i < sentence; i++)
        {
            if (speachTerm >= i*TermTime && speachTerm < (i+1) + TermTime)
            {
                if(i == 0)
                    npcTxt[startNum + i].SetActive(true);
                else if(i == 1)
                {
                    npcTxtDisaper(startNum + i - 1);
                    npcTxt[startNum + i].SetActive(true);
                }
            }
        }

        if (speachTerm > sentence * TermTime)
        {
            speachTerm = 0;
            clickUI.SetActive(false);
            if (chapterNum == 10)
                temp = true;

            chapterNum = nextChapter;
        }
    }

    void chapter1()
    {
        npcUI.SetActive(true);
        changeDialogue(2.0f, 2, 0, 2);
    }

    void chapter2()
    {
        npcUI.SetActive(false);
        menualUI.SetActive(true);
        menualTxt[0].SetActive(true);
        clickUI.SetActive(true);
        mouseTwinkle.twinkle(0, idles, lCliks);

        npcTxtDisaper(0);
        npcTxtDisaper(1);

        bMouseUI = true;

        if (nextChapter == true)
        {
            menualTxt[0].SetActive(false);
            menualUI.SetActive(false);
            mouseUIDisapear(0);

            chapterNum = 3;
        }
    }

    void chapter3()
    {
        nextChapter = false;
        mouseTwinkle.twinkle(1, idles, lCliks);

        if(nextTutorial == true)
        {
            mouseUIDisapear(1);
            chapterNum = 4;   
        }
            
    }

    void chapter4()
    {
        Debug.Log("tileclick");
        nextTutorial = false;
        mouseTwinkle.twinkle(2, idles, lCliks);

        if (nextChapter == true)
        {
            mouseUIDisapear(2);
            nextTutorial = true;
        }
    }

    void chapter5()
    {
        npcUI.SetActive(true);
        npcTxt[2].SetActive(true);
        blindBtn.SetActive(true);
        if (PlayerScript.HP<50)
        {
            blindBtn.SetActive(false);
            manualTxtDisapear(1);
            manualTxtDisapear(2);
            npcTxtDisaper(2);
            chapterNum = 6;
        }
    }

    void chapter6()
    {
        Debug.Log("chap6");

        menualUI.SetActive(true);
        menualTxt[1].SetActive(true);
        npcTxt[3].SetActive(true);

        if (nextChapter == true)
        {
            npcTxtDisaper(3);
            npcTxt[4].SetActive(true);
            speachTerm += Time.deltaTime;
            menualTxt[1].SetActive(false);
            menualUI.SetActive(false);

            if (speachTerm > 2.0f)
            {
                npcUI.SetActive(false);
                npcTxtDisaper(4);
                nextChapter = false;
                chapterNum = 7;
            }
        }
    }

    void chapter7()
    {
        speachTerm = 0;
        menualUI.SetActive(true);
        menualTxt[2].SetActive(true);
        mouseTwinkle.twinkle(3, idles, lCliks);

        if(nextTutorial == true)
        {
            menualUI.SetActive(false);
            menualTxt[2].SetActive(false);
            mouseUIDisapear(3);
            chapterNum = 8;
        }
    }

    void chapter8()
    {
        mouseTwinkle.twinkle(4, idles, lCliks);

        if (nextChapter)
        {
            mouseUIDisapear(4);
        }
    }

    void chapter9()
    {
        mouseTwinkle.twinkle(5, idles, lCliks);
        speachTerm = 0;
        if (nextTutorial == true)
        {
        }
    }

    void chapter10()
    {
        mouseUIDisapear(5);
        clickUI.SetActive(false);
        npcUI.SetActive(true);
        changeDialogue(2.0f, 2, 5, 11);
        if (temp || Input.GetMouseButton(0))
            chapterNum = 11;
    }


    void chapter11()
    {
        npcUI.SetActive(false);
        menualUI.SetActive(true);
        menualTxt[3].SetActive(true);
        Debug.Log("finish");
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("ggg");
            menualUI.SetActive(false);
            clickUI.SetActive(false);
            Application.LoadLevel(0);
        }
    }
}
