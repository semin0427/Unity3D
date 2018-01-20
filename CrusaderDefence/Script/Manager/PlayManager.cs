using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlayManager : MonoBehaviour
{
    GridManager gm = null;

    GameObject unit;
    GameObject unitFolder;

    float getGoldTime;
    public int Gold;
    public int Wood;

    public static int Score;

    public int Cost;

    public List<GameObject> Units;

    public GameObject SwordFolder;
    public GameObject SwordMan;

    public GameObject ArcherFolder;
    public GameObject Archer;

    public GameObject SpearFolder;
    public GameObject SpearMan;

    public GameObject HorseFolder;
    public GameObject HorseMan;

    public GameObject WorkerFolder;
    public GameObject Worker;

    public GameObject CatapultFolder;
    public GameObject Catapult;

    private GameObject preset;

    public GameObject PresetSword;
    public GameObject PresetArcher;
    public GameObject PresetSpear;
    public GameObject PresetWorker;
    public GameObject PresetHorse;
    public GameObject PresetCatapult;

    public GameObject lastObject;

    ListManager lm;
    StageManager _parameter = null;

    public bool bGameOver;
    public bool bGameClear;

    public int stageNum;

    static public bool atkState;

    public enum SelectUnit
    {
        None,
        Sword,
        Bow,
        Spear,
        Work,
        Horse,
        Catapult,
    }

    // Use this for initialization
    void Start()
    {
        atkState = false;
        gm = Camera.main.GetComponent<GridManager>() as GridManager;
        lm = GameObject.Find("ListMgr").GetComponent<ListManager>();
        Units = lm.DefenceDefUnit;
        bGameOver = false;
        bGameClear = false;
        _parameter = GameObject.Find("QuestManager").GetComponent<StageManager>();
        Score = 0;
        lastObject = null;
        createUnit(null, null, null);


        switch (Application.loadedLevel)
        {
            case 2:
                Gold = 500;
                Wood = 500;
                break;
            case 3:
                Gold = 1000;
                Wood = 500;
                break;
            case 4:
                Gold = 2000;
                Wood = 500;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(preset !=null)
            PresetSetting();

        if (Input.GetKeyDown(KeyCode.Z))
            Gold += 500;
        if (Input.GetKeyDown(KeyCode.X))
            Wood += 500;
        if (Input.GetKeyDown(KeyCode.C))
            PlayerScript.HP += 500;

        getGold();
        pickCastle();
        bGameOver = gameOver();
        bGameClear = gameClear();
    }

    public string pickCastle()
    {
        if (Input.GetMouseButtonDown(0) && onUI() == false)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == GameObject.Find("Castle").transform)
                {
                    if (Application.loadedLevel == 2)
                    {
                        if (_parameter.tutorialNum == 2)
                        {
                            _parameter.tutorialNum = 3;
                        }
                    }
                    if (Application.loadedLevel == 4)
                    {
                        if (_parameter.tutorialNum == 3)
                        {
                            _parameter.tutorialNum = 4;
                        }
                    }
                    UIManager.isCastlePick = true;
                    return "Castle";
                }
                else
                {
                    if (Application.loadedLevel == 3)
                    {
                        if (pickTile() < 2700 && atkState == false)
                        {
                            placedUnit();
                        }
                        else if (pickTile() < 4800 && pickTile() >= 2700 && atkState)
                        {
                            placedUnit();
                        }
                    }
                    else if(Application.loadedLevel == 4 || Application.loadedLevel == 9)
                    {
                        Debug.Log(pickTile());
                        if ((pickTile() / 120 > 28 && pickTile() / 120 < 91) && (pickTile() % 120 > 28 && pickTile() % 120 < 91))
                        {
                                if (atkState == false)
                                    placedUnit();
                        }
                        else
                        {
                            if ((pickTile() / 120 > 19 && pickTile() / 120 < 100) && (pickTile() % 120 > 19 && pickTile() % 120 < 100))
                                if (atkState)
                                    placedUnit();
                        }
                    }
                    else
                    {
                        Debug.Log(pickTile());
                        placedUnit();
                    }
                    return "Tile";
                }
            }
        }
        return "NONE";
    }

    int pickTile()
    {
        int tileNum = 0;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            tileNum = gm.pos2Cell(hit.point);

        return tileNum;
    }

    void placedUnit()
    {
        if (gm.world[pickTile()] == GridManager.TileType.Plain)
        {
            if (Catapult != null && unit == Catapult)
            {
                if(Wood - Cost >= 0)
                {
                    GameObject child = Instantiate(unit, gm.locateToCenter(gm.cell2Pos(pickTile())), Quaternion.identity) as GameObject;
                    child.transform.parent = unitFolder.transform;
                    lm.DefenceDefUnit.Add(child);
                    Wood -= Cost;
                    lastObject = child;
                    if (atkState)
                        gm.world[pickTile()] = GridManager.TileType.Plain;
                    else
                        gm.world[pickTile()] = GridManager.TileType.Wall;
                }
            }
            else
            {
                if (Gold - Cost >= 0)
                {
                    GameObject child = Instantiate(unit, gm.locateToCenter(gm.cell2Pos(pickTile())), Quaternion.identity) as GameObject;
                    child.transform.parent = unitFolder.transform;
                    lm.DefenceDefUnit.Add(child);
                    Gold -= Cost;
                    lastObject = child;
                    if (unit == Worker)
                        gm.world[pickTile()] = GridManager.TileType.Plain;
                    else if (unit != Worker)
                    {
                        if (atkState)
                            gm.world[pickTile()] = GridManager.TileType.Plain;
                        else
                            gm.world[pickTile()] = GridManager.TileType.Wall;
                    }
                }
            }
        }

        if (gm.world[pickTile()] == GridManager.TileType.Wall)
        {
        }

        if (gm.world[pickTile()] == GridManager.TileType.Unit)
        {
        }
    }

    void UnitChoice(SelectUnit unit)
    {
        switch (unit)
        {
            case SelectUnit.Sword:
                createUnit(SwordMan, SwordFolder, PresetSword);
                break;

            case SelectUnit.Bow:
                createUnit(Archer, ArcherFolder, PresetArcher);
                break;

            case SelectUnit.Spear:
                createUnit(SpearMan, SpearFolder, PresetSpear);
                break;

            case SelectUnit.Work:
                createUnit(Worker, WorkerFolder, PresetWorker);
                break;

            case SelectUnit.Horse:
                createUnit(HorseMan, HorseFolder, PresetHorse);
                break;
            case SelectUnit.Catapult:
                createUnit(Catapult, CatapultFolder, PresetCatapult);
                break;

            default:
                break;
        }
    }

    public void setPosition(bool _position)
    {
        atkState = _position;
      //  Debug.Log(atkState);
    }

    void getGold()
    {
        getGoldTime += Time.deltaTime;
        if (getGoldTime >= 1)
        {
            Gold += 1;
            getGoldTime = 0;
        }
    }

    void createUnit(GameObject _unitClass, GameObject _unitFolder, GameObject _presetClass)
    {
        if(preset != null)
            preset.transform.position = new Vector3(0, -10, 0);

        unit = _unitClass;
        unitFolder = _unitFolder;
        preset = _presetClass;
    }

    void PresetSetting()
    {
        preset.transform.position = gm.locateToCenter(gm.cell2Pos(pickTile()));
    }

    bool gameOver()
    {
        if (PlayerScript.HP <= 0)
        {
            return true;
        }
        return false;
    }

    bool gameClear()
    {
        if(Application.loadedLevel == 2)
        {
            if(Score >= 145)
             return true;
        }
        if (Application.loadedLevel == 3 && _parameter.tutorialNum > 10)
        {
            if (EnemyCreater.EnemyHP <= 0)
            {
                return true;
            }
        }
        if (Application.loadedLevel == 4)
        {
            if (EnemyCreater.EnemyHP <= 0)
            {
                return true;
            }
        }
        return false;
    }

    bool onUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
