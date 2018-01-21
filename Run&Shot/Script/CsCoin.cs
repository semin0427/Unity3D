using UnityEngine;
using System.Collections;

public class CsCoin : MonoBehaviour {

    public bool getCheck;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        getCheck = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (getCheck)
        {
            GameRoot.lm.Coin.Remove(this.gameObject);
            Debug.Log("get!");
            Destroy(this.gameObject);
        }

        if(transform.position.x + 10 < player.transform.position.x)
        {
            GameRoot.lm.Coin.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
	}
}
