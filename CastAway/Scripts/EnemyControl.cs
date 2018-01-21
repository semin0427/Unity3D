using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

    private GameObject Willson;
    private GameObject User;
    private PlayerControl playerControl;

    public float RunAwayVelocity;
    public float ComingVelocity;
    public Vector3 safePosition;
    private GameStatus gs;

    float downTime = 2; //levelDesign
    float stayTime = 0;

    bool Back = false;
    int RandomPos;

    enum State
    {
        NONE = -1,
        IDLE = 0,
        RUN,
        ATK,
        NUM,
    }

    State _state;
    Animator ani;
	// Use this for initialization
	void Start () {
        Willson = GameObject.Find("WillsonTable");
        User = GameObject.Find("Player");
        playerControl = User.GetComponentInChildren<PlayerControl>();
        gs = GameObject.Find("GameRoot").GetComponent<GameStatus>();
        _state = State.IDLE;
        ani = GetComponent<Animator>();
        RandomPos = Random.Range(0,4);

        for (int i = 0; i < 4; i++)
        {
            if (RandomPos >= i && RandomPos < i+1)
            {
                RandomPos = i;
            }
        }

        switch(RandomPos)
        {
            case 0:
                safePosition = new Vector3(-15, 0, 0);
                break;
            case 1:
                safePosition = new Vector3(15, 0, 0);
                break;
            case 2:
                safePosition = new Vector3(0, 0, -15);
                break;
            case 3:
                safePosition = new Vector3(0, 0, 15);
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
        ChangeState();
        Act();
    }

    void Act()
    {
        switch(_state)
        {
            case State.IDLE:
                Debug.Log("Idel");
                Move2Willson();
                break;
            case State.RUN:
                Debug.Log("Run");
                AwayFromPlayer();
                break;
            case State.ATK:
                Debug.Log("attack");
                AtackWillson();
                break;
        }
    }

    void ChangeState()
    {
        if (/*gs.getFire && */Vector3.Distance(User.transform.position, transform.position) < 1.0f)
        {
            Back = true;
            _state = State.RUN;
        }
        else if(Vector3.Distance(Willson.transform.position, transform.position) < 1.0f)
        {
            _state = State.ATK;
        }
        else if(Back == false)
            _state = State.IDLE;
    }

    void Move2Willson()
    {
        ani.SetBool("isAttack", false);
        Back = false;

        if (Back == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, Willson.transform.position, ComingVelocity * Time.deltaTime);
            transform.LookAt(Willson.transform);
        }
    }

    void AwayFromPlayer()
    {
        ani.SetBool("isAttack", false);
        if (Back == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, safePosition, RunAwayVelocity * Time.deltaTime);
            transform.LookAt(safePosition);
            if (Vector3.Distance(transform.position, safePosition) <= 0.1f)
            {
                _state = State.IDLE;
            }
        }
    }

    void AtackWillson()
    {
        stayTime += Time.deltaTime;
        ani.SetBool("isAttack", true);

        if (SoundManager.instance.myAudio.isPlaying)
        {
            SoundManager.instance.myAudio.Stop();
        }
        else
            SoundManager.instance.atkEFS();

        if (stayTime>downTime)
        {
            Willson.GetComponent<CsWillson>().HP -= 1;
            stayTime = 0;
        }
    }
}
