using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public bool left;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        if (left == true) {
            //move left
            this.rb.AddForce(new Vector2(-10, 0) * speed);
        }
        else{
            //move right
            this.rb.AddForce(new Vector2(10, 0) * speed);
        }
       
    }
}
