using UnityEngine;
using System.Collections;

public class GameStatus : MonoBehaviour {
    
    // 철광석, 식물을 사용했을 때 각각의 수리 정도.
    public static float GAIN_REPAIRMENT_IRON = 0.30f;
    public static float GAIN_REPAIRMENT_PLANT = 0.10f;
    // 철광석, 사과, 식물을 운반했을 때 각각의 체력 소모 정도.
    public static float CONSUME_SATIETY_IRON = 0;//0.2f;
    public static float CONSUME_SATIETY_APPLE = 0;// 0.1f;
    public static float CONSUME_SATIETY_PLANT = 0;// 0.1f;
    // 사과, 식물을 먹었을 때 각각의 체력 회복 정도.
    public static float REGAIN_SATIETY_APPLE = 0.7f;
    public static float REGAIN_SATIETY_PLANT = 0.3f;
    public float repairment = 0.0f; // 우주선의 수리 정도(0.0f~1.0f).
    public float satiety = 1.0f; // 배고픔,체력(0.0f~1.0f).
    public float lonely = 0.0f; // 외로워
    public GUIStyle guistyle; // 폰트 스타일.
    public static float CONSUME_SATIETY_ALWAYS = 0.01f;//0.03f;
    public static float CONSUME_LONELY_FAR_FROM_WILLSON = 0.01f;

    public GameObject[] boatParts = null;
    public int repairCount;

    public int fireCount;
    public bool bCamping = false; //
    public bool getFire = false;
    private DayTime dt;

    public GameObject CampFirePrefab;
    public GameObject FireSpotPrefab;

    private GameObject CampFire;
    private GameObject FireSpot;

    private CsWillson myFriend;
    private int fireFuncCount = 0;
    private int campPosFuncCount = 0;

    void Start()
    {
        this.guistyle.fontSize = 24; // 폰트 크기를 24로.
        repairCount = 0;
        fireCount = 0;
        dt = GameObject.Find("GameRoot").GetComponent<DayTime>();
        myFriend = GameObject.Find("WillsonTable").GetComponent<CsWillson>();

        // CampFire.transform.Translate(new Vector3(0, -50, 0));
        foreach (var part in boatParts)
            part.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (fireCount > 0)
            bCamping = true;
        else if(fireCount <=0)
        {
            fireCount = 0;
            bCamping = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
            if (fireCount == 0)
                fireCount = 1;
            else
                fireCount = 0;

        if (bCamping)
        {
            campPosFuncCount += 1;
            fireFuncCount = 0;

            if(campPosFuncCount == 1)
                CampFire = Instantiate(CampFirePrefab) as GameObject;

            if (FireSpot!=null)
                Destroy(FireSpot);
        }
        else
        {
            fireFuncCount += 1;
            campPosFuncCount = 0;

            if(fireFuncCount == 1)
                FireSpot = Instantiate(FireSpotPrefab) as GameObject;

            if (CampFire != null)
                Destroy(CampFire);
        }

        boatGenerate();
    }

    void OnGUI()
    {
        float x = Screen.width * 0.2f;
        float y = 20.0f;
        // 체력을 표시.
        GUI.Label(new Rect(x, y, 200.0f, 20.0f), "체력:" +
        (this.satiety * 100.0f).ToString("000"), guistyle);
        x += 200;

        GUI.Label(new Rect(x, y, 200.0f, 20.0f), "외로움:" +
        (this.lonely * 100.0f).ToString("000"), guistyle);
        x += 200;

        // 수리 정도를 표시.
        GUI.Label(new Rect(x, y, 200.0f, 20.0f),
        "땟목 :" + (this.repairment * 100.0f).ToString("000"), guistyle);
        x += 200;

        GUI.Label(new Rect(x, y, 200.0f, 20.0f), "윌슨:" +
        (myFriend.HP).ToString("0"), guistyle);
        x += 200;

        GUI.Label(new Rect(x, y, 200.0f, 20.0f), "장작:" +
        (fireCount).ToString("0"), guistyle);
        x += 200;
    }
    // 우주선 수리를 진행
    public void addRepairment(float add)
    {
        this.repairment = Mathf.Clamp01(this.repairment + add); // 0.0~1.0 강제 지정
    }
    // 체력을 늘리거나 줄임
    public void addSatiety(float add)
    {
        this.satiety = Mathf.Clamp01(this.satiety + add);
    }
    // 외로움을 늘리거나 줄임
    public void addLonely(float add)
    {
        this.lonely = Mathf.Clamp01(this.lonely + add * Time.deltaTime);
    }

    // 게임을 클리어했는지 검사
    public bool isGameClear()
    {
        bool is_clear = false;
        if (this.repairment >= 1.0f)
        { // 수리 정도가 100% 이상이면.
            is_clear = true; // 클리어했다.
        }
        return (is_clear);
    }
    // 게임이 끝났는지 검사
    public bool isGameOver()
    {
        bool is_over = false;
        if (this.satiety <= 0.0f || lonely >= 1.0f)
        { // 체력이 0이하라면.
            is_over = true; // 게임 오버.
        }
        if (dt.am == false) // night and no fire
        {
            if (bCamping == false)
                is_over = true;
        }
        if (myFriend.HP <= 0)
            is_over = true;

        return (is_over);
    }

    // 배를 고프게 하는 메서드 추가
    public void alwaysSatiety()
    {
        this.satiety = Mathf.Clamp01(this.satiety - CONSUME_SATIETY_ALWAYS * Time.deltaTime);
    }

    // 외롭게 하는 메서드 추가
    public void soLonely()
    {
        this.lonely = Mathf.Clamp01(this.lonely + CONSUME_LONELY_FAR_FROM_WILLSON * Time.deltaTime);
    }

    public void boatGenerate()
    {
        for(int i = 0; i<10; i++)
        {
            if (repairCount == i)
            {
                for (int j = 9; j < 0; j--)
                {
                    boatParts[j].SetActive(false);
                }
                for(int j = 0; j< i; j++)
                {
                    boatParts[j].SetActive(true);
                }
            }
        }
    }
}
