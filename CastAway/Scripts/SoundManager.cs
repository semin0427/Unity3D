using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource mainBGM;
    public AudioSource myAudio;
    public AudioClip pickItem;
    public AudioClip eatApple;
    public AudioClip fixBoat;
    public AudioClip attackWilson;

    public static SoundManager instance;

	// Use this for initialization
	void Awake () {
        if (SoundManager.instance == null)  //게임시작했을때 이 instance가 없을때
        {
            SoundManager.instance = this;  // instance를 생성
            myAudio.Stop();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void pickEFS()
    {
        myAudio.PlayOneShot(pickItem);
    }
    public void eatEFS()
    {
        myAudio.PlayOneShot(eatApple);
    }
    public void fixEFS()
    {
        myAudio.PlayOneShot(fixBoat);
    }
    public void atkEFS()
    {
        myAudio.PlayOneShot(attackWilson);
    }
}
