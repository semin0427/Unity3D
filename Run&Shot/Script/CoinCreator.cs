using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinCreator : MonoBehaviour {

    public GameObject coinPrefabs;
    private int coin_count = 0; // 생성한 블록의 개수
    private GameObject CoinBox;

    // Use this for initialization
    void Start()
    {
        CoinBox = GameObject.Find("CoinCreator");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createCoin(Vector3 block_position)
    {
        GameObject go = GameObject.Instantiate(this.coinPrefabs) as GameObject;
        go.transform.parent = CoinBox.transform;
        go.transform.position = block_position; // 블록의 위치를 이동
        this.coin_count++; // 블록의 개수를 증가
        GameRoot.lm.Coin.Add(this.gameObject);
    }
}
