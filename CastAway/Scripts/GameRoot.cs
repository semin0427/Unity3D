using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void gameStart()
    {
        SceneManager.LoadScene(1);
    }
}
