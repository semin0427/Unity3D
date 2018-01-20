using UnityEngine;
using System.Collections;

public class ATKSwordMan : AtkUnit
{
    StageManager _parameter = null;
    // Use this for initialization
    void Start()
    {
        Init();
        SetStatus(100, 10, 5, 9, 1.5f, 0.8f, 1.0f, 1.4f);
        killPoint = 5;
        gold = 2;
        if (Application.loadedLevel == 2)
        {
            _parameter = GameObject.Find("QuestManager").GetComponent<StageManager>();

            if (_parameter.tutorialNum != 15)
                SetStatus(2, 1, 0, 5, 1.5f, 0.8f, 1.0f, 1.4f);
            else
                SetStatus(10, 2, 0, 5, 1.5f, 0.8f, 1.0f, 1.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ActManager();
    }

    public override void Idle()
    {
        base.Idle();
    }

    public override void Attack()
    {
        base.Attack();

        attackObject(1);
    }

    public override void Death()
    {
        base.Death();

        if (Application.loadedLevel == 2)
        {
            if (_parameter.tutorialNum == 4)
                _parameter.tutorialNum = 5;

            if (_parameter.tutorialNum == 5)
            {
                GameObject.Find("UnitCreater").GetComponent<EnemyCreater>().createSword(new Vector3(-2, 0, -13));
                _parameter.tutorialNum = 6;
            }
            else if (_parameter.tutorialNum == 8)
            {
                _parameter.tutorialNum = 9;
            }
        }
    }
}