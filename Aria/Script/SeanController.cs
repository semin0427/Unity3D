using UnityEngine;
using System.Collections;

public class SeanController : MonoBehaviour {
    GameObject character1;
    GameObject character2;
    GameObject character3;
    GameObject Map1;
    GameObject Map2;
    GameObject Map3;
    GameObject Map2_Unlock;
    
    // Use this for initialization
    void Start () {
        //character1 = GameObject.Find("Character1");
        //character2 = GameObject.Find("Character2");
       // character3 = GameObject.Find("Character3");
        Map1 = GameObject.Find("Map1");
        Map2 = GameObject.Find("Map2");
        Map3 = GameObject.Find("Map3");
        Map2_Unlock = GameObject.Find("Map2_Unlock");
                
        //character1.SetActive(false);
       // character2.SetActive(false);
       // character3.SetActive(false);
        Map1.SetActive(false);
        Map2.SetActive(false);
        Map3.SetActive(false);
        Map2_Unlock.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
