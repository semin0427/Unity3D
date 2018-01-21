using UnityEngine;
using System.Collections;

public class MapCreator : MonoBehaviour {

    public GameObject Map1;
    public GameObject Map2;
    public GameObject Map3;
    public Transform T_Map1;
    public Transform T_Map2;
    public Transform T_Map3;
    CsButton Button;

    Transform M_P;
    // Use this for initialization
    void Start()
    {
        Button = GameObject.Find("MainCanvas").GetComponent<CsButton>();
        M_P = GetComponent<Transform>();
        switch (Button.M_1)
        {
            case 1:
                Instantiate(Map1, T_Map1.position, T_Map1.rotation);                
                break;
            case 2:
                Instantiate(Map2, T_Map2.position, T_Map2.rotation);
                break;
            case 3:
                Instantiate(Map3, T_Map3.position, T_Map3.rotation);
                break;
        }
    }
}
