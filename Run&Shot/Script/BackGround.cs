using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour
{

    public GameObject player;

    public GameObject bGround1;
    public GameObject bGround2;

    int moveTime;

    // Use this for initialization
    void Start()
    {
        moveTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > 90 + moveTime * 60)
        {
            BgMove(bGround1);
            BgMove(bGround2);
        }
    }

    void BgMove(GameObject bg)
    {
        if (bg == bGround1 && moveTime % 2 == 0)
        {
            bg.transform.position = new Vector3(120 + moveTime * 60, 0, 0);
            moveTime += 1;
        }
        else if (bg == bGround2 && moveTime % 2 == 1)
        {
            bg.transform.position = new Vector3(120 + moveTime * 60, 0, 0);
            moveTime += 1;
        }
    }
}
