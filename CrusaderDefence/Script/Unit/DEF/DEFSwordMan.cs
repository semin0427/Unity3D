using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DEFSwordMan : DefUnit
{
    // Use this for initialization
    void Start()
    {
        Init();

        if (!isAttacker)
        {
            SetStatus(10, 6, 5, 5, 1.5f, 0, 1.0f, 1.2f);
        }
        else if (isAttacker)
            SetStatus(10, 10, 5, 5, 1.5f, 1, 1.0f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacker)
            EnemyList = lm.OffenseAtkUnit;
        else
            EnemyList = lm.OffenceDefUnit;

        if (EnemyList != null)
            closest = FindClosestUnit(gameObject, EnemyList);

        ActManager();

        if (atkDelay == 0 && CurrentState == ActState._ATTACK)
            SoundManager.instance.SoundPlay2(SoundManager.instance.SwordEFS);
    }
}