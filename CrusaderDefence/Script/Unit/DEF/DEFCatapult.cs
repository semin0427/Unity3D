using UnityEngine;
using System.Collections;

public class DEFCatapult : DefUnit {

	// Use this for initialization
	void Start () {
        Init();

        if (!isAttacker)
            SetStatus(10, 40, 5, 20, 10, 0, 10.0f, 1.2f);
        else if (isAttacker)
            SetStatus(10, 40, 5, 20, 10, 0.1f, 10.0f, 1.2f);
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
            SoundManager.instance.SoundPlay2(SoundManager.instance.CatapultEFS);
    }
}
