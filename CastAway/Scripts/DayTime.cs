using UnityEngine;
using System.Collections;

public class DayTime : MonoBehaviour {

    public TextAsset levelData = null;

    float _dayTime;
    float _nightTime;

    float dayTerm = 5;
    float nightTerm = 5;

    public bool am;

    GameObject ambientLight;
    GameObject playerLight;

    public LevelControl LC = null;

    public Material daySky;
    public Material nightSky;

    GameStatus gs;
    SceneControl sc;

	// Use this for initialization
	void Start () {
        ambientLight = GameObject.Find("Directional Light");
        gs = GameObject.Find("GameRoot").GetComponent<GameStatus>();
        //  playerLight = GameObject.Find("Spotlight");
        LC = new LevelControl();
        LC.initialize();
        LC.loadLevelData(levelData);
        sc = GameObject.Find("GameRoot").GetComponent<SceneControl>();
        RenderSettings.skybox = daySky;

       // playerLight.SetActive(false);
        am = true;
    }
	
	// Update is called once per frame
	void Update () {

        LC.selectLevel();
        
        if (am)
        {
            RenderSettings.skybox = daySky;
            _nightTime = 0;
            _dayTime += Time.deltaTime;
            if (_dayTime > LC.getDaytime())
            {
                sc.ChangeTime = LC.getNighttime();
                gs.fireCount -= 1;
                am = false;
            }
        }

        if (!am)
        {
            RenderSettings.skybox = nightSky;
            _dayTime = 0;
            _nightTime += Time.deltaTime;
            if (_nightTime > LC.getNighttime())
            {
                sc.ChangeTime = LC.getDaytime();
                LC.select_level += 1;
                gs.fireCount -= 1;
                gs.getFire = false;
                am = true;
            }
        }

        changeLight();
    }

    void changeLight()
    {
        if(am)
        {
            ambientLight.SetActive(true);
        }
        else
        {
            ambientLight.SetActive(false);
        }
    }
}
