using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListManager : MonoBehaviour {

    public List<GameObject> OffenseAtkUnit;
    public List<GameObject> DefenceDefUnit;
    public List<GameObject> OffenceDefUnit;
    public List<GameObject> DefenceAtkUnit;
    public List<GameObject> TreeList;
    // Use this for initialization
    void Start() {
        
    }
	
	// Update is called once per frame
	void Update () {
        OffenseAtkUnit = new List<GameObject>(GameObject.FindGameObjectsWithTag("Offense"));
        DefenceDefUnit = new List<GameObject>(GameObject.FindGameObjectsWithTag("Defence"));
        TreeList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tree"));
        OffenceDefUnit = new List<GameObject>(GameObject.FindGameObjectsWithTag("OffenseDef"));
        DefenceAtkUnit = new List<GameObject>(GameObject.FindGameObjectsWithTag("DefenceAtk"));
    }
}
