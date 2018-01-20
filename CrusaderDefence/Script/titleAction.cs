using UnityEngine;
using System.Collections;

public class titleAction : MonoBehaviour {

    public GameObject Enemy;
    GameObject mainCamera;

    public Animator ma;
    Fade fader;
	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera");
        fader = GameObject.Find("FadeInPrefab").GetComponent<Fade>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject != mainCamera)
        {
            if (Vector3.Distance(transform.position, Enemy.transform.position) > 4)
            {
                transform.position = Vector3.MoveTowards(transform.position, Enemy.transform.position, 3 * Time.deltaTime);
            }
            else
            {
                ma.SetBool("Fight", true);
                Time.timeScale = 0.2f;

                fader.FadeIn();
                fader.introSword();
            }
        }

        if (gameObject == mainCamera)
        {
            transform.position = Vector3.MoveTowards(transform.position, Enemy.transform.position, 10 * Time.deltaTime);
            if (transform.rotation.x <= 25)
            {
                transform.Rotate(Vector3.right, -2.5f * Time.deltaTime, Space.World);
            }
        }
    }
}
