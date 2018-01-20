using UnityEngine;
using System.Collections;

public class FadeText : MonoBehaviour {

    public float _fadeTime;
    float _time;

    SpriteRenderer SR;
    TextMesh TM;

	// Use this for initialization
	void Start () {
        _time = 0;
        TM = GetComponent<TextMesh>();
        SR = GetComponentInChildren<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        disapear();
    }

    void disapear()
    {
        _time += Time.deltaTime;
        TM.color = new Color(TM.color.r, TM.color.g, TM.color.b, 1.5f - _time / _fadeTime);
        SR.color = new Color(SR.color.r, SR.color.g, SR.color.b, 1.5f - _time / _fadeTime);
    }


}
