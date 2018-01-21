using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemCreator : MonoBehaviour {

    public GameObject item;
    private int item_count = 0; // 생성한 블록의 개수
    private GameObject ItemBox;

    // Use this for initialization
    void Start () {
        ItemBox = GameObject.Find("ItemBox");
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void createItem(Vector3 _pos)
    {
        if (_pos.x > 5)
        {
            GameObject go = GameObject.Instantiate(this.item) as GameObject;
            go.transform.parent = ItemBox.transform;
            go.transform.position = _pos + new Vector3(0, 3, 0); // 블록의 위치를 이동
            this.item_count++; // 블록의 개수를 증가
            GameRoot.lm.Item.Add(this.gameObject);
        }
    }
}
