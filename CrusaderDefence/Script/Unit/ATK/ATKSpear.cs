using UnityEngine;
using System.Collections;

public class ATKSpear : AtkUnit
{
    // Use this for initialization
    void Start()
    {
        Init();
        SetStatus(12, 10, 5, 10, 2.5f, 1.0f, 1.1f, 1.6f);
        killPoint = 8;
        gold = 5;
    }

    // Update is called once per frame
    void Update()
    {
        ActManager();
    }

    public override void Attack()
    {
        base.Attack();

        attackObject(1.1f);
    }
}
