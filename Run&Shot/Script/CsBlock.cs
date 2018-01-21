using UnityEngine;
using System.Collections;

public class CsBlock : MonoBehaviour {
    private GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x + 10 < player.transform.position.x)
        {
            GameRoot.lm.Coin.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }
}
