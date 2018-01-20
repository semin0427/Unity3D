using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CsUnit : MonoBehaviour
{
    public int hp;
    public int atk;
    public int def;
    public float detectRange;
    public float attackRange; 
    public float speed;
    public float atkSpd;
    public float deadSpd;
    public int killPoint;
    public bool isAttacker; //true = atk, false = def

    public float atkDelay;
    public float dieDelay;
    public bool isDead;

    public int atkMotion;
    public int dieMotion;

    public GridManager gm;
    public GameObject closest;
    //public GameObject[] EnemyUnit;
    public List<GameObject> EnemyList;
    public ListManager lm;

    public Animator unitAnimator;

    public GameObject bloodParticle;
    ParticleSystem _particle;
    public Coroutine move_coroutine = null;

    public StageManager SM;
    public ActState CurrentState;

    public enum ActState
    {
        _IDLE,
        _Detect,
        _ATTACK,
        _DEATH,
    }

    void Awake()
    {
        
    }

    void Start()
    {
        Init();
        atkDelay = 0;
        dieDelay = 0;
        atkMotion = 0;
        isDead = false;
    }

    void Update()
    {
    }

    public virtual void SetStatus(int _hp, int _atk, int _def, float _detactRange, float _attackRange, float _spd, float _atkSpd, float _deadSpd)
    {
        hp = _hp;
        atk = _atk;
        def = _def;
        detectRange = _detactRange;
        attackRange = _attackRange;
        speed = _spd;
        atkSpd = _atkSpd;
        deadSpd = _deadSpd;
    }

    public void setSpd(float _spd)
    {
        speed = _spd;
    }

    public virtual void Init()
    {
        if (Application.loadedLevel == 0 || Application.loadedLevel == 1)
            return;
        lm = GameObject.Find("ListMgr").GetComponent<ListManager>();
        unitAnimator = gameObject.GetComponent<Animator>();
        gm = Camera.main.GetComponent<GridManager>() as GridManager;
        SM = GameObject.Find("QuestManager").GetComponent<StageManager>();
        if (bloodParticle)
            _particle = bloodParticle.GetComponent<ParticleSystem>();
    }

    public virtual void ActManager()
    {
 //       ActState curentState;
        CurrentState = FSM();
        
        if (CurrentState == ActState._IDLE)
            Idle();
        else if (CurrentState == ActState._Detect)
            Detect();
        else if (CurrentState == ActState._ATTACK)
            Attack();
        else if (CurrentState == ActState._DEATH)
            Death();
    }

    public virtual ActState FSM()
    {
        return 0;
    }

    public virtual void Idle()
    {
        unitAnimator.SetBool("isDetect", false);
        unitAnimator.SetBool("isAttack", false);

        if (bloodParticle)
            _particle.Stop();
    }

    public virtual void Detect()
    {
        unitAnimator.SetBool("isDetect", true);
        unitAnimator.SetBool("isAttack", false);

        if (bloodParticle)
            _particle.Stop();
    }

    public virtual void Attack()
    {
        unitAnimator.SetBool("isDetect", true);
        unitAnimator.SetBool("isAttack", true);
    }

    public virtual void Death()
    {
        unitAnimator.SetBool("isDetect", false);
        unitAnimator.SetBool("isAttack", false);
        unitAnimator.SetBool("isDead", true);
        if (bloodParticle)
            _particle.Stop();
    }

    public virtual GameObject FindClosestUnit(GameObject _Object, List<GameObject> _Units)
    {
        GameObject closest = null;

        float closestDist = float.MaxValue;
        List<GameObject> units = _Units;

        foreach (var unit in units)
        {
            float dist = (unit.transform.position - _Object.transform.position).sqrMagnitude;
            if (dist < closestDist)
            {
                closest = unit;
                closestDist = dist;
            }
        }

        // 반복문을 통과했다 해도 과연 값이 제대로 설정될 것인지는 확인! 또 확인!
        if (closest != null)
            Debug.DrawLine(_Object.transform.position, closest.transform.position, Color.red);

        return closest;
    }

    public virtual void FireClosestMonster(GameObject _Object, List<GameObject> _Units)
    {
        if (!_Object) return; // 하지만 player가 널일리는 없음. 왜? PlayerScript에서 본 함수를 호출하기 때문 

        if (_Units.Count == 0) return;

        closest = FindClosestUnit(_Object, _Units);

        if (!closest)
        {
            print("더 이상 남아 있는 몬스터가 없음. 그런데 실제 이 코드로 올리는 없음.");
            return;
        }

        int dmg = atk - def;

        if (dmg <= 0)
            dmg = 1;

        if (atkDelay == 0)
            closest.GetComponent<CsUnit>().hp -= dmg;

        if (bloodParticle)
        {
            bloodParticle.transform.position = closest.transform.position;

            if (_particle.isPlaying == false)
                _particle.Play();
        }

        atkDelay += Time.deltaTime;

        if (atkDelay >= atkSpd)
            atkDelay = 0;
    }
}