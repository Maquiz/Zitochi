using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour {

    public WayPoint e;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SendMessage("ApplyEffect", e);
      //  DestroyObject(this.gameObject);
    }

}
