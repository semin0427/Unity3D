using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip soundJump;
    public AudioClip soundGetItem;
    public AudioClip soundGameOver;
    public AudioClip soundShoot;
    public AudioClip soundHit;

    public AudioSource myAudio;
    public static SoundManager instance; 

    void Awake() 
    {
        if (SoundManager.instance == null)  //게임시작했을때 이 instance가 없을때
            SoundManager.instance = this;  // instance를 생성
    }
    // Use this for initialization
    void Start()
    {
 //       myAudio = GetComponent<AudioSource>();  //myAudio에 컴퍼넌트에있는 AudioSource넣기
    }

    public void PlayerJump()
    {
        myAudio.PlayOneShot(soundJump);
    }

    public void PlayerGetItem()
    {
        myAudio.PlayOneShot(soundGetItem);
    }

    public void PlayerShoot()
    {
        myAudio.PlayOneShot(soundShoot);
    }

    public void PlayerLose()
    {
        myAudio.PlayOneShot(soundGameOver);
    }

    public void PlayerHit()
    {
        myAudio.PlayOneShot(soundHit);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

