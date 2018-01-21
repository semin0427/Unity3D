using UnityEngine;
using System.Collections;

public class CreaterPlyer : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public Transform P_T;
    CsButton Button;
    

    // Use this for initialization
    void Start() {
        Button = GameObject.Find("MainCanvas").GetComponent<CsButton>();
       
        switch (Button.P_1)
        {
            case 1:
            Instantiate(Player1, P_T.position, P_T.rotation);
                break;
            case 2:
            Instantiate(Player2, P_T.position, P_T.rotation);
                break;
            case 3:
            Instantiate(Player3, P_T.position, P_T.rotation);
                break;
        }
        

    }
	 
    
}
