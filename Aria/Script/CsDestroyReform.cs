using UnityEngine;
using System.Collections;

public class CsDestroyReform : MonoBehaviour {

    void Update()
    {
        StartCoroutine("Destroyer");
    }
    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
