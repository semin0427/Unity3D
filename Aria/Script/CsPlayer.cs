using UnityEngine;
using System.Collections;

public class CsPlayer : MonoBehaviour
{
    public Vector2 jumpVelocity; //점프속도
    static public float Gyohwa;
    public float Gyohwatime;
    static public int Energy;
    static public bool B_Gyohwa;
    static public bool B_Colled;
    static public bool B_Collision;
    
    //업데이트 체크
    static public bool B_GyohwaUpdate;
    static public bool B_Delstate;

    // 교화 수치 업데이트용
    public float preGyohwa;

    //민들레 관련
    public float F_DelTime;
    
    //충돌
    static public float F_ColTime;
    
    //Animator A_Del;
    //애니메이터
    Animator A_Reform;
    Animation Ani;
    Transform T;
    Vector2 V2;
    Rigidbody2D D2;

    //GameObject
    public GameObject GameOverMenu;

    // Use this for initialization
    void Start()
    {
        GameOverMenu = GameObject.Find("gameoverCanvas");

        GameOverMenu.SetActive(false);
        //   Gyohwa = 0;
        B_Gyohwa = false;
        B_Delstate = false;
        Energy = 5;
        Gyohwatime = 0;
        B_Colled = false;
        B_Collision = false;
        A_Reform = GetComponent<Animator>();
        T = GetComponent<Transform>();
        V2 = T.position;
        D2 = GetComponent<Rigidbody2D>();
        
        // 업데이트 체크
        B_GyohwaUpdate = false;
        
        // 교화 수치 업데이트 용
        preGyohwa = 0.2f;
        // 교화 수치 초기화
        Gyohwa = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Energy != 0)
        {
            if (B_GyohwaUpdate == false)
            {
                Gyohwa += Time.deltaTime / 30.0f;
            }
            else if (B_GyohwaUpdate == true)
            {
                Gyohwa += preGyohwa;
                B_GyohwaUpdate = false;
            }
            B_Colled = false;
            //바닥면과 붙어있고, 스페이스키나 마우스 왼쪽버튼이 눌렸다면....
            if (Input.GetMouseButton(0) && B_Gyohwa == false&& Time.timeScale ==1.0f)
            {
                T.GetComponent<Rigidbody2D>().AddForce(jumpVelocity, ForceMode2D.Impulse);
            }
            if (B_Gyohwa == false && Gyohwa >= 1.0f)
            {
                B_Gyohwa = true;
            }
            if (B_Gyohwa == true)
            {
                Gyohwa -= 0.2f * Time.deltaTime;
                Gyohwatime += Time.deltaTime;
                A_Reform.SetBool("Reform", true);
                if (T.position.y > 1.0f)
                {
                    D2.AddForce(-jumpVelocity, ForceMode2D.Impulse);
                }
                if (T.position.y < 1.0f)
                {
                    D2.AddForce(jumpVelocity, ForceMode2D.Impulse);
                }
            }
            if (Gyohwatime >= 5f)
            {
                B_Gyohwa = false;
                Gyohwa = 0;
                Gyohwatime = 0;
                A_Reform.SetBool("Reform", false);
            }
            // 업데이트 체크
            //B_GyohwaUpdate = true;

            //민들레 먹을때
            if (B_Delstate == true)
            {
                A_Reform.SetBool("Dandelion", true);
                F_DelTime += Time.deltaTime;
            }
            if (F_DelTime >= 4f)
            {
                A_Reform.SetBool("Dandelion", false);
                B_Delstate = false;
                F_DelTime = 0;
            }

            if (B_Collision == true)
            {
                A_Reform.SetBool("Colision", true);
                F_ColTime += Time.deltaTime;
            }
            else if (B_Collision == false)
            {
                A_Reform.SetBool("Colision", false);
            }
            if (F_ColTime > 2 && B_Collision == true)
            {
                A_Reform.SetBool("Colision", false);
                B_Collision = false;
                F_ColTime = 0;

            }
        }
        if (Energy == 0)
        {
            GameOverMenu.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //충돌한 오브젝트의 이름이 runner일 경우
        //Tag를 사용하지 않고 name을 사용하는 방식
        if (B_Delstate == true)
        {
            return;
        }
        else if (B_Collision == true)
        {
            return;
        }
        else if (B_Delstate == false && B_Collision == false)
        {
            if (other.tag == "Enemy")
            {
                GetComponent<AudioSource>().Play();
                Energy = Energy - 1;
                B_Colled = true;
                B_Collision = true;
            }
        }
        if (other.tag == "Item_Reform")
        {
            B_GyohwaUpdate = true;
            Destroy(other.gameObject);
        }
        if (other.tag == "Item_Del")
        {
            B_Delstate = true;
            Destroy(other.gameObject);
        }
    }
}