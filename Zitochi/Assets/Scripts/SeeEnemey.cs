using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeEnemey : MonoBehaviour {
    public AICharacter ai;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player")
        {
            ai.seePlayer = true;
            ai.target = collision.gameObject;
        }
        else if (ai._TEAM == Character.TEAM.TEAM1 && collision.tag == "Team2") {
            ai.seePlayer = true;
            ai.target = collision.gameObject;

        }
        else if (ai._TEAM == Character.TEAM.TEAM2 && collision.tag == "Team1")
        {
            ai.seePlayer = true;
            ai.target = collision.gameObject;

        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player" ) {
            ai.seePlayer = false;
        }
    }
}
