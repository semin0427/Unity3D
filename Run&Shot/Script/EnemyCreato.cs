using UnityEngine;
using System.Collections;

public class EnemyCreato : MonoBehaviour {

    public GameObject Sword;
    public GameObject Bow;

    private int enemy_count = 0; // 생성한 블록의 개수
    private GameObject Enemy;

    // Use this for initialization
    void Start () {
        Enemy = GameObject.Find("Enemy");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void createSword(Vector3 _pos)
    {
        if (_pos.x > 5)
        {
            GameObject go = GameObject.Instantiate(this.Sword) as GameObject;
            go.transform.parent = Enemy.transform;
            go.transform.position = _pos + new Vector3(0, 0.5f, 0); // 블록의 위치를 이동
            this.enemy_count++; // 블록의 개수를 증가
            GameRoot.lm.Enemy.Add(this.gameObject);
        }
    }

    public void createBow(Vector3 _pos)
    {
        if (_pos.x > 5)
        {
            GameObject go = GameObject.Instantiate(this.Bow) as GameObject;
            go.transform.parent = Enemy.transform;
            go.transform.position = _pos + new Vector3(0, 0.5f, 0); // 블록의 위치를 이동
            this.enemy_count++; // 블록의 개수를 증가
            GameRoot.lm.Enemy.Add(this.gameObject);
        }
    }
}
