using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
  

    Animator At;
	// Use this for initialization
	void Start () {
        At = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (CsPlayer.Energy==5)
        {
            At.SetFloat("Health", 5);
        }
        if (CsPlayer.Energy==4)
        {
            At.SetFloat("Health", 4);
        }
        if (CsPlayer.Energy==3)
        {
            At.SetFloat("Health", 3);
        }
        if (CsPlayer.Energy==2)
        {
            At.SetFloat("Health", 2);
        }
        if (CsPlayer.Energy==1)
        {
            At.SetFloat("Health", 1);
        }
        if (CsPlayer.Energy==0)
        {
            At.SetFloat("Health", 0);
        }
    }
}
