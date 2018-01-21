using UnityEngine;
using System.Collections;

public class EndingBoat : MonoBehaviour {

    GameObject destination;

    float _time;
	// Use this for initialization
	void Start () {
        destination = GameObject.Find("Goal");
	}
	
	// Update is called once per frame
	void Update () {
        _time += Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination.transform.position, 3.0f * Time.deltaTime);

        if (_time > 7)
            Application.LoadLevel(0);
	}
}
