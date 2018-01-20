using UnityEngine;
using System.Collections;

public class DEFSpearMan : DefUnit
{
    // Use this for initialization
    void Start()
    {
        Init();

        if (!isAttacker)
        {
            SetStatus(15, 8, 5, 5, 2.5f, 0, 1.1f, 1.6f);
        }
        else if (isAttacker)
            SetStatus(12, 10, 5, 5, 2.5f, 1.0f, 1.1f, 1.6f);
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
            SoundManager.instance.SoundPlay2(SoundManager.instance.SpearEFS);
    }
}
