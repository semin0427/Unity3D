using UnityEngine;
using System.Collections;

public class Button_Front_Destroy : MonoBehaviour {


	void Update () {
        StartCoroutine("Destroyer");
    }
    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
