using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManger : MonoBehaviour {

    public Text scoreTxt;
    public Text moveDistance;
    public Text coin;

    public Text tTime;
    public Text tScore;
    public Text totalScore;

    GameRoot GR;

    float scoreTime;
    public static int score;
    public static float fTime;
	// Use this for initialization
	void Start () {
        if(Application.loadedLevel == 1)
            GR = GameObject.Find("GameRoot").GetComponent<GameRoot>();

        PlayerControl.bulletCount = 0;
        scoreTime = 0;
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevel == 1)
        {
            fTime = GR.getPlayTime();
            moveDistance.text = "Time : " + string.Format("{0:f1}", GR.getPlayTime()).ToString() + "s";
            coin.text = "Bullet : " + PlayerControl.bulletCount.ToString();
            scoreTxt.text = "Score : " + GetScore().ToString();
        }

        if (Application.loadedLevel == 2)
        {
            tScore.text = dataSave.iScore.ToString();
            tTime.text = string.Format("{0:f1}", dataSave.fTime).ToString();
            float ts = (dataSave.iScore * dataSave.fTime);
            totalScore.text = string.Format("{0:f1}", ts).ToString();
        }
    }

    int GetScore()
    {
        scoreTime += Time.deltaTime;
        if(scoreTime >= 10.0f)
        {
            score += 100;
            scoreTime = 0;
        }

        if (PlayerControl.getScore)
            score += 10;

        return score;
    }

    public void ToMain()
    {
        Application.LoadLevel(0);
    }

    public void Restart()
    {
        Application.LoadLevel(1);
    }
}
