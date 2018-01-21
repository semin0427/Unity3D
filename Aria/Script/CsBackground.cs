using UnityEngine;
using System.Collections;

public class CsBackground : MonoBehaviour
{
    public float speed; //이동 속도
    public float fever;
    public float Distance;
    //CsPlayer CsPlayer;
    Transform T;
   
    // Use this for initialization
    void Start()
    {
        fever = 1;
        //CsPlayer = GameObject.Find("Player(Clone)").GetComponent<CsPlayer>();
       //  G = GameObject.Find("Empty").GetComponent<GameController>();
        T = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //왼쪽방향으로 speed만큼 이동하게됩니다.
        T.Translate(Vector2.left * speed* fever * Time.deltaTime);

        //x좌표가 -12.8보다 작아지게 되면...
        if (T.position.x < -30.0f)
        {
            //위치를 0, 0 으로 옮겨줍니다.
            T.position = new Vector2(Distance, 0);
        }

        if (CsPlayer.B_Gyohwa == true)
        {
            fever = 2.0f;
        }
        else
        {
            fever = 1.0f;
        }
    }
}

