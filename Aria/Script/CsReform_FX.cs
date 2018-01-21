using UnityEngine;
using System.Collections;

public class CsReform_FX : MonoBehaviour {

    public GameObject ReformImage;
    public Transform Reform;
    public int a;
	// Use this for initialization
	void Start () {
        a = 0;
    //    ReformImage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (CsPlayer.B_Gyohwa)
        {
            a += 1;
      //      ReformImage.SetActive(true);
        }
        else
        {
            a = 0;
       //     ReformImage.SetActive(false);
        }


        if (a == 1)
        {
            Instantiate(ReformImage, Reform.position, Reform.rotation);
      //      ReformImage.SetActive(true);
        }

    }
}
