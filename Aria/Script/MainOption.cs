using UnityEngine;
using System.Collections;

public class MainOption : MonoBehaviour {


    public GameObject pauseMenuCanvas;
    public GameObject OptionMenu;
    public Transform Button_Op_F;
    public Animator Op_P_L;
    public GameObject Button_Front;

    void Start()
    {
        pauseMenuCanvas.SetActive(false);
        OptionMenu.SetActive(false);
    }
    void Update()
    {

    }
    public void Option()
    {
        StartCoroutine(this.Option_Butoon());
        
    }
    public void Option_Exit()
    {
        pauseMenuCanvas.SetActive(false);
        OptionMenu.SetActive(false);
    }
    IEnumerator Option_Butoon()
    {
        GetComponent<AudioSource>().Play();
        Instantiate(Button_Front, Button_Op_F.position, Quaternion.identity);
        Op_P_L.SetBool("P_Motion", true);
        yield return new WaitForSeconds(1);
        Op_P_L.SetBool("P_Motion", false);
        yield return new WaitForSeconds(1);
        pauseMenuCanvas.SetActive(true);
        OptionMenu.SetActive(true);
    }
}
