using UnityEngine;
using System.Collections;

public class BlockCreator : MonoBehaviour {

    public GameObject[] blockPrefabs; // 블록을 저장할 배열
    private int block_count = 0; // 생성한 블록의 개수
    
    GameObject Ground;
    CoinCreator cc;
    EnemyCreato ec;
    ItemCreator ic;

    // Use this for initialization
    void Start () {
        Ground = GameObject.Find("Ground");
        cc = GameObject.Find("CoinCreator").GetComponent<CoinCreator>();
        ec = GameObject.Find("Enemy").GetComponent<EnemyCreato>();

        ic = GameObject.Find("ItemBox").GetComponent<ItemCreator>();
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void createBlock(Vector3 block_position)
    {
        float randomNum = Random.Range(0.0f, 3.0f);
        if (randomNum < 0.4f)
        {
            if (randomNum < 0.2f)
            {
                if(randomNum < 0.1f)
                    ec.createSword(block_position);
                else
                    ec.createBow(block_position);
            }
            else if (randomNum > 0.3f)
            {
                ic.createItem(block_position + new Vector3(0, 0, 0)); 
            }
        }
        else
            cc.createCoin(block_position + new Vector3(0, 1, 0));

        // 만들어야 할 블록의 종류(흰색인가 빨간색인가)를 구한다
        int next_block_type = this.block_count % this.blockPrefabs.Length; // % : 나머지를 구하는 연산자
                                                                           // 블록을 생성하고 go에 보관한다
        GameObject go = GameObject.Instantiate(this.blockPrefabs[next_block_type]) as GameObject;
        go.transform.parent = Ground.transform;
        go.transform.position = block_position; // 블록의 위치를 이동
        this.block_count++; // 블록의 개수를 증가
    }
}
