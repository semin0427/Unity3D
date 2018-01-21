using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
    }

    public void gameStart()
    {
        SceneManager.LoadScene("Game");
    }
}
