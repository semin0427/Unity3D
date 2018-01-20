using UnityEngine;
using System.Collections;

public class ATKHorseMan : AtkUnit
{
    // Use this for initialization
    void Start() {
        Init();
        SetStatus(20, 10, 5, 6, 1.5f, 1.4f, 1.1f, 2.15f);
        killPoint = 10;
        gold = 10;
    }

    // Update is called once per frame
    void Update() {
        ActManager();
    }

    public override void Attack()
    {
        base.Attack();

        if (isAttacker)
            attackCastle(1.1f);
    }
}
