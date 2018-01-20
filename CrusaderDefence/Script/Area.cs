using UnityEngine;
using System.Collections;

public class Area : MonoBehaviour {
    SpriteRenderer SR;

	// Use this for initialization
	void Start () {
        SR = GetComponent<SpriteRenderer>();
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 0.3f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
