using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static float MOVE_AREA_RADIUS = 15.0f; // 섬의 반지름.
    public float MOVE_SPEED; // 이동 속도.
    public float MOVE_ROTATE; // 회전 속도

    private GameObject MapCamera;
    private GameObject goDir;
    private GameObject backDir;

    private GameObject willson;
    private struct Key
    { // 키 조작 정보 구조체.
        public bool up; // ↑.
        public bool down; // ↓.
        public bool right; // →.
        public bool left; // ←.
        public bool pick; // 줍는다／버린다.
        public bool action; // 먹는다 / 수리한다.
    };
    private Key key; // 키 조작 정보를 보관하는 변수.
    public enum STEP
    { // 플레이어의 상태를 나타내는 열거체.
        NONE = -1, // 상태 정보 없음.
        MOVE = 0, // 이동 중.
        REPAIRING, // 수리 중.
        EATING, // 식사 중.
        MAKINGCAMP, //
        GETTINGFIRE,
        NUM, // 상태가 몇 종류 있는지 나타낸다(=3).
    };

    public STEP step = STEP.NONE; // 현재 상태.
    public STEP next_step = STEP.NONE; // 다음 상태.
    public float step_timer = 0.0f;

    // 다음 네 개의 멤버 변수를 PlayerControl class에 추가.
    private GameObject closest_item = null; // 플레이어의 정면에 있는 GameObject.
    private GameObject carried_item = null; // 플레이어가 들어올린 GameObject.
    private ItemRoot item_root = null; // ItemRoot 스크립트를 가짐.
    public GUIStyle guistyle; // 폰트 스타일.

    private GameObject closest_event = null;// 주목하고 있는 이벤트를 저장.
    private EventRoot event_root = null; // EventRoot 클래스를 사용하기 위한 변수.
    private GameObject rocket_model = null; // 우주선의 모델을 사용하기 위한 변수.

    private GameObject game_root;

    private GameStatus game_status = null;
    private DayTime dt = null;
    private Animator animator = null;
    private GameObject _Torch;

    // Use this for initialization
    void Start()
    {
        goDir = GameObject.Find("FrontDir");
        backDir = GameObject.Find("BackDir");
        MapCamera = GameObject.Find("MiniMap");

        animator = GetComponentInChildren<Animator>();
        willson = GameObject.Find("WillsonTable");
        game_root = GameObject.Find("GameRoot");
        this.step = STEP.NONE; // 현 단계 상태를 초기화.
        this.next_step = STEP.MOVE;
        this.item_root = game_root.GetComponent<ItemRoot>();
        this.guistyle.fontSize = 16;
        
        this.event_root = game_root.GetComponent<EventRoot>();
        this.rocket_model = GameObject.Find("boat").transform.FindChild("boat_model").gameObject;
        this.game_status = game_root.GetComponent<GameStatus>();
        dt = game_root.GetComponent<DayTime>();

        _Torch = GameObject.Find("Torch");
        _Torch.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(willson.transform.position, transform.position) > 3)
            game_status.addLonely(GameStatus.CONSUME_LONELY_FAR_FROM_WILLSON);
        else
            game_status.addLonely(-GameStatus.CONSUME_LONELY_FAR_FROM_WILLSON);

        if (game_status.getFire)
            _Torch.SetActive(true);
        else
            _Torch.SetActive(false);

        if (carried_item == null)
        {
            animator.SetBool("isCarried", false);
            item_root.itemInfo.sprite = item_root.emptySprite;
        }
        else if(carried_item != null)
            animator.SetBool("isCarried", true);

        this.get_input(); // 입력 정보 취득.
                          // 상태가 변화했을 때------------.

        this.step_timer += Time.deltaTime;
        float eat_time = 0.5f; // 사과는 2초에 걸쳐 먹는다.
        float repair_time = 2.0f; // 수리에 걸리는 시간도 2초.

        // 상태를 변화시킨다---------------------.
        if (this.next_step == STEP.NONE)
        { // 다음 예정이 없으면.
            switch (this.step)
            {
                case STEP.MOVE: // '이동 중' 상태의 처리.
                    do
                    {
                        if (!this.key.action)
                        { // 액션 키가 눌려있지 않다.
                            break; // 루프 탈출.
                        }
                        // 주목하는 이벤트가 있을 때.
                        if (this.closest_event != null)
                        {
                            if (!this.is_event_ignitable())
                            { // 이벤트를 시작할 수 없으면.
                                break; // 아무 것도 하지 않는다.
                            }
                            // 이벤트 종류를 가져온다.
                            Event.TYPE ignitable_event =
                            this.event_root.getEventType(this.closest_event);
                            switch (ignitable_event)
                            {
                                case Event.TYPE.ROCKET: // 이벤트의 종류가 ROCKET이면.
                                                        // REPAIRING(수리) 상태로 이행.
                                    this.next_step = STEP.REPAIRING;
                                    break;
                                case Event.TYPE.MAKEFIRE:
                                    this.next_step = STEP.MAKINGCAMP;
                                    break;
                                case Event.TYPE.GETFIRE:
                                    this.next_step = STEP.GETTINGFIRE;
                                    break;
                            }
                            break;
                        }
                        if (this.carried_item != null)
                        {
                            // 가지고 있는 아이템 판별.
                            Item.TYPE carried_item_type = this.item_root.getItemType(this.carried_item);
                            switch (carried_item_type)
                            {
                                case Item.TYPE.APPLE: // 사과라면.
                                    this.next_step = STEP.EATING;
                                    break;
                                case Item.TYPE.PLANT: // 식물이라면.
                                    this.next_step = STEP.EATING; // '식사 중' 상태로 이행.
                                    break;
                            }
                        }
                    } while (false);
                    break;
                case STEP.EATING: // '식사 중' 상태의 처리.
                    if (this.step_timer > eat_time)
                    { // 2초 대기.
                        if (SoundManager.instance.myAudio.isPlaying)
                        {
                            SoundManager.instance.myAudio.Stop();
                        }
                        else
                            SoundManager.instance.eatEFS();
                        this.next_step = STEP.MOVE; // '이동' 상태로 이행.
                    }
                    break;
                case STEP.REPAIRING: // '수리 중' 상태의 처리.
                    if (this.step_timer > repair_time)
                    { // 2초 대기.
                        
                        this.next_step = STEP.MOVE; // '이동' 상태로 이행.
                    }
                    break;
                case STEP.MAKINGCAMP:
                    this.next_step = STEP.MOVE;
                    break;
                case STEP.GETTINGFIRE:
                    this.next_step = STEP.MOVE;
                    break;
                default:
                    break;
            }
        }
        while (this.next_step != STEP.NONE)
        { // 상태가 NONE이외 = 상태가 변화했다.
            this.step = this.next_step;
            this.next_step = STEP.NONE;
            switch (this.step)
            {
                case STEP.MOVE:
                    break;
                case STEP.EATING: // '식사 중' 상태의 처리.
                    if (this.carried_item != null)
                    {
                        // 가지고 있던 아이템을 폐기.
                        GameObject.Destroy(this.carried_item);
                        this.game_status.addSatiety(this.item_root.getRegainSatiety(this.carried_item));
                        this.carried_item = null;
                    }
                    break;
                case STEP.REPAIRING: // '수리 중'이 되면.
                    if (this.carried_item != null)
                    {
                        // 들고 있는 아이템의 '수리 진척 상태'를 가져와서 설정.
                        this.game_status.addRepairment(this.item_root.getGainRepairment(this.carried_item));

                        // 가지고 있는 아이템 삭제.
                        GameObject.Destroy(this.carried_item);
                        this.carried_item = null;
                        this.closest_item = null;
                    }
                    break;
                case STEP.MAKINGCAMP:
                    if (this.carried_item != null)
                    {
                        GameObject.Destroy(this.carried_item);
                        game_status.fireCount = 3;
                        this.carried_item = null;
                        this.closest_item = null;
                    }
                    break;
                case STEP.GETTINGFIRE:
                    if (this.carried_item == null)
                    {
                        game_status.fireCount -= 1;
                        game_status.getFire = true;
                    }
                    break;
            }
            this.step_timer = 0.0f;
        }
        // 각 상황에서 반복할 것----------.
        switch (this.step)
        {
            case STEP.MOVE:
                this.move_control();
                this.pick_or_drop_control();
                // 이동 가능한 경우는 항상 배가 고파진다.
                this.game_status.alwaysSatiety();
                break;
            case STEP.REPAIRING:
                // 우주선을 회전시킨다.
                animator.SetBool("fix", true);
                if (SoundManager.instance.myAudio.isPlaying)
                {
                    SoundManager.instance.myAudio.Stop();
                }
                else
                    SoundManager.instance.fixEFS();
                //this.rocket_model.transform.localRotation *=
                //Quaternion.AngleAxis(360.0f / 10.0f * Time.deltaTime,
                //Vector3.up);
                break;
        }
    }

    // 키 입력을 조사해 그 결과를 바탕으로 맴버 변수 key의 값을 갱신한다.
    private void get_input()
    {
        this.key.up = false;
        this.key.down = false;
        this.key.right = false;
        this.key.left = false;
        // ↑키가 눌렸으면 true를 대입.
        this.key.up |= Input.GetKey(KeyCode.UpArrow);
        this.key.up |= Input.GetKey(KeyCode.Keypad8);
        // ↓키가 눌렸으면 true를 대입.
        this.key.down |= Input.GetKey(KeyCode.DownArrow);
        this.key.down |= Input.GetKey(KeyCode.Keypad2);
        // →키가 눌렸으면 true를 대입.
        this.key.right |= Input.GetKey(KeyCode.RightArrow);
        this.key.right |= Input.GetKey(KeyCode.Keypad6);
        // ←키가 눌렸으면 true를 대입..
        this.key.left |= Input.GetKey(KeyCode.LeftArrow);
        this.key.left |= Input.GetKey(KeyCode.Keypad4);
        // Z 키가 눌렸으면 true를 대입.
        this.key.pick = Input.GetKeyDown(KeyCode.Z);
        // X 키가 눌렸으면 true를 대입.
        this.key.action = Input.GetKeyDown(KeyCode.X);
    }

    // 키 입력에 따라 실제로 이동시키는 처리를 한다.
    private void move_control()
    {
        Vector3 move_vector = Vector3.zero; // 이동용 벡터.
        Vector3 position = this.transform.position; // 현재 위치를 보관.
        Vector3 rotate_vector = Vector3.zero;
        Vector3 target = Vector3.zero;

        float MOVE_BACK = 0.3f; //뒤로가는 속도

        bool is_moved = false;
        if (this.key.right)
            rotate_vector += new Vector3(0, MOVE_ROTATE, 0);
        if (this.key.left)
            rotate_vector += new Vector3(0, -MOVE_ROTATE, 0);
        if (this.key.up)
        {
            move_vector += Vector3.forward;
            target = goDir.transform.position;
            MOVE_BACK = 1;
            is_moved = true;
        }
        if (this.key.down)
        {
            move_vector += Vector3.back;
            target = backDir.transform.position;
            is_moved = true;
        }

        transform.Rotate(rotate_vector);
        move_vector.Normalize(); // 길이를 1로.
        move_vector *= MOVE_SPEED * Time.deltaTime; // 속도×시간＝거리.
        position.y = 0.0f; // 높이를 0으로 한다.
        transform.position = Vector3.MoveTowards(transform.position, target, move_vector.magnitude * MOVE_BACK);
        MapCamera.transform.position = transform.position + new Vector3(0, 50, 0);

        // 새로 구한 위치(position)의 높이를 현재 높이로 되돌린다.
        position.y = this.transform.position.y;
        // 실제 위치를 새로 구한 위치로 변경한다.
      //  this.transform.position = position;
        animator.SetFloat("spd", move_vector.magnitude);
        
        if (is_moved)
        {
            // 들고 있는 아이템에 따라 '체력 소모 정도'를 조사한다.
            float consume = this.item_root.getConsumeSatiety(this.carried_item);
            // 가져온 '소모 정도'를 체력에서 뺀다.
            this.game_status.addSatiety(-consume * Time.deltaTime);

            animator.SetBool("fix", false);
            if (SoundManager.instance.myAudio.isPlaying)
            {
                SoundManager.instance.myAudio.Stop();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        GameObject other_go = other.gameObject;
        // 트리거의 GameObject 레이어 설정이 Item이라면.
        if (other_go.layer == LayerMask.NameToLayer("Item"))
        {
            // 아무 것도 주목하고 있지 않으면.
            if (this.closest_item == null)
            {
                if (this.is_other_in_view(other_go))
                { // 정면에 있으면.
                    this.closest_item = other_go; // 주목한다.
                }
                // 뭔가 주목하고 있으면.
            }
            else if (this.closest_item == other_go)
            {
                if (!this.is_other_in_view(other_go))
                { // 정면에 없으면.
                    this.closest_item = null; // 주목을 그만둔다.
                }
            }
        }
        // 트리거의 GameObject의 레이어 설정이 Event라면.
        else if (other_go.layer == LayerMask.NameToLayer("Event"))
        {
            // 아무것도 주목하고 있지 않으면.
            if (this.closest_event == null)
            {
                if (this.is_other_in_view(other_go))
                { // 정면에 있으면.
                    this.closest_event = other_go; // 주목한다.
                }
                // 뭔가에 주목하고 있으면.
            }
            else if (this.closest_event == other_go)
            {
                if (closest_event.tag == "FireRespawn")
                    Debug.Log("FireSpot");
                if (closest_event.tag == "CampFire")
                    Debug.Log("Fire!!");

                if (!this.is_other_in_view(other_go))
                { // 정면에 없으면.
                    this.closest_event = null; // 주목을 그만둔다.
                }
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        GameObject other_go = other.gameObject;

        if (other_go.tag == "Chestnut")
            game_status.addSatiety(-0.1f);
    }

    // 들고 있는 아이템의 종류와 주목하는 이벤트의 종류를 보고 이벤트 시작
    private bool is_event_ignitable()
    {
        bool ret = false;
        do
        {
            if (this.closest_event == null)
            { // 주목 이벤트가 없으면.
                break; // false를 반환한다.
            }
            // 들고 있는 아이템 종류를 가져온다.
            Item.TYPE carried_item_type =
            this.item_root.getItemType(this.carried_item);
            // 들고 있는 아이템 종류와 주목하는 이벤트의 종류에서.
            // 이벤트가 가능한지 판정하고, 이벤트 불가라면 false를 반환한다.
            if (!this.event_root.isEventIgnitable(
            carried_item_type, this.closest_event))
            {
                break;
            }
            ret = true; // 여기까지 오면 이벤트를 시작할 수 있다고 판정!.
        } while (false);
        return (ret);
    }

    // 주목을 그만두게 한다.
    void OnTriggerExit(Collider other)
    {
        if (this.closest_item == other.gameObject)
        {
            this.closest_item = null; // 주목을 그만둔다.
        }
    }

    // 주목 중이거나 들고 있는 아이템이 있을 때 표시
    void OnGUI()
    {
        float x = 20.0f;
        float y = Screen.height - 40.0f;
        // 들고 있는 아이템이 있다면.
        if (this.carried_item != null)
        {
            GUI.Label(new Rect(x, y, 200.0f, 20.0f), "Z:버린다", guistyle);
            do
            {
                if (this.is_event_ignitable())
                {
                    break;
                }
                if (item_root.getItemType(this.carried_item) == Item.TYPE.IRON)
                {
                    break;
                }
                GUI.Label(new Rect(x + 100.0f, y, 200.0f, 20.0f), "X:먹는다",
                guistyle);
            } while (false);
        }
        else
        {
            // 주목하고 있는 아이템이 있다면.
            if (this.closest_item != null)
            {
                GUI.Label(new Rect(x, y, 200.0f, 20.0f), "Z:줍는다", guistyle);
            }
        }

        switch (this.step)
        {
            case STEP.EATING:
                GUI.Label(new Rect(x, y, 200.0f, 20.0f),
                "우적우적우물우물……", guistyle);
                break;
            case STEP.REPAIRING:
                GUI.Label(new Rect(x + 200.0f, y, 200.0f, 20.0f), "수리중",
                guistyle);
                break;
        }

        if (this.is_event_ignitable())
        { // 이벤트가 시작 가능한 경우.
          // 이벤트용 메시지를 취득.
            string message =
            this.event_root.getIgnitableMessage(this.closest_event);
            GUI.Label(new Rect(x + 100.0f, y, 200.0f, 20.0f),
            "X:" + message, guistyle);
        }
    }

    private void pick_or_drop_control()
    {
        do
        {
            if (!this.key.pick)
            { // '줍기/버리기'키가 눌리지 않았으면.
                break; // 아무것도 하지 않고 메소드 종료.
            }
            if (this.carried_item == null)
            { // 들고 있는 아이템이 없고.
                if (this.closest_item == null)
                {// 주목 중인 아이템이 없으면.
                    break; // 아무것도 하지 않고 메소드 종료.
                }
                // 주목 중인 아이템을 들어올린다.
                this.carried_item = this.closest_item;
                if (SoundManager.instance.myAudio.isPlaying)
                {
                    SoundManager.instance.myAudio.Stop();
                }
                else
                    SoundManager.instance.pickEFS();

                // 들고 있는 아이템을 자신의 자식으로 설정.
                this.carried_item.transform.parent = this.transform;
                // 2.0f 위에 배치(머리 위로 이동).
                this.carried_item.transform.localPosition = Vector3.up * 2.0f;
                // 주목 중 아이템을 없앤다.
                this.closest_item = null;
            }
            else
            { // 들고 있는 아이템이 있을 경우.
              // 들고 있는 아이템을 약간(1.0f) 앞으로 이동시켜서.
                this.carried_item.transform.localPosition = Vector3.forward * 1.0f;
                this.carried_item.transform.parent = null;// 자식 설정을 해제.
                this.carried_item = null; // 들고 있던 아이템을 없앤다.
            }
        } while (false);
    }

    // 접촉한 물건이 자신의 정면에 있는지 판단한다.
    private bool is_other_in_view(GameObject other)
    {
        bool ret = false;
        do
        {
            Vector3 heading = // 자신이 현재 향하고 있는 방향을 보관.
            this.transform.TransformDirection(Vector3.forward);
            Vector3 to_other = // 자신 쪽에서 본 아이템의 방향을 보관.
            other.transform.position - this.transform.position;
            heading.y = 0.0f;
            to_other.y = 0.0f;
            heading.Normalize(); // 길이를 1로 하고 방향만 벡터로.
            to_other.Normalize(); // 길이를 1로 하고 방향만 벡터로.
            float dp = Vector3.Dot(heading, to_other); // 양쪽 벡터의 내적을 취득.
            if (dp < Mathf.Cos(45.0f))
            { // 내적이 45도인 코사인 값 미만이면.
                break; // 루프를 빠져나간다.
            }
            ret = true; // 내적이 45도인 코사인 값 이상이면 정면에 있다.
        } while (false);
        return (ret);
    }
}
