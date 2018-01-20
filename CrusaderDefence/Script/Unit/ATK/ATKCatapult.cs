using UnityEngine;
using System.Collections;

public class ATKCatapult : AtkUnit
{

    public float getCatapult;
    // Use this for initialization
    void Start()
    {
        Init();
        if (Application.loadedLevel == 3)
        {
            if (SM.tutorialNum == 0)
            {
                gameObject.tag = "Tree";
                //lm.TreeList.Add(this.gameObject);
                SetStatus(1, 0, 0, 0, 0, 0, 0, 1.0f);
                unitAnimator.Play("WK_catapult_04_death");
                getCatapult = 1;
            }
            else
            {
                SetStatus(10, 5, 3, 20, 10, 1, 10, 1.0f);
                killPoint = 30;
                gold = 10;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ActManager();
    }

    public override void Attack()
    {
        base.Attack();

        attackObject(10);
    }
}
