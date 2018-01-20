using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource myAudios;
    public AudioSource myAudios2;
    public AudioSource myAudios3;

    public AudioClip IntroBgm;
    public AudioClip IntroSword;

    public AudioClip MainBgm;

    public AudioClip ButtonClick;

    public AudioClip SwordEFS;
    public AudioClip BowEFS;
    public AudioClip SpearEFS;
    public AudioClip AxeEFS;
    public AudioClip CatapultEFS;
    public AudioClip HorseEFS;
    public AudioClip DieEFS;
    public AudioClip ShoutEFS;
    public AudioClip GroaningEFS;

    public static SoundManager instance; //다른 스크립트에서 이스크립트에있는 함수를 호출할때 쓰임

    void Awake()  // Start함수보다 먼저 호출됨
    {
        if (SoundManager.instance == null)  //게임시작했을때 이 instance가 없을때
            SoundManager.instance = this;  // instance를 생성

        DontDestroyOnLoad(this.gameObject);
    }


	// Use this for initialization
	void Start ()
    {
        if (!myAudios.isPlaying)
            SoundPlay(IntroBgm);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
            Application.LoadLevel(5);
        if (Input.GetKeyDown(KeyCode.W))
            Application.LoadLevel(6);
        if (Input.GetKeyDown(KeyCode.E))
            Application.LoadLevel(7);
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(9);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            Time.timeScale = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Time.timeScale = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Time.timeScale *= 2;
    }
    
    public void SoundPlay(AudioClip _SFX)
    {
        myAudios.PlayOneShot(_SFX);
    }

    public void SoundPlay2(AudioClip _SFX)
    {
        myAudios2.PlayOneShot(_SFX);
    }

    public void onoff(bool _bool)
    {
        gameObject.SetActive(_bool);
    }
}
