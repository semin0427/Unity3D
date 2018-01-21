using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public RectTransform healthBarP;
    public RectTransform boatBar;

    public Text HPtxt;
    public Text Repairtxt;

    GameStatus GS;

    float cachedY;
    float maxValue;
    float minValue;

    float EcachedY;
    float EmaxValue;
    float EminValue;

    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameRoot").GetComponent<GameStatus>();
        cachedY = healthBarP.transform.position.y;
        maxValue = healthBarP.transform.position.x;
        minValue = healthBarP.transform.position.x - healthBarP.rect.width;

        //EcachedY = boatBar.transform.position.y;
        //EmaxValue = boatBar.transform.position.x;
        //EminValue = boatBar.transform.position.x - boatBar.rect.width;
    }
	
	// Update is called once per frame
	void Update () {
        HPtxt.text = (GS.satiety * 100).ToString("000");
        HealthP();
	}

    public void HealthP()
    {
        //float cal_health = MapValues((float)GS.satiety * 100, 0, (float)GS.satiety * 100, minValue, maxValue);
        //healthBarP.position = new Vector3(cal_health, cachedY);
        healthBarP.position = new Vector3(-200 + (float)(1.54f * (GS.satiety * 100)), healthBarP.position.y, healthBarP.position.z);
    }

    public void HealthE()
    {
        float cal_health = MapValues((float)GS.repairment * 100, 0, (float)GS.repairment * 100, EminValue, EmaxValue);
        boatBar.position = new Vector3(cal_health, EcachedY);
    }

    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
