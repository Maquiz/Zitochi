using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeEnemey : MonoBehaviour {
    public AICharacter ai;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            ai.seePlayer = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            ai.seePlayer = false;
        }
    }
}
