using UnityEngine;
using System.Collections;

public class HurdleCreater : MonoBehaviour
{

    //프리팹을 넣어줄 공개변수들
    public GameObject hurdle1_1;
    public GameObject hurdle1_2;
    public GameObject hurdle1_3;
    public GameObject hurdle1_4;
    public GameObject hurdle1_5;
    public GameObject hurdle1_6;
    public GameObject hurdle1_7;
    public GameObject hurdle1_8;
    public GameObject hurdle1_9;
    public GameObject hurdle1_10;
    public GameObject hurdle2_1;
    public GameObject hurdle2_2;
    public GameObject hurdle2_3;
    public GameObject hurdle2_4;
    public GameObject hurdle2_5;
    public GameObject hurdle2_6;
    public GameObject hurdle2_7;
    public GameObject hurdle2_8;
    public GameObject hurdle2_9;
    public GameObject hurdle2_10;
    float timer = 0; //누적시간을 저장할 변수
    public int birdNum;
    // 스테이지 별 장애물 소환 time
    float Set_Time;
    // 스태이지 별 위치 
    Transform Hurdles_T;
    CsButton Button;
    void Start()
    {
        
        Button = GameObject.Find("MainCanvas").GetComponent<CsButton>();
        Hurdles_T = GetComponent<Transform>();
        birdNum = 1;
        switch (Button.M_1)
        {
            case 1:
                Instantiate(hurdle1_1, Hurdles_T.position, Quaternion.identity);
                Set_Time = 10.0f;
                break;
            case 2:
                Instantiate(hurdle2_1, Hurdles_T.position, Quaternion.identity);
                Set_Time = 7.5f;
                break;
            case 3:
                CreateEnemy1();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //시간을 누적시킴
        timer += Time.deltaTime;

        //2초가 지나면...
        if (timer > Set_Time)
         {
         switch (Button.M_1)
          {
            case 1:
                 CreateEnemy1();
                    break;
                case 2:
                 CreateEnemy2();
                    break;
                case 3:
                  CreateEnemy1();
                    break;
          }
            //누적시간 초기화
            timer = 0;
        }
    }

    //////////////
    /// 적 생성 ///
    //////////////
    void CreateEnemy1()
    {
        //랜덤하게 생성하기위해 랜덤값을 받습니다.
        //int birdNum = Random.Range(1, 10);
        
        birdNum += 1;
        //랜덤값에 따라 다른 적을 생성합니다.
        switch (birdNum)
        {
            case 1:
                Instantiate(hurdle1_1, Hurdles_T.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(hurdle1_2, Hurdles_T.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(hurdle1_3, Hurdles_T.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(hurdle1_4, Hurdles_T.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(hurdle1_5, Hurdles_T.position, Quaternion.identity);
                break;
            case 6:
                Instantiate(hurdle1_6, Hurdles_T.position, Quaternion.identity);
                break;
            case 7:
                Instantiate(hurdle1_7, Hurdles_T.position, Quaternion.identity);
                break;
            case 8:
                Instantiate(hurdle1_8, Hurdles_T.position, Quaternion.identity);
                break;
            case 9:
                Instantiate(hurdle1_9, Hurdles_T.position, Quaternion.identity);
                break;
            case 10:
                Instantiate(hurdle1_10, Hurdles_T.position, Quaternion.identity);
                birdNum = 0;
                break;

        }
    }
    void CreateEnemy2()
    {
        //랜덤하게 생성하기위해 랜덤값을 받습니다.
        //int birdNum = Random.Range(1, 10);

        birdNum += 1;
        //랜덤값에 따라 다른 적을 생성합니다.
        switch (birdNum)
        {
            case 1:
                Instantiate(hurdle2_1, Hurdles_T.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(hurdle2_2, Hurdles_T.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(hurdle2_3, Hurdles_T.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(hurdle2_4, Hurdles_T.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(hurdle2_5, Hurdles_T.position, Quaternion.identity);
                break;
            case 6:
                Instantiate(hurdle2_6, Hurdles_T.position, Quaternion.identity);
                break;
            case 7:
                Instantiate(hurdle2_7, Hurdles_T.position, Quaternion.identity);
                break;
            case 8:
                Instantiate(hurdle2_8, Hurdles_T.position, Quaternion.identity);
                break;
            case 9:
                Instantiate(hurdle2_9, Hurdles_T.position, Quaternion.identity);
                break;
            case 10:
                Instantiate(hurdle2_10, Hurdles_T.position, Quaternion.identity);
                birdNum = 0;
                break;

        }
    }
}
