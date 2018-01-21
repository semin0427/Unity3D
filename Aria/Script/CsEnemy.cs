using UnityEngine;
using System.Collections;

public class CsEnemy : MonoBehaviour
{
    public float speed; //이동 속도


    //CsPlayer CsPlayer;

    
    // Use this for initialization
    void Start()
    {
        //CsPlayer = GameObject.Find("Player(Clone)").GetComponent<CsPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //왼쪽 방향으로 speed만큼 움직여 줍니다.
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // x 좌표가 -8 보다 작으면 해당 오브젝트를 삭제합니다.
        if (transform.position.x < -30)
        {
            //해당 오브젝트를 지워줌.
            Destroy(gameObject);
        }

        if (CsPlayer.B_Gyohwa == true)
        {
            Destroy(gameObject);
        }

    }

    //IsTrigger를 체크했을 때 발생하는 콜백수함
   
}