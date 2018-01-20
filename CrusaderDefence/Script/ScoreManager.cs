using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public int s_Score;
    public float s_Time;

    public GameObject timeBoard;
    public GameObject pointBoard;
    public GameObject TotalScoreBoard;

    Text time;
    Text point;
    Text total;

    public static ScoreManager instance;

    float totalScore;
    // Use this for initialization
    void Awake() 
    {
        if (ScoreManager.instance == null)
            ScoreManager.instance = this; 

        DontDestroyOnLoad(this.gameObject);
    }

    void start()
    {
    }
    // Update is called once per frame
    void Update () {
        if (Application.loadedLevel == 1)
            Destroy(this.gameObject);

        if (Application.loadedLevel < 9)
        {
            initScore();
        }
        if (Application.loadedLevel == 9)
        {
            getScore();
            setTotalScore();
        }
        if (Application.loadedLevel == 10)
        {
            timeBoard = GameObject.Find("timeTxt");
            pointBoard = GameObject.Find("PointTxt");
            TotalScoreBoard = GameObject.Find("totalTxt");

            time = instance.timeBoard.GetComponentInChildren<Text>();
            point = instance.pointBoard.GetComponentInChildren<Text>();
            total = instance.TotalScoreBoard.GetComponentInChildren<Text>();

            time.text = string.Format("{0:f1}", s_Time).ToString();
            point.text = s_Score.ToString();
            total.text = string.Format("{0:f1}", totalScore).ToString();
        }
    }

    void initScore()
    {
        s_Score = 0;
        s_Time = 0;
    }

    void getScore()
    {
        s_Score = PlayManager.Score;

        if(!GameObject.Find("Castle").GetComponent<PlayManager>().bGameOver)
            s_Time += Time.deltaTime;
    }

    void setTotalScore()
    {
        totalScore = s_Score + (s_Time * 10.0f);
    }
}
