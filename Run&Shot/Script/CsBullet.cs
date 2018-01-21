using UnityEngine;
using System.Collections;

public class CsBullet : MonoBehaviour {

    public float speed;
    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -speed * Time.deltaTime, 0);

        if (player.transform.position.x + 10 < transform.position.x)
            Destroy(this.gameObject);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Archer" || col.tag == "Sword")
        {
            col.GetComponent<CsEnemy>().HP -= 1;
            Destroy(this.gameObject);
        }
    }
}
