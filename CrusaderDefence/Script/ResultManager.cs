using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    public GameObject HorseMan;
    public GameObject Bow;
    public GameObject SwordMan;

    public GameObject HorseSaid;
    public GameObject BowSaid;
    public GameObject SwordSaid;

    GameObject Camera;

    public GameObject[] BlueTeam;
    public GameObject[] RedTeam;

    int RedAni;
    int BlueAni;
    int stageResultNum;
    float SceneTime;

    public static bool stage1Clear = false;
    public static bool stage2Clear = false;
    public static bool stage3Clear = false;

    // Use this for initialization
    void Start () {
        SoundManager.instance.myAudios.Stop();
        SoundManager.instance.myAudios2.Stop();
        Camera = GameObject.Find("Main Camera");
        BlueAni = 0;
        RedAni = 0;
        stageResultNum = 0;
        SceneTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
	    switch(Application.loadedLevel)
        {
            case 5:
                stage1Clear = true;
                if (!SoundManager.instance.myAudios.isPlaying)
                    SoundManager.instance.SoundPlay(SoundManager.instance.HorseEFS);
                if (!SoundManager.instance.myAudios2.isPlaying)
                    SoundManager.instance.SoundPlay2(SoundManager.instance.ShoutEFS);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SoundManager.instance.myAudios2.Stop();
                    Application.LoadLevel(1);
                }
                break;
            case 6:
                stage2Clear = true;
                if (Input.GetKeyDown(KeyCode.Space) && stageResultNum < 6)
                {
                    stageResultNum += 1;
                }
                if(stageResultNum == 2)
                {
                    if (Bow.transform.position.x >= 1.20f)
                    {
                        stageResultNum += 1;
                    }
                }
                Stage2Result(stageResultNum);
                break;
            case 7:
                stage3Clear = true;
                if (Input.GetKeyDown(KeyCode.Space) && stageResultNum != 0 && stageResultNum != 3)
                {
                    stageResultNum += 1;
                }
                Stage3Result(stageResultNum);
                break;
        }
	}

    void Stage2Result(int _SceneNum)
    {
        foreach (GameObject blue in BlueTeam)
        {
            blue.GetComponent<Animator>().SetInteger("AnimationNum", BlueAni);
        }
        foreach (GameObject red in RedTeam)
        {
            red.GetComponent<Animator>().SetInteger("AnimationNum", RedAni);
        }
        switch (_SceneNum)
        {
            case 0:
                SceneTime += Time.deltaTime;
                if (!SoundManager.instance.myAudios.isPlaying)
                {
                    SoundManager.instance.SoundPlay(SoundManager.instance.IntroBgm);
                }

                Bow.SetActive(false);
                BowSaid.SetActive(false);
                HorseSaid.SetActive(false);
                if (SceneTime > 3.0f)
                    stageResultNum = 1;
                break;
            case 1:
                SceneTime = 0;
                SoundManager.instance.myAudios.Stop();
                if (!SoundManager.instance.myAudios2.isPlaying)
                {
                    SoundManager.instance.SoundPlay2(SoundManager.instance.ShoutEFS);
                }
                RedAni = 1;
                BlueAni = 2;
                HorseMan.GetComponent<Animator>().SetInteger("AnimationNum", 2);
                HorseSaid.SetActive(true);
                HorseSaid.GetComponentInChildren<Text>().text = "돈키호테 중령\n\n승전보를 울려라!\n"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 2:
                SoundManager.instance.myAudios2.Stop();
                if (!SoundManager.instance.myAudios.isPlaying)
                {
                    SoundManager.instance.SoundPlay(SoundManager.instance.MainBgm);
                }
                Bow.SetActive(true);
                Bow.transform.position = Vector3.MoveTowards(Bow.transform.position, new Vector3(1.21f,0,-3.9f), 3.0f * Time.deltaTime);
                Bow.transform.LookAt(HorseMan.transform);
                
                BowSaid.SetActive(true);
                HorseSaid.SetActive(false);
                BowSaid.GetComponentInChildren<Text>().text = "보우 하사\n\n중령님!\n"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 3:
                Bow.GetComponent<Animator>().SetBool("atHorse", true);
                Bow.transform.position = new Vector3(1.21f, 0, -3.9f);
                Camera.transform.position = new Vector3(2.25f, 2.09f, -6.85f);
                foreach (GameObject red in RedTeam)
                    red.SetActive(false);

                HorseSaid.SetActive(true);
                BowSaid.SetActive(false);
                HorseSaid.GetComponentInChildren<Text>().text = "돈키호테 중령\n\n무슨일인가!\n"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 4:
                HorseSaid.SetActive(false);
                BowSaid.SetActive(true);
                BowSaid.GetComponentInChildren<Text>().text = "보우 하사\n\n적의 근거지를 발견했습니다!\n"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 5:
                HorseSaid.SetActive(true);
                BowSaid.SetActive(false);
                HorseSaid.GetComponentInChildren<Text>().text = "돈키호테 중령\n\n그곳에서 모든것이 결정나겠군..\n가자 출정이다!"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 6:
                HorseSaid.SetActive(false);
                BowSaid.SetActive(false);
                SceneTime += Time.deltaTime;
                Camera.transform.position = new Vector3(-1.3f, 4.68f, -11.17f);
                HorseMan.transform.position = Vector3.MoveTowards(HorseMan.transform.position, new Vector3(-30.0f, 0, -1.684f),3*Time.deltaTime);
                HorseMan.GetComponent<Animator>().SetBool("goToHorse", true);
                foreach (GameObject blue in BlueTeam)
                {
                    blue.transform.LookAt(HorseMan.transform);
                    if (Vector3.Distance(blue.transform.position, HorseMan.transform.position) > 5)
                    {
                        blue.GetComponent<Animator>().SetBool("goToHorse", true);
                        blue.transform.position = Vector3.MoveTowards(blue.transform.position, HorseMan.transform.position, 2 * Time.deltaTime);
                    }
                    else
                        blue.GetComponent<Animator>().SetBool("goToHorse", false);
                }
                Bow.GetComponent<Animator>().SetBool("goToHorse", true);
                Bow.transform.LookAt(HorseMan.transform);
                if (Vector3.Distance(Bow.transform.position, HorseMan.transform.position) > 3)
                {
                    Bow.transform.position = Vector3.MoveTowards(Bow.transform.position, HorseMan.transform.position, 2 * Time.deltaTime);
                    Bow.GetComponent<Animator>().SetBool("goToHorse", true);
                }
                else
                    Bow.GetComponent<Animator>().SetBool("goToHorse", false);
                //fadeout
                if(SceneTime > 1.0f)
                    GameObject.Find("FadeInPrefab").GetComponent<Fade>().FadeOut2Main();
                break;
            default:
                GameObject.Find("FadeInPrefab").GetComponent<Fade>().FadeOut2Main();
                break;

        }
    }

    void Stage3Result(int _SceneNum)
    {
        HorseMan.GetComponent<Animator>().SetInteger("AnimationNum", BlueAni);
        SwordMan.GetComponent<Animator>().SetInteger("AnimationNum", BlueAni);
        Bow.GetComponent<Animator>().SetBool("result3", true);

        switch (_SceneNum)
        {
            case 0:
                Time.timeScale = 0.3f;
                SceneTime += Time.deltaTime;
                if (!SoundManager.instance.myAudios.isPlaying)
                {
                    SoundManager.instance.SoundPlay(SoundManager.instance.IntroBgm);
                }

                BowSaid.SetActive(false);
                HorseSaid.SetActive(false);
                SwordSaid.SetActive(false);
                if (SceneTime > 1.0f)
                {
                    SoundManager.instance.myAudios.Stop();
                    if (!SoundManager.instance.myAudios2.isPlaying)
                    {
                        SoundManager.instance.SoundPlay2(SoundManager.instance.IntroSword);
                        BlueAni = 2;
                    }
                    stageResultNum = 1;
                    SceneTime = 0;
                }
                break;
            case 1:

                Time.timeScale = 1.0f;
                SceneTime += Time.deltaTime;

                if (SceneTime > 1.0f)
                    SoundManager.instance.myAudios2.Stop();

                Bow.GetComponent<Animator>().SetBool("atHorse", true);
                HorseSaid.SetActive(true);
                HorseSaid.GetComponentInChildren<Text>().text = "돈키호테 중령\n\n끝난건가..\n"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 2:
                SceneTime = 0;
                SoundManager.instance.myAudios2.Stop();

                BowSaid.SetActive(true);
                HorseSaid.SetActive(false);
                BowSaid.GetComponentInChildren<Text>().text = "보우 하사\n\n끝이다.. 끝이야!\n"
                    + "\n                                                                                                                                      Space Bar▷▶";
                break;
            case 3:
                SceneTime += Time.deltaTime;

                if (!SoundManager.instance.myAudios.isPlaying)
                {
                    SoundManager.instance.SoundPlay(SoundManager.instance.ShoutEFS);
                }
                SwordSaid.SetActive(true);
                BowSaid.SetActive(false);
                SwordSaid.GetComponentInChildren<Text>().text = "칼 대위\n\n우리가 승리했다!!!\n";

                //fadeout
                if (SceneTime > 1.0f)
                {
                    GameObject.Find("FadeInPrefab").GetComponent<Fade>().FadeOut2Ending();
                    if(!SoundManager.instance.myAudios2.isPlaying)
                        SoundManager.instance.SoundPlay2(SoundManager.instance.MainBgm);
                }
                    break;
            
            default:
                GameObject.Find("FadeInPrefab").GetComponent<Fade>().FadeOut2Ending();
                break;

        }
    }
}
