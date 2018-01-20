using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCreater : MonoBehaviour
{
    public GameObject SwordFolder;
    public GameObject HorseFolder;

    public GameObject SwordMan;
    public GameObject HorseMan;

    public GameObject ArcherFolder;
    public GameObject SpearFolder;
    public GameObject CatapultFolder;

    public GameObject BowMan;
    public GameObject SpearMan;
    public GameObject Catapult;

    public GameObject Church;

    public List<GameObject> Units;
    public List<GameObject> tutorialList;
    public ListManager lm;
    PlayerScript ps;
    GridManager gm;

    float funcCount;
    public static int maxEnemyHP;
    public static int EnemyHP;
    int currentUnitCount;

    float createTime;
    int randomSpot;
    StageManager QM;
    UIManager UM;
    GameObject[] EChurch = new GameObject[3];

    public TextAsset level_data_text = null;
    LevelControl level_control;
    float playTime;
    float levelTime;
    float restTime;

    float WAVE;
    float NEXTWAVE;
    // Use this for initialization
    void Awake()
    {
        lm = GameObject.Find("ListMgr").GetComponent<ListManager>();
        Units = lm.OffenseAtkUnit;
        tutorialList = lm.TreeList;
        playTime = 0;
        levelTime = 0;
        restTime = 0;
        ps = GameObject.Find("Castle").GetComponent<PlayerScript>();
        gm = GameObject.Find("Camera").GetComponent<GridManager>();
        QM = GameObject.Find("QuestManager").GetComponent<StageManager>();

        switch (Application.loadedLevel)
        {
            case 2:
                maxEnemyHP = 20;
                break;
            case 3:
                maxEnemyHP = 100;
                EnemyHP = 100;
                break;
            case 4:
                randomSpot = 0;
                createTime = 0;
                maxEnemyHP = 300;
                break;
            case 9:
                UM = GameObject.Find("Camera").GetComponent<UIManager>();
                WAVE = 40;
                NEXTWAVE = 10;
                level_control = new LevelControl();
                level_control.initialize();
                level_control.loadLevelData(level_data_text);
                break;
        }
    }

    void Start()
    {
        switch (Application.loadedLevel)
        {
            case 2:
                createSword(new Vector3(-10, 0, -11));
                break;
            case 3:
                createCatapult(gm.cell2Pos(800));
                gm.world[800] = GridManager.TileType.Wall;
                break;
            case 4:
                createStage4();
                break;
            case 9:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Application.loadedLevel == 2)
        {
        }
        else if (Application.loadedLevel == 3)
        {
            Stage2Update();
        }
        else if (Application.loadedLevel == 4)
        {
            Stage3Update();
        }
        else if(Application.loadedLevel == 9)
        {
            playTime += Time.deltaTime;

            if (playTime > 695)
                playTime = 695;

            level_control.update(playTime);
            //InfinityStage(level_control.regenTime(level_control.level));
            InfinityStage(level_control.playTime(level_control.level), level_control.regenTime(level_control.level), level_control.restTime(level_control.level));
        }
    }

    void Stage2Update()
    {
        if (!EChurch[0])
            EnemyHP = 0;
        else
            EnemyHP = EChurch[0].GetComponent<CsEnemyBasecamp>().hp;
    }

    void Stage3Update()
    {
        if (!EChurch[0] && !EChurch[1] && !EChurch[2])
            EnemyHP = 0;
        else
        {
            if (!EChurch[0] && !EChurch[1])
                EnemyHP = EChurch[2].GetComponent<CsEnemyBasecamp>().hp;
            else if (!EChurch[0] && !EChurch[2])
                EnemyHP = EChurch[1].GetComponent<CsEnemyBasecamp>().hp;
            else if (!EChurch[1] && !EChurch[2])
                EnemyHP = EChurch[0].GetComponent<CsEnemyBasecamp>().hp;
            else
            {
                if (!EChurch[0])
                    EnemyHP = EChurch[1].GetComponent<CsEnemyBasecamp>().hp + EChurch[2].GetComponent<CsEnemyBasecamp>().hp;
                else if (!EChurch[1])
                    EnemyHP = EChurch[0].GetComponent<CsEnemyBasecamp>().hp + EChurch[2].GetComponent<CsEnemyBasecamp>().hp;
                else if (!EChurch[2])
                    EnemyHP = EChurch[0].GetComponent<CsEnemyBasecamp>().hp + EChurch[1].GetComponent<CsEnemyBasecamp>().hp;
                else if (EChurch[0] && EChurch[1] && EChurch[2])
                    EnemyHP = EChurch[0].GetComponent<CsEnemyBasecamp>().hp + EChurch[1].GetComponent<CsEnemyBasecamp>().hp + EChurch[2].GetComponent<CsEnemyBasecamp>().hp;
            }
        }

        if (EnemyHP <= 0)
            Application.LoadLevel(7);

        if (QM.tutorialNum > 0)
        {
            createTime += Time.deltaTime;
            if (createTime > 3)
            {
                Stage4AtkEnemy();
                createTime = 0;
            }
        }
    }

    void InfinityStage(float _regenTime)
    {
        createTime += Time.deltaTime;
        if (createTime > _regenTime)
        {
            Stage4AtkEnemy();
            createTime = 0;
        }
    }

    void InfinityStage(float _LevelTime, float _RegenTime, float _RestTime)
    {
        levelTime += Time.deltaTime;
        if(_LevelTime >= levelTime)
        {
            restTime = 0;
            WAVE = _LevelTime - levelTime;
            if (WAVE < 0)
                WAVE = 0;
            Debug.Log("Create!" + _LevelTime);
            createTime += Time.deltaTime;
            if (createTime >= _RegenTime)
            {
                Stage4AtkEnemy();
                createTime = 0;
            }
        }
        else
        {
            WAVE = 0;
            Debug.Log("Rest!");
            restTime += Time.deltaTime;
            NEXTWAVE = _RegenTime - restTime;
            if (NEXTWAVE < 0)
                NEXTWAVE = 0;
            if (_RestTime <= restTime)
                levelTime = 0;
        }
        UM.txtPT.text = string.Format("{0:f1}", WAVE).ToString();
        UM.txtRT.text = string.Format("{0:f1}", NEXTWAVE).ToString();
    }

    void createEnemy01(float _delayTime) //stage1
    {
        createTime += Time.deltaTime;

        if (createTime > _delayTime)
        {
            if (currentUnitCount <= 18)
            {
                createHorse(new Vector3(0, 0, 0));
                createSword(new Vector3(-10, 0, 0));
                createTime = 0;
            }
        }
    }

    void createEnemy02(float _delayTime)
    {
        createTime += Time.deltaTime;

        if (createTime > _delayTime)
        {
            if (currentUnitCount <= 36)
            {
                createHorse(new Vector3(15, 0, 49));
                createSword(new Vector3(25, 0, 39));
                createBow(new Vector3(5, 0, 39));
                createSpear(new Vector3(35, 0, 39));
                createTime = 0;
            }
        }

        if (PlayManager.Score == 155)
            QM.tutorialNum = 5;
    }

    void createEnemy03()
    {
        if (currentUnitCount < 1)
        {
            createBow(gm.cell2Pos(4758));
            createBow(gm.cell2Pos(4760));
            createCatapult(gm.cell2Pos(4821));
            createBow(gm.cell2Pos(4762));
            createBow(gm.cell2Pos(4764));

            EChurch[0] = Instantiate(Church, gm.cell2Pos(4770), Quaternion.identity) as GameObject;

            createBow(gm.cell2Pos(4776));
            createBow(gm.cell2Pos(4778));
            createCatapult(gm.cell2Pos(4839));
            createBow(gm.cell2Pos(4780));
            createBow(gm.cell2Pos(4782));

            createHorse(gm.cell2Pos(4580));
            createHorse(gm.cell2Pos(4641));
            createHorse(gm.cell2Pos(4582));
            createHorse(gm.cell2Pos(4643));
            createHorse(gm.cell2Pos(4584));

            createHorse(gm.cell2Pos(4659));
            createHorse(gm.cell2Pos(4600));
            createHorse(gm.cell2Pos(4661));
            createHorse(gm.cell2Pos(4602));
            createHorse(gm.cell2Pos(4663));
        }
    }

    void createStage4()
    {
        //상단
        createCatapult(gm.cell2Pos(14347));
        createHorse(gm.cell2Pos(14344));
        EChurch[0] = Instantiate(Church, gm.cell2Pos(14341), Quaternion.identity) as GameObject;
        createHorse(gm.cell2Pos(14338));
        createCatapult(gm.cell2Pos(14335));

        createSpear(gm.cell2Pos(14227));
        createSword(gm.cell2Pos(14224));
        createHorse(gm.cell2Pos(14221));
        createSword(gm.cell2Pos(14218));
        createSpear(gm.cell2Pos(14215));

        createBow(gm.cell2Pos(14107));
        createSpear(gm.cell2Pos(14104));
        createSword(gm.cell2Pos(14101));
        createSpear(gm.cell2Pos(14098));
        createBow(gm.cell2Pos(14095));

        createBow(gm.cell2Pos(13987));
        createBow(gm.cell2Pos(13984));
        createBow(gm.cell2Pos(13981));
        createBow(gm.cell2Pos(13978));
        createBow(gm.cell2Pos(13975));

        //좌측
        createSword(gm.cell2Pos(7926));
        createBow(gm.cell2Pos(7924));
        createBow(gm.cell2Pos(7922));

        createBow(gm.cell2Pos(7686));
        createSpear(gm.cell2Pos(7684));
        createHorse(gm.cell2Pos(7682));

        createSpear(gm.cell2Pos(7446));
        createHorse(gm.cell2Pos(7444));
        createCatapult(gm.cell2Pos(7442));

        createHorse(gm.cell2Pos(7206));
        createSword(gm.cell2Pos(7204));
        EChurch[1] = Instantiate(Church, gm.cell2Pos(7202), Quaternion.identity) as GameObject;

        createCatapult(gm.cell2Pos(6962));
        createHorse(gm.cell2Pos(6964));
        createSpear(gm.cell2Pos(6966));

        createHorse(gm.cell2Pos(6722));
        createSpear(gm.cell2Pos(6724));
        createBow(gm.cell2Pos(6726));

        createBow(gm.cell2Pos(6482));
        createBow(gm.cell2Pos(6484));
        createSword(gm.cell2Pos(6486));

        //우측
        createSword(gm.cell2Pos(7673));
        createBow(gm.cell2Pos(7675));
        createBow(gm.cell2Pos(7677));

        createBow(gm.cell2Pos(7553));
        createSpear(gm.cell2Pos(7555));
        createHorse(gm.cell2Pos(7557));

        createSpear(gm.cell2Pos(7433));
        createHorse(gm.cell2Pos(7435));
        createCatapult(gm.cell2Pos(7437));

        createHorse(gm.cell2Pos(7313));
        createSword(gm.cell2Pos(7315));
        EChurch[2] = Instantiate(Church, gm.cell2Pos(7317), Quaternion.identity) as GameObject;

        createCatapult(gm.cell2Pos(7197));
        createHorse(gm.cell2Pos(7195));
        createSpear(gm.cell2Pos(7193));

        createHorse(gm.cell2Pos(7077));
        createSpear(gm.cell2Pos(7075));
        createBow(gm.cell2Pos(7073));

        createBow(gm.cell2Pos(6957));
        createBow(gm.cell2Pos(6955));
        createSword(gm.cell2Pos(6953));
    }

    void stage2tutorial1()
    {
        foreach (GameObject _unit in lm.OffenceDefUnit)
        {
            _unit.transform.position = new Vector3(_unit.transform.position.x, -30, _unit.transform.position.z);
        }
    }

    void stage2tutorial9()
    {
        foreach (GameObject _unit in lm.OffenceDefUnit)
            _unit.transform.position = new Vector3(_unit.transform.position.x, 0, _unit.transform.position.z);
    }

    void Stage4AtkEnemy()
    {
        randomSpot = (int)Random.Range(0, 4);
        Debug.Log(randomSpot);
        if (funcCount % 5 == 0)
        {
            switch (randomSpot)
            {
                case 0:
                    createSword(gm.cell2Pos(2904));
                    break;
                case 1:
                    createBow(gm.cell2Pos(2904));
                    break;
                case 2:
                    createSpear(gm.cell2Pos(2904));
                    break;
                case 3:
                    createHorse(gm.cell2Pos(2904));
                    break;
            }
        }
        else if (funcCount % 5 == 1)
        {
            switch (randomSpot)
            {
                case 0:
                    createSword(gm.cell2Pos(2939));
                    break;
                case 1:
                    createBow(gm.cell2Pos(2939));
                    break;
                case 2:
                    createSpear(gm.cell2Pos(2939));
                    break;
                case 3:
                    createHorse(gm.cell2Pos(2939));
                    break;
            }
        }
        else if (funcCount % 5 == 2)
        {
            switch (randomSpot)
            {
                case 0:
                    createSword(gm.cell2Pos(2971));
                    break;
                case 1:
                    createBow(gm.cell2Pos(2971));
                    break;
                case 2:
                    createSpear(gm.cell2Pos(2971));
                    break;
                case 3:
                    createHorse(gm.cell2Pos(2971));
                    break;
            }
        }
        else if (funcCount % 5 == 3)
        {
            switch (randomSpot)
            {
                case 0:
                    createSword(gm.cell2Pos(11544));
                    break;
                case 1:
                    createBow(gm.cell2Pos(11544));
                    break;
                case 2:
                    createSpear(gm.cell2Pos(11544));
                    break;
                case 3:
                    createHorse(gm.cell2Pos(11544));
                    break;
            }
        }
        else if (funcCount % 5 == 4)
        {
            switch (randomSpot)
            {
                case 0:
                    createSword(gm.cell2Pos(11611));
                    break;
                case 1:
                    createBow(gm.cell2Pos(11611));
                    break;
                case 2:
                    createSpear(gm.cell2Pos(11611));
                    break;
                case 3:
                    createHorse(gm.cell2Pos(11611));
                    break;
            }
        }
        funcCount += 1;
    }

    int createCellNum()
    {
        int cell = 0;

        if (ps.areaNum == 0)
        {
            cell = Random.Range(0, ps.row / 2);
        }
        if (ps.areaNum == 1)
        {
            cell = Random.Range(ps.row / 2, ps.row);
        }
        if (ps.areaNum == 2)
        {
            cell = Random.Range(ps.row * (ps.col - 1), ((ps.row * ps.col) - (ps.row / 2)));
        }
        if (ps.areaNum == 3)
        {
            cell = Random.Range(((ps.row * ps.col) - (ps.row / 2)), ps.row * ps.col - 1);
        }

        return cell;
    }

    public void createSword(Vector3 v3)
    {
        GameObject child = Instantiate(SwordMan, gameObject.transform.position + v3, Quaternion.identity) as GameObject;
        child.transform.parent = SwordFolder.transform;
        child.transform.Rotate(0, 180, 0);
        currentUnitCount++;
    }

    void createHorse(Vector3 v3)
    {
        GameObject child = Instantiate(HorseMan, gameObject.transform.position + v3, Quaternion.identity) as GameObject;
        child.transform.parent = HorseFolder.transform;
        child.transform.Rotate(0, 180, 0);
        currentUnitCount++;
    }

    void createSpear(Vector3 v3)
    {
        GameObject child = Instantiate(SpearMan, gameObject.transform.position + v3, Quaternion.identity) as GameObject;
        child.transform.parent = SpearFolder.transform;
        child.transform.Rotate(0, 180, 0);
        currentUnitCount++;
    }

    void createBow(Vector3 v3)
    {
        GameObject child = Instantiate(BowMan, gameObject.transform.position + v3, Quaternion.identity) as GameObject;
        child.transform.parent = ArcherFolder.transform;
        child.transform.Rotate(0, 180, 0);
        currentUnitCount++;
    }

    void createCatapult(Vector3 v3)
    {
        GameObject child = Instantiate(Catapult, gameObject.transform.position + v3 + new Vector3(0.5f,0,0.5f), Quaternion.identity) as GameObject;
        child.transform.parent = CatapultFolder.transform;
        child.transform.Rotate(0, 180, 0);
        if (Application.loadedLevel == 3)
        {
            if (currentUnitCount < 1)
                child.tag = "Tree";
            else
            {
                child.tag = "OffenseDef";
                currentUnitCount++;
            }
        }
        else
            currentUnitCount++;
    }

    int looseLocate()
    {
        int locate = 0;

        //if(player)

        return locate;
    }


}
