using UnityEngine;
using System.Collections;

public class CsEnemyCreater : MonoBehaviour
{

    //프리팹을 넣어줄 공개변수들
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject iTem1;
    public GameObject iTem2;
     float timer = 0; //누적시간을 저장할 변수

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //시간을 누적시킴
        timer += Time.deltaTime;

        //2초가 지나면...
        if (timer > 2.0f)
        {
            //적 생성
            CreateEnemy();

            //누적시간 초기화
            timer = 0;
        }
    }

    //////////////
    /// 적 생성 ///
    //////////////
    void CreateEnemy()
    {
        //랜덤하게 생성하기위해 랜덤값을 받습니다.
        int birdNum = Random.Range(1, 5);

        //랜덤값에 따라 다른 적을 생성합니다.
        switch (birdNum)
        {
            case 1:
                Instantiate(enemy1, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(enemy2, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(enemy3, transform.position, Quaternion.identity);
                break;
            case 4:
                StartCoroutine("Enemy4");
                break;
            case 5:
                Instantiate(iTem1, transform.position, Quaternion.identity);
                break;

        }
    }
    IEnumerator Enemy4()
    {
        Instantiate(iTem2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(enemy2, transform.position, Quaternion.identity);
    }
 
}
