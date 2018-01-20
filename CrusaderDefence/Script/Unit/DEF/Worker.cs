using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Worker : DefUnit
{
    GameObject catapult;
    public GameObject Castle;
    Coroutine move_coroutine = null;

    public bool getWood;
    StageManager _parameter = null;

    int getWoodCount;
    // Use this for initialization

    void Start()
    {
        Init();
        Castle = GameObject.Find("Castle");
        SetStatus(10, 1, 0, 0, 1, 1, 0, 0);
        getWood = false;

        if (Application.loadedLevel == 2 || Application.loadedLevel == 3)
        {
            _parameter = GameObject.Find("QuestManager").GetComponent<StageManager>();
        }

        if (Application.loadedLevel == 3)
            catapult = GameObject.Find("ATK_Catapult(Clone)");

        if (Application.loadedLevel == 9)
        {
            getWoodCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyList = lm.TreeList;

        if (lm.TreeList.Count == 0)
        {
                lm.DefenceDefUnit.Remove(gameObject);
                Destroy(gameObject);
        }
        else
        {
            closest = FindClosestUnit(gameObject, EnemyList);
            ActManager();
        }

        if(getWoodCount == 5)
        {
            lm.DefenceDefUnit.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    
    public override ActState FSM()
    {
        if (!getWood)
        {
            if (attackRange >= Vector3.Distance(gameObject.transform.position, closest.transform.position))
                return ActState._ATTACK;

            return ActState._IDLE;
        }
        else if (getWood)
            return ActState._Detect;

        return 0;
    }

    public override void Idle()
    {
        if (move_coroutine != null)
            StopCoroutine(move_coroutine);
        move_coroutine = StartCoroutine(gm.MoveForWorker(gameObject, closest.transform.position, speed * 0.7f));
    }

    public override void Attack()
    {
        unitAnimator.SetBool("goToTree", false);
        unitAnimator.SetBool("arrivedAtTree", true);
        unitAnimator.SetBool("finishCutting", false);

        if (move_coroutine != null)
            StopCoroutine(move_coroutine);
        CutTree(gameObject, EnemyList);
    }

    public override void Detect()
    {

        if (move_coroutine != null)
            StopCoroutine(move_coroutine);

        move_coroutine = StartCoroutine(gm.Move(gameObject, Castle.transform.position, speed * 0.8f));

        if (attackRange + 0.5f >= Vector3.Distance(transform.position, Castle.transform.position))
        {
            Castle.GetComponent<PlayManager>().Wood += 5;
            if (Application.loadedLevel == 2)
            {
                if (_parameter.tutorialNum == 11)
                    _parameter.tutorialNum = 12;
            }
            getWood = false;
            if(Application.loadedLevel == 9)
                getWoodCount += 1;

            unitAnimator.SetBool("goToTree", true);
            unitAnimator.SetBool("arrivedAtTree", false);
            unitAnimator.SetBool("finishCutting", false);
        }
    }

    void CutTree(GameObject _Object, List<GameObject> _Units)
    {
        if (!_Object) return; // 하지만 player가 널일리는 없음. 왜? PlayerScript에서 본 함수를 호출하기 때문 

        if (_Units.Count == 0) return;

        if (!closest)
        {
            print("더 이상 남아 있는 몬스터가 없음. 그런데 실제 이 코드로 올리는 없음.");
            return;
        }

        atkSpd += Time.deltaTime;
        GameObject tree = closest;
        _Units.Remove(closest);
        closest = null;

        if (atkSpd >= 1.0f)
        {
            float dmg = 0.5f;

            if (catapult && tree == catapult)
                tree.GetComponent<ATKCatapult>().getCatapult -= dmg;
            else
                tree.GetComponent<CsTree>().atkCount -= dmg;

            atkSpd = 0;
        }

        if (Application.loadedLevel == 3 && tree == catapult)
        {
            if (tree.GetComponent<ATKCatapult>().getCatapult <= 0 && _parameter.tutorialNum == 6)
            {
                gm.world[gm.pos2Cell(tree.transform.position)] = GridManager.TileType.Plain;
                Destroy(tree);
                tree = null;

                _parameter.tutorialNum = 7;

                unitAnimator.SetBool("goToTree", true);
                unitAnimator.SetBool("arrivedAtTree", false);
                unitAnimator.SetBool("finishCutting", false);
            }
        }
        else if (tree.GetComponent<CsTree>().atkCount <= 0)
        {
            getWood = true;

            gm.world[gm.pos2Cell(tree.transform.position)] = GridManager.TileType.Plain;
            Destroy(tree);
            tree = null;

            unitAnimator.SetBool("goToTree", false);
            unitAnimator.SetBool("arrivedAtTree", false);
            unitAnimator.SetBool("finishCutting", true);
        }
    }
}
