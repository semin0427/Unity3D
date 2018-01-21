using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {

    public TextAsset levelData = null;
    LevelControl LC = null;
    float vanishingTime;

	// Use this for initialization
	void Start () {
        vanishingTime = 0;

        LC = new LevelControl();
        LC.initialize();
        LC.loadLevelData(levelData);
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < 1.5f)
            vanishingTime += Time.deltaTime;
        else
            vanishingTime = 0;

        LC.selectLevel();


        if (gameObject.tag == "Apple")
            apple(LC.getAppleVanishTime());
        if (gameObject.tag == "Iron")
            iron(LC.getIronVanishTime());
        if (gameObject.tag == "Plant")
            plant(LC.getPlantVanishTime());
        if (gameObject.tag == "Chestnut")
            chestnut(LC.getNutVanishTime());
    }

    void apple(float _time)
    {
        if (vanishingTime > _time)
            Destroy(gameObject);
    }

    void plant(float _time)
    {
        if (vanishingTime > _time)
            Destroy(gameObject);
    }

    void iron(float _time)
    {
        if (vanishingTime > _time)
            Destroy(gameObject);
    }

    void chestnut(float _time)
    {
        if (vanishingTime > _time)
            Destroy(gameObject);
    }

}
