using UnityEngine;
using System.Collections;

public class ATKArcher : AtkUnit {

	// Use this for initialization
	void Start () {
        Init();
        SetStatus(7, 5, 1, 12.0f, 5.0f, 0.8f, 1.1f, 1.2f);
        killPoint = 8;
        gold = 3;
    }
	
	// Update is called once per frame
	void Update () {
            ActManager();
    }

    public override void Attack()
    {
        base.Attack();
        attackObject(1.1f);
    }
}
