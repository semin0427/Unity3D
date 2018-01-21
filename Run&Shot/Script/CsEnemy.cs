using UnityEngine;
using System.Collections;

public class CsEnemy : MonoBehaviour {

    public int HP;

    public GameObject firePoint;
    public GameObject arrow;

    float attackTime;

    float dieTime;

    Animator animator;
    private GameObject player;

	// Use this for initialization
	void Start () {
        animator = transform.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {

        if (player.transform.position.x > transform.position.x + 20)
            Destroy(gameObject);

        if (HP <= 0)
            Die();
        else
            Idle();
    }

    void Idle()
    {
        animator.SetBool("Die", false);

        attackTime += Time.deltaTime;

        if (transform.tag == "Archer")
            if (attackTime >= 1.333f)
                createArrow(transform.position);
    }

    void Die()
    {
        BoxCollider tmp = GetComponent<BoxCollider>();
        Destroy(tmp);

        if(dieTime == 0)
            UIManger.score += 50; 

        dieTime += Time.deltaTime;
        animator.SetBool("Die", true);

        if (dieTime > 1.633f)
        {
            dieTime = 0;
            Destroy(this.gameObject);
        }
    }

    void createArrow(Vector3 _pos)
    {
        GameObject child = Instantiate(arrow ,this.transform.position + new Vector3(0.5f, 1.3f, 0), Quaternion.identity) as GameObject;
        child.transform.parent = firePoint.transform;
        child.transform.Rotate(new Vector3(0, 0, 90));
        Debug.Log(attackTime);
        attackTime = 0;
    }
}
