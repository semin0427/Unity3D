using UnityEngine;
using System.Collections;

public class DEFArcher : DefUnit
{
    // Use this for initialization
    void Start()
    {
        Init();

        if (!isAttacker)
        {
            SetStatus(7, 5, 1, 6.0f, 5.0f, 0, 1.1f, 1.2f);
        }
        else if (isAttacker)
            SetStatus(7, 5, 1, 6.0f, 5.0f, 0.8f, 1.1f, 1.2f);
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
            SoundManager.instance.SoundPlay2(SoundManager.instance.BowEFS);
    }
}
