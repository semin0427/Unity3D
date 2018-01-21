using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CsText : MonoBehaviour {

   // CsPlayer CsPlayer;
    public Text effect;
    public Text health;

	// Use this for initialization
	void Start () {
        //CsPlayer = GameObject.Find("Player(Clone)").GetComponent<CsPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
        Health();
        Effect();
    }

    public void Health()
    {
        health.text = "Health: " + CsPlayer.Energy.ToString();
    }
    public void Effect()
    {
        effect.text = "effect: " + CsPlayer.Gyohwa.ToString();

    }
}
