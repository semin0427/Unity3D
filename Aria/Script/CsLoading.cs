using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CsLoading : MonoBehaviour {

    public Scrollbar Load;
    public float loadtime;
    static public float percent;

	// Use this for initialization
	void Start () {
        Load.size = 0.0f;
        Load.value = 0.0f;
        loadtime = 0.0f;
        percent = 0;

    }
	
	// Update is called once per frame
	void Update () {
        loadtime += Time.deltaTime;

        Load.size = loadtime / 5;

        percent = Mathf.Round(Load.size * 100) ;

        if (Load.size >= 1)
            Load.size = 1;

        if (loadtime >= 7)
            Application.LoadLevel(3);

        //if(loadtime > 0.5)
        //{
        //    Load.size += 0.1f;
        //    loadtime = 0;
        //}
    }
}
