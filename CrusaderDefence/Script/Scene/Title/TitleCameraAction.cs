using UnityEngine;
using System.Collections;

public class TitleCameraAction : MonoBehaviour {

    Vector3 originalPos;
    Vector3 frontAngel;

    Vector3 optionView;
    public float journeyTime = 30.0F;
    private float startTime;
    public Animator workerAni;

    float aniTime;

    void Start()
    {
        Time.timeScale = 1;
        SoundManager.instance.myAudios.Stop();
        SoundManager.instance.myAudios2.Stop();
        startTime = Time.time;
        originalPos = transform.position;
        frontAngel = GameObject.Find("angelStatue").transform.position - new Vector3(0,0,30);
        optionView = GameObject.Find("WorkerPos").transform.position;
        SoundManager.instance.SoundPlay(SoundManager.instance.MainBgm);
    }

    void Update()
    {
        
    }

    public void moveToAngel()
    {
        transform.position = Vector3.Lerp(transform.position, frontAngel + new Vector3(0, 6.31f,0), 3 * Time.deltaTime);
    }

    public void lookWorker()
    {
        workerAni.SetBool("backMain", false);
        Vector3 dirToTarger = this.optionView - this.transform.position;
        Vector3 look = Vector3.Slerp(this.transform.forward, dirToTarger.normalized, 3 * Time.deltaTime);
        this.transform.rotation = Quaternion.LookRotation(look, Vector3.up);
        aniTime = 0;
    }

    public void BackFromOption()
    {
        workerAni.SetBool("backMain", true);

        aniTime += Time.deltaTime;

        if (aniTime > 0.7f)
        {
            Vector3 dirToTarger = this.frontAngel - this.transform.position;
            Vector3 look = Vector3.Slerp(this.transform.forward, dirToTarger.normalized, 2 * Time.deltaTime);
            this.transform.rotation = Quaternion.LookRotation(look, Vector3.up);
            workerAni.SetBool("backMain", false);
        }
    }

    public void BackFromStage()
    {
        transform.rotation = new Quaternion(0, 0, 0, 1);
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 1, 9.6f), 3 * Time.deltaTime);
    }
    
}
