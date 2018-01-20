using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    public float fadeInSpd;
    public float totalTime = 0;
    float alphaValue;
    bool isFading;
    int stageNum;
    // Use this for initialization
    void Start () {
        isFading = false;
    }
	
	// Update is called once per frame
	void Update () {
        alphaValue = fadeInSpd * totalTime;
        if (Application.loadedLevel == 0)
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alphaValue);
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alphaValue);
            if (isFading)
                FadeOut(stageNum);
        }
    }

    public void FadeIn()
    {
        totalTime += Time.deltaTime;

        if (alphaValue >= 1)
            Application.LoadLevel(Application.loadedLevel + 1);
    }

    public void FadeOut(int StageNum)
    {
        totalTime += Time.deltaTime;

        if (alphaValue >= 1)
        {
            Application.LoadLevel(Application.loadedLevel + StageNum);
            isFading = false;
        }
    }

    public void boolChange(bool _isChange)
    {
        isFading = _isChange;
    }

    public void setStageNum(int _stageNum)
    {
        stageNum = _stageNum;
    }

    bool a = true;

    public void introSword()
    {
        if (alphaValue > 0.7f && a)
        {
            SoundManager.instance.SoundPlay(SoundManager.instance.IntroSword);
            a = false;
        }
    }

    public void FadeOut2Main()
    {
        totalTime += Time.deltaTime;

        if (alphaValue >= 1)
        {
            Application.LoadLevel(1);
            isFading = false;
        }
    }

    public void FadeOut2Ending()
    {
        totalTime += Time.deltaTime;

        if (alphaValue >= 1)
        {
            Application.LoadLevel(8);
            isFading = false;
        }
    }

    public void FadeIn2Main()
    {
        totalTime += Time.deltaTime;

        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, alphaValue);

        if (alphaValue >= 1)
            Application.LoadLevel(1);
    }

    public void FadeIn4Infinity()
    {
        totalTime += Time.deltaTime;

        GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, alphaValue);

        if (alphaValue >= 1)
            Application.LoadLevel(10);
    }
}
