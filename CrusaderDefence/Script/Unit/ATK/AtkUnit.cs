using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtkUnit : CsUnit
{
    public GameObject Castle;
    GameObject atkTarget;
    public int gold;

    void Update()
    {
        ActManager();
    }

    public override void Init()
    {
        base.Init();

        Castle = GameObject.Find("Castle");

        if ((Application.loadedLevel == 3 && SM.tutorialNum == 1) || (Application.loadedLevel == 4 && SM.tutorialNum == 0))
            isAttacker = false;
        else
            isAttacker = true;

        if(!isAttacker)
            gameObject.tag = "OffenseDef";

        unitAnimator.SetBool("Position", isAttacker);
    }

    public override void ActManager()
    {
        base.ActManager();

        if (!isAttacker)
            EnemyList = lm.DefenceAtkUnit;
    }

    public override ActState FSM()
    {
        if (hp > 0)
        {
            if (isAttacker.Equals(true))
                closest = Castle;
            else
            {
                if (EnemyList.Count == 0)
                {
                    closest = Castle;
                    return ActState._IDLE;
                }
                else
                    closest = FindClosestUnit(gameObject, EnemyList);
            }

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
            //else
            //{
            //    if (detectRange >= Vector3.Distance(transform.position, Castle.transform.position) && attackRange < Vector3.Distance(transform.position, Castle.transform.position))
            //    {
            //        return ActState._Detect;
            //    }
            //    else if (attackRange >= Vector3.Distance(transform.position, Castle.transform.position))
            //    {
            //        return ActState._ATTACK;
            //    }
            //    else
            //        return ActState._IDLE;
            //}
        }
        else if (hp <= 0)
        {
            return ActState._DEATH;
        }

        return 0;
    }

    public override void Idle()
    {
        base.Idle();

        if (move_coroutine != null)
            StopCoroutine(move_coroutine);

        if(isAttacker)
            move_coroutine = StartCoroutine(gm.Move(gameObject, closest.transform.position, speed * 0.8f));
    }

    public override void Detect()
    {
        base.Detect();

        if (move_coroutine != null)
            StopCoroutine(move_coroutine);

        if(isAttacker)
            move_coroutine = StartCoroutine(gm.Move(gameObject, closest.transform.position, speed * 0.5f));
        else if(!isAttacker)
            move_coroutine = StartCoroutine(gm.Move(gameObject, closest.transform.position, speed * 0.8f));
    }

    public override void Attack()
    {
        base.Attack();

        if (move_coroutine != null)
            StopCoroutine(move_coroutine);

        if(!isAttacker)
        {
            FireClosestMonster(gameObject, EnemyList);
            transform.LookAt(closest.transform.position);
        }
    }

    public override void Death()
    {
        base.Death();

        if (dieDelay == 0)
            SoundManager.instance.SoundPlay2(SoundManager.instance.DieEFS);

        dieDelay += Time.deltaTime;
        lm.OffenseAtkUnit.Remove(gameObject);

        if (dieDelay >= deadSpd)
        {
            Destroy(gameObject);
            dieDelay = 0;
            PlayManager.Score += killPoint;
            Castle.GetComponent<PlayManager>().Gold += gold;
            //EnemyCreater.EnemyHP -= 1;
        }

        if (move_coroutine != null)
            StopCoroutine(move_coroutine);
    }

    public virtual void attackCastle(float _delayTime)
    {
        atkSpd += Time.deltaTime;
        if (atkSpd >= _delayTime)
        {
            PlayerScript.HP -= atk;
            atkSpd = 0;
        }
    }

    public void attackObject(float _delayTime)
    {
        atkSpd += Time.deltaTime;

        if (atkSpd >= _delayTime)
        {
            if (closest == Castle)
            {
                PlayerScript.HP -= atk;
            }
            else
                closest.GetComponent<CsUnit>().hp -= atk;
                            
            atkSpd = 0;
        }
    }
}
