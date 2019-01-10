using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smasher : MonoBehaviour {
    public float waitTime;

    private Rigidbody2D rb;
    public bool hitTop, hitGround;
    private IEnumerator coroutine;
    public GameObject smashEffect;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody2D>();
        hitTop = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (hitTop == true )
        {

            rb.gravityScale = 1f;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" )
        {
            print("hit the ground");
            rb.gravityScale = -.25f;
            hitTop = false;
            Instantiate(smashEffect, collision.transform.position, collision.transform.rotation);
        }
  
    }

    private IEnumerator lifeSpan(float time)
    {

        yield return new WaitForSeconds(time);
        hitTop = !hitTop;
    }
}
