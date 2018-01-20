using UnityEngine;
using System.Collections;

public class CsPreSet : MonoBehaviour
{
    public GameObject AtkRange;

    // Use this for initialization
    void Start()
    {
        AtkRange = GameObject.Find("AtkRange");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setRange(float _range)
    {
        AtkRange.transform.localScale = (new Vector3(_range, 0, _range));
    }
}