using UnityEngine;
using System.Collections;

public class tutorialImage : MonoBehaviour {

    GameObject _Text;
    float _blinkTime;
	// Use this for initialization
	void Start () {
        SoundManager.instance.myAudios.Stop();
        SoundManager.instance.myAudios2.Stop();
        SoundManager.instance.myAudios3.Stop();
        _Text = GameObject.Find("Text");
    }
	
	// Update is called once per frame
	void Update () {
        _blinkTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            Application.LoadLevel(2);

        if(_blinkTime < 0.4f)
        {
            _Text.SetActive(false);
        }
        else if(_blinkTime < 1.0f)
        {
            _Text.SetActive(true);
        }
        else
            _blinkTime = 0;
    }
}
