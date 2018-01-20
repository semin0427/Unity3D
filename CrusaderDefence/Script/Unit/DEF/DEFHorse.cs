using UnityEngine;
using System.Collections;

public class DEFHorse : DefUnit {

	// Use this for initialization
	void Start () {
        Init();

        if (!isAttacker)
        {
            SetStatus(30, 15, 5, 5, 1.5f, 0, 1.1f, 2.15f);
        }
        else if (isAttacker)
            SetStatus(20, 15, 5, 5, 1.5f, 1.4f, 1.1f, 2.15f);
        
    }
	
	// Update is called once per frame
	void Update () {
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
