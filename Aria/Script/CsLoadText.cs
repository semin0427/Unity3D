using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CsLoadText : MonoBehaviour {

    public Text Loading;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        LoadPercent();
    }


    public void LoadPercent()
    {
        Loading.text = CsLoading.percent.ToString() + "%";
    }
}
