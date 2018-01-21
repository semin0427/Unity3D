using UnityEngine;
using System.Collections;

public class dataSave : MonoBehaviour {

    public static int iScore;
    public static float fTime;

	// Use this for initialization
	void Start () {
        if(Application.loadedLevel == 0)
        {
            iScore = 0;
            fTime = 0;
        }
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.loadedLevel == 1)
        {
            iScore = UIManger.score;
            fTime = UIManger.fTime;
        }
    }
}
