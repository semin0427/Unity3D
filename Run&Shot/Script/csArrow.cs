using UnityEngine;
using System.Collections;

public class csArrow : MonoBehaviour {
    public float speed;

    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x + 20)
            Destroy(gameObject);

        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "playerbox")
        {
            if (PlayerControl.bulletCount == 0)
            {
                SoundManager.instance.PlayerHit();
                SoundManager.instance.PlayerLose();
                Application.LoadLevel(2);
            }
            else
            {
                SoundManager.instance.PlayerHit();
                PlayerControl.bulletCount = 0;
            }

            Destroy(this.gameObject);
        }
    }
}
