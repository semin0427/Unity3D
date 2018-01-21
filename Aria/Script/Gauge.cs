using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public Scrollbar S;
    public float g = 0;
    public int value;
    static public float Gauge_size;

    CsPlayer Player;
    // Use this for initialization
    void Start()
    {
       S.size = 0.0f;
        S.value = 0.0f;
        Gauge_size = 0.0f;

        //Player = GameObject.Find("Player1(Clone)").GetComponent<CsPlayer>();
    }

    // Update is called once per frame
    void Update()
    {

     //   if (CsPlayer.B_Gyohwa == false)
     //   {
            S.size = CsPlayer.Gyohwa;
      //  }
        //     Gauge_size = S.size;

        //    float R = 0.0f;

      //  if (CsPlayer.B_Gyohwa == true)
      //  {
       //     S.size -= 0.2f * Time.deltaTime;
       // }
        

    }
}
