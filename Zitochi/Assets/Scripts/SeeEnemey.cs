using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeEnemey : MonoBehaviour {
    public AICharacter ai;
	// Use this for initialization
	void Start () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ai.seePlayer = true;
        }
    }
}
