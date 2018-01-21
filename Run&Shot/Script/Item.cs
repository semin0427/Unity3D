using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    public GameObject rotateScreen;

    int itemNum;
    float effectTime;
    public bool getItem;
    bool screenRotate;

    bool itemTime;

    Vector3 normalPos;

    GameObject player;

    // Use this for initialization
    void Start () {
        itemNum = 0;
        effectTime = 0;
        getItem = false;
        screenRotate = false;
        normalPos = transform.position;
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        effectTime += Time.deltaTime;

        if (itemTime)
            transform.position = normalPos + new Vector3(0, -20, 0);
        else
            transform.position = normalPos;

        if (getItem)
        {
            UpsideDown();
            this.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void UpsideDown()   //화면 회전
    {
        if (effectTime == 0)
            UIManger.score += 10;

        effectTime = 0;
        screenRotate = true;

        if (Camera.main.transform.rotation.z < 0)
        {
            getItem = false;
            screenRotate = false;
        }

        if (!screenRotate)
            Camera.main.transform.rotation = Quaternion.identity;
        else
            Camera.main.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 500));
    }
}
