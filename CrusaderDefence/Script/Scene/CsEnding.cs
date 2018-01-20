using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CsEnding : MonoBehaviour {

    public GameObject Horse;
    public GameObject Spear;
    public GameObject Sword;
    public GameObject Bow;

    public GameObject UI;
    public Text title;
    public Text Contents;
    public GameObject Thanks;

    float _time;
    // Use this for initialization
    void Start () {
        Spear.SetActive(false);
        Sword.SetActive(false);
        Bow.SetActive(false);
        Thanks.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        _time += Time.deltaTime;

        if (_time < 3.0f)
        {
            Horse.GetComponent<Animator>().SetBool("isDetect", true);
            Horse.transform.position = Vector3.MoveTowards(Horse.transform.position, new Vector3(-93, 0, 62.4f), 20 * Time.deltaTime);
        }
        else if (_time < 6.0f)
        {
            Horse.SetActive(false);
            Spear.SetActive(true);
            Spear.GetComponent<Animator>().SetBool("isDetect", true);
            Spear.GetComponent<Animator>().SetBool("isAttack", true);
            title.text = "Crusader\nDefence";
            Contents.text = "";
        }
        else if (_time < 9.0f)
        {
            Spear.SetActive(false);
            Bow.SetActive(true);
            Bow.GetComponent<Animator>().SetBool("isDetect", true);
            Bow.GetComponent<Animator>().SetBool("isAttack", true);
            title.text = "Made By";
            Contents.text = "게임 소프트웨어\nB277036 장세민";
        }
        else if (_time < 12.0f)
        {
            Bow.SetActive(false);
            Sword.SetActive(true);
            Sword.GetComponent<Animator>().SetBool("isDetect", true);
            Sword.transform.position = Vector3.MoveTowards(Sword.transform.position, new Vector3(-4.79f, 0, 2.75f), 3 * Time.deltaTime);
            title.text = "Thanks to";
            Contents.text = "Prof.김 예진";
        }
        else if (_time < 12.8f)
        {
            Sword.GetComponent<Animator>().SetBool("isAttack", true);
            title.enabled = false;
            Contents.enabled = false;
            Thanks.SetActive(true);
        }
        else
        {
            if (!SoundManager.instance.myAudios.isPlaying)
                SoundManager.instance.SoundPlay(SoundManager.instance.IntroSword);

            if (_time > 13.8f)
                SoundManager.instance.myAudios.Stop();

            GameObject.Find("FadeInPrefab").GetComponent<Fade>().FadeOut2Main();
        }
	}
}
