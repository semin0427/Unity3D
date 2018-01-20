using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    GridManager gm = null;

    public int row;
    public int col;

    public static int HP;

    public int areaNum;
    StageManager _parameter = null;

    // Use this for initialization
    void Awake ()
    {
        gm = Camera.main.GetComponent<GridManager>() as GridManager;
        _parameter = GameObject.Find("QuestManager").GetComponent<StageManager>();
        switch(Application.loadedLevel)
        {
            case 2:
                worldSetting(30, 30, 50, 15, 15);
                break;
            case 3:
                worldSetting(60, 90, 40, 25.5f, 9.5f);
                break;
            case 4:
                worldSetting(120, 120, 30, 60.5f, 60.5f);
                break;
            case 9:
                worldSetting(120, 120, 100, 60.5f, 60.5f);
                break;
        }
    }

    void worldSetting(int _row, int _col, int _hp, float castleX, float castleY)
    {
        row = _row;
        col = _col;
        HP = _hp;
        gm.BuildWorld(row, col, castleX, castleY);
    }

    // Update is called once per frame
    void Update ()
    {
        switch(Application.loadedLevel)
        {
            case 2:
                if (HP > 50)
                    HP = 50;
                break;
            case 3:
                if (HP > 40)
                    HP = 40;
                break;
            case 4:
                if (HP > 30)
                    HP = 30;
                break;
            case 9:
                if (HP > 100)
                    HP = 100;
                break;
        }

        if (HP < 0)
            HP = 0;

        if (Application.loadedLevel == 2)
        {
            if (HP != 50 && _parameter.tutorialNum == 6)
            {
                _parameter.tutorialNum = 7;
            }
        }
    }   
}
