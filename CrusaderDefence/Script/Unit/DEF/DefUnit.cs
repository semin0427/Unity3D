using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefUnit : CsUnit {

    public int cost;

    PlayManager pm;
	// Update is called once per frame
	void Update () {
        ActManager();
    }

    public override void Init()
    {
        base.Init();
        isAttacker = PlayManager.atkState;
        pm = GameObject.Find("Castle").GetComponent<PlayManager>();

        if (isAttacker)
            gameObject.tag = "DefenceAtk";
    }

    public override void ActManager()
    {
        base.ActManager();
        unitAnimator.SetBool("Position",isAttacker);
    }

    public override ActState FSM()
    {
        if (pm.bGameOver)
            return ActState._DEATH;
        else
        {
            if (hp > 0)
            {
                if (closest != null)
                {
                    if (detectRange >= Vector3.Distance(transform.position, closest.transform.position) && attackRange < Vector3.Distance(transform.position, closest.transform.position))
                    {
                        return ActState._Detect;
                    }
                    else if (attackRange >= Vector3.Distance(transform.position, closest.transform.position))
                    {
                        return ActState._ATTACK;
                    }
                    else
                        return ActState._IDLE;
                }
                else
                    return ActState._IDLE;
            }
            else if (hp <= 0)
            {
                return ActState._DEATH;
            }
        }
        return 0;
    }

    public override void Idle()
    {
        base.Idle();

        if(!isAttacker)
            transform.rotation = new Quaternion(0, 0, 0, 0);
        else if(isAttacker)
        {
            if (closest)
            {
                transform.LookAt(closest.transform.position);
                transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, speed * Time.deltaTime);
            }
        }
    }

    public override void Detect()
    {
        base.Detect();
        transform.LookAt(closest.transform.position);

        if(isAttacker)
        {
            transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, speed * Time.deltaTime * 0.8f);
        }
    }

    public override void Attack()
    {
        base.Attack();

        FireClosestMonster(gameObject, EnemyList);

        transform.LookAt(closest.transform.position);
    }

    public override void Death()
    {
        base.Death();

        if (dieDelay == 0)
            SoundManager.instance.SoundPlay2(SoundManager.instance.DieEFS);

        dieDelay += Time.deltaTime;
        lm.DefenceAtkUnit.Remove(gameObject);
        if (dieDelay >= deadSpd)
        {
            Destroy(gameObject);
            dieDelay = 0;
        }
    }
}
