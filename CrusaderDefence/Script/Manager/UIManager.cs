using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{

    PlayManager pm = null;
    StageManager _parameter = null;
    string clickThings;

    TitleCameraAction cameraAct;
    public GameObject MainUI;
    public GameObject StageUI;
    public GameObject OptionUI;
    public GameObject PauseMenu;

    public GameObject InGameUI;
    public GameObject GameOverUI;
    public GameObject GameClearUI;

    public Text mGold;
    public Text mWood;

    public Text mSwordCost;
    public Text mSpearCost;
    public Text mBowCost;
    public Text mWorkCost;
    public Text mFixCost;
    public Text mHorseCost;
    public Text mCatapultCost;

    public Text mKillPoint;
    public Text enemyHP;
    public Text playTimer;
    public Text playerHP;

    public GameObject atkBtn;
    public GameObject defBtn;
    public GameObject AtkArea;
    public GameObject DefArea;

    public RectTransform healthBarP;
    public RectTransform healthBarE;

    public Sprite iLocker;
    public Sprite buttonIcon;

    public GameObject stg2btn;
    public GameObject stg3btn;
    public GameObject infinitebtn;

    public static bool isCastlePick;

    int playerMaxHP;
    int enemyMaxHP;

    Fade fader;

    float timer;
    bool isPause;

    float cachedY;
    float maxValue;
    float minValue;

    float EcachedY;
    float EmaxValue;
    float EminValue;

    public Text txtPT;
    public Text txtRT;
    LevelControl LC;

    enum MenuSelection
    {
        GameStart,
        GameStart_Back,
        Option,
        Option_Back,
        NONE
    }

    MenuSelection selectMenu;

    // Use this for initialization
    void Start()
    {
        isPause = false;
        isCastlePick = false;
        if (SoundManager.instance.myAudios3.isPlaying)
            SoundManager.instance.myAudios3.Stop();

        if (Application.loadedLevel == 1)
        {
            cameraAct = GetComponent<TitleCameraAction>();
            StageUI.SetActive(false);
            OptionUI.SetActive(false);
            selectMenu = MenuSelection.NONE;
            fader = GameObject.Find("FadeInPrefab").GetComponent<Fade>();
            Image btn1 = stg2btn.GetComponent<Image>();
            Image btn2 = stg3btn.GetComponent<Image>();
            Image btn3 = infinitebtn.GetComponent<Image>();

            stg2btn.GetComponent<Button>().enabled = false;
            stg3btn.GetComponent<Button>().enabled = false;
            infinitebtn.GetComponent<Button>().enabled = false;

            if (ResultManager.stage1Clear)
            {
                stg2btn.GetComponent<Button>().enabled = true;
                btn1.sprite = buttonIcon;
            }
            if (ResultManager.stage2Clear)
            {
                stg3btn.GetComponent<Button>().enabled = true;
                btn2.sprite = buttonIcon;
            }
            if (ResultManager.stage3Clear)
            {
                infinitebtn.GetComponent<Button>().enabled = true;
                btn3.sprite = buttonIcon;
            }
        }
        else if (Application.loadedLevel >= 2 && Application.loadedLevel != 10)
        {
            _parameter = GameObject.Find("QuestManager").GetComponent<StageManager>();
            pm = GameObject.Find("Castle").GetComponent<PlayManager>() as PlayManager;
            InGameUI.SetActive(false);
            GameOverUI.SetActive(false);
            GameClearUI.SetActive(false);
            PauseMenu.SetActive(false);

            mGold.text = "";
            mWood.text = "";
            mSwordCost.text = "5";
            mSpearCost.text = "8";
            mBowCost.text = "7";
            mWorkCost.text = "20";
            mFixCost.text = "5";

            mKillPoint.text = "";
            enemyHP.text = "";
            playTimer.text = "";
            playerHP.text = "";

            cachedY = healthBarP.transform.position.y;
            maxValue = healthBarP.transform.position.x;
            minValue = healthBarP.transform.position.x - healthBarP.rect.width;
            playerMaxHP = PlayerScript.HP;
        }
        if (Application.loadedLevel >= 3 && Application.loadedLevel != 10)
        {
            mHorseCost.text = "9";
            mCatapultCost.text = "20";
            AtkArea.SetActive(false);
            DefArea.SetActive(false);

            if (Application.loadedLevel != 9)
            {
                EcachedY = healthBarE.transform.position.y;
                EmaxValue = healthBarE.transform.position.x;
                EminValue = healthBarE.transform.position.x - healthBarE.rect.width;
                enemyMaxHP = EnemyCreater.maxEnemyHP;
            }

            if (Application.loadedLevel == 9)
            {
                LC = GameObject.Find("LoadDataManager").GetComponent<LevelControl>();
                DefArea.SetActive(true);

                txtPT.text = "40";
                txtRT.text = "10";
            }
        }

        if (Application.loadedLevel == 10)
        {

        }
    }

    void Update()
    {
        if (Application.loadedLevel == 1)
        {
            switch (selectMenu)
            {
                case MenuSelection.GameStart:
                    cameraAct.moveToAngel();
                    break;
                case MenuSelection.GameStart_Back:
                    cameraAct.BackFromStage();
                    break;
                case MenuSelection.Option:
                    cameraAct.lookWorker();
                    break;
                case MenuSelection.Option_Back:
                    cameraAct.BackFromOption();
                    break;
                case MenuSelection.NONE:
                    cameraAct.BackFromStage();
                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Application.loadedLevel > 1 && Application.loadedLevel != 10)
        {
            if (isCastlePick)
            {
                clickCastle();
            }

            HealthP();
            if (Application.loadedLevel > 2 && Application.loadedLevel != 9)
                HealthE();

            if (pm.bGameOver)
            {
                GameOver();
            }

            if (pm.bGameClear)
            {
                if (Application.loadedLevel == 2 && _parameter.tutorialNum == 15)
                {
                    _parameter.tutorialNum = 16;
                }
                else if (Application.loadedLevel == 3)
                {
                    Application.LoadLevel(6);
                }
            }

            textUpdate();

            if (isPause)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    PlayAgain();
            }
            else if (!isPause)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    PauseBtn();
            }

            if (!pm.bGameClear && !pm.bGameOver)
                timer += Time.deltaTime;
        }
    }

    void textUpdate()
    {
        mGold.text = pm.Gold.ToString();
        mWood.text = pm.Wood.ToString();
        mKillPoint.text = PlayManager.Score.ToString();
        enemyHP.text = EnemyCreater.EnemyHP + "/" + EnemyCreater.maxEnemyHP.ToString();
        playerHP.text = PlayerScript.HP.ToString() + "/" + playerMaxHP.ToString();
        playTimer.text = string.Format("{0:f1}", timer).ToString();
    }

    void clickCastle()
    {
        //좌측하단 유아이 활성화
        InGameUI.SetActive(true);
    }

    /// <summary>
    /// MainMenu
    /// </summary>

    public void StartBtn()
    {
        MainUI.SetActive(false);
        StageUI.SetActive(true);
        selectMenu = MenuSelection.GameStart;
        SoundManager.instance.SoundPlay(SoundManager.instance.ButtonClick);
    }

    public void BackFromStageMenu()
    {
        MainUI.SetActive(true);
        StageUI.SetActive(false);
        OptionUI.SetActive(false);
        selectMenu = MenuSelection.GameStart_Back;
        SoundManager.instance.SoundPlay(SoundManager.instance.ButtonClick);
    }

    public void OptionBtn()
    {
        MainUI.SetActive(false);
        StageUI.SetActive(false);
        OptionUI.SetActive(true);
        selectMenu = MenuSelection.Option;
        SoundManager.instance.SoundPlay(SoundManager.instance.ButtonClick);
    }

    public void BackFromOption()
    {
        MainUI.SetActive(true);
        StageUI.SetActive(false);
        OptionUI.SetActive(false);
        selectMenu = MenuSelection.Option_Back;
        SoundManager.instance.SoundPlay(SoundManager.instance.ButtonClick);
    }

    public void GameEXIT()
    {
        SoundManager.instance.SoundPlay(SoundManager.instance.ButtonClick);
        Application.Quit();
    }

    /// <summary>
    /// Chapter Choice
    /// </summary>
    /// <param name="StageNum"></param>
    public void StageBtn(int StageNum)
    {
        //pm.stageNum = StageNum;
        SoundManager.instance.SoundPlay(SoundManager.instance.IntroSword);

        if (StageNum == 10)
        {
            fader.SendMessage("boolChange", true);
            fader.SendMessage("setStageNum", StageNum);
        }
        else if (StageNum == 2)
        {
            fader.SendMessage("boolChange", true);
            fader.SendMessage("setStageNum", StageNum);
        }
        else if (StageNum == 3)
        {
            fader.SendMessage("boolChange", true);
            fader.SendMessage("setStageNum", StageNum);
        }
        else if (StageNum == 8)
        {
            fader.SendMessage("boolChange", true);
            fader.SendMessage("setStageNum", StageNum);
        }
    }

    public void tutorialBtn()
    {
        Application.LoadLevel(2);
    }

    public void HealthP()
    {
        float cal_health = MapValues((float)PlayerScript.HP, 0, (float)playerMaxHP, minValue, maxValue);
        healthBarP.position = new Vector3(cal_health, cachedY);
    }

    public void HealthE()
    {
        float cal_health = MapValues((float)EnemyCreater.EnemyHP, 0, (float)enemyMaxHP, EminValue, EmaxValue);
        healthBarE.position = new Vector3(cal_health, EcachedY);
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void SwordBtn()
    {
        if (Application.loadedLevel == 2)
        {
            if (_parameter.tutorialNum == 3)
            {
                _parameter.tutorialNum = 4;
            }
        }
        pm.Cost = 5;

        pm.SendMessage("UnitChoice", 1);
    }

    public void ArcherBtn()
    {
        pm.Cost = 7;

        pm.SendMessage("UnitChoice", 2);
    }

    public void SpearBtn()
    {
        pm.Cost = 8;

        pm.SendMessage("UnitChoice", 3);
    }

    public void WorkerBtn()
    {
        if (Application.loadedLevel == 2)
        {
            if (_parameter.tutorialNum == 10)
            {
                _parameter.tutorialNum = 11;
            }
        }
        pm.Cost = 20;

        pm.SendMessage("UnitChoice", 4);
    }

    public void HorseBtn()
    {
        pm.Cost = 9;

        pm.SendMessage("UnitChoice", 5);
    }

    public void CatapultultBtn()
    {
        pm.Cost = 20;

        pm.SendMessage("UnitChoice", 6);
    }

    public void FixBtn()
    {
        if (pm.Wood >= 5 && PlayerScript.HP < 50)
        {
            pm.Wood -= 5;
            PlayerScript.HP += 5;

            if (Application.loadedLevel == 2)
            {
                if (_parameter.tutorialNum == 12)
                {
                    _parameter.tutorialNum = 13;
                }
            }
        }
    }

    public void ATKBtn()
    {
        pm.SendMessage("setPosition", true);
        AtkArea.SetActive(true);
        DefArea.SetActive(false);

        if (Application.loadedLevel == 3)
            if (_parameter.tutorialNum == 10)
                _parameter.tutorialNum = 11;
    }

    public void DEFBtn()
    {
        pm.SendMessage("setPosition", false);
        AtkArea.SetActive(false);
        DefArea.SetActive(true);
    }

    public void PauseBtn()
    {
        isPause = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        isPause = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);
        PauseMenu.SetActive(false);
        Application.LoadLevel(1);
        Time.timeScale = 1.0f;
    }

    public void ToMain()
    {
        Application.LoadLevel(1);
    }

    public void EXIT()
    {
        Application.Quit();
    }

    public void Restart()
    {
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(false);
        PauseMenu.SetActive(false);
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1.0f;
    }

    public void NextStage()
    {
        PauseMenu.SetActive(false);
    }

    void GameOver()
    {
        if (Application.loadedLevel > 1)
        {
            if (Application.loadedLevel == 9)
                GameObject.Find("game_over").GetComponent<Fade>().FadeIn4Infinity();
            else
                GameObject.Find("game_over").GetComponent<Fade>().FadeIn2Main();

            if (!SoundManager.instance.myAudios3.isPlaying)
                SoundManager.instance.myAudios3.Play();
        }
        //   Time.timeScale = 0.0f;
    }

    void GameClear()
    {
        GameOverUI.SetActive(false);
        GameClearUI.SetActive(true);
        PauseMenu.SetActive(false);

        Time.timeScale = 0.0f;
    }

    public void SoundOn()
    {
        SoundManager.instance.myAudios.mute = false;
        SoundManager.instance.SoundPlay(SoundManager.instance.MainBgm);
    }

    public void SoundOff()
    {
        SoundManager.instance.myAudios.Stop();
        SoundManager.instance.myAudios.mute = true;
    }
}
