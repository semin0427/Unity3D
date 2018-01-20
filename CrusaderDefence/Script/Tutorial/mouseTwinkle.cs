using UnityEngine;
using System.Collections;

public class mouseTwinkle : MonoBehaviour {

    public GameObject[] pos;

    public static int tutorialNum;
    public static float twinkleTime;

	// Use this for initialization
	void Start () {
        foreach (var _pos in pos)
            _pos.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
    }

    public static void twinkle(int tutNum, GameObject[] _mouse, GameObject[] _lClick)
    {
        twinkleTime += Time.deltaTime;

        if (twinkleTime >= 0.0f && twinkleTime < 0.5f)
        {
            _mouse[tutNum].SetActive(true);
            _lClick[tutNum].SetActive(false);
        }
        else if (twinkleTime >= 0.5f && twinkleTime < 1.0f)
        {
            _mouse[tutNum].SetActive(false);
            _lClick[tutNum].SetActive(true);
        }
        else
            twinkleTime = 0;
    }
}
