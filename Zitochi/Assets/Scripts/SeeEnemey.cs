using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeEnemey : MonoBehaviour {
    public AICharacter ai;
    public Animator anim;
    public BoxCollider2D see, close;

    
    public void OnTriggerEnter2D(Collider2D collision) {
        
        //anim.GetCurrentAnimatorStateInfo.;
        if (collision.tag == "Player") {
          //  ai.seePlayer = true;
         //   ai.target = collision.gameObject;
          //  anim.SetBool("SeeEnemy", true);
        }
        else if (ai._TEAM == Character.TEAM.TEAM1 && collision.tag == "Team2") {
            ai.seePlayer = true;
            ai.target = collision.gameObject;
            anim.SetBool("SeeEnemy", true);
        }
        else if (ai._TEAM == Character.TEAM.TEAM2 && collision.tag == "Team1") {
            ai.seePlayer = true;
            ai.target = collision.gameObject;
            anim.SetBool("SeeEnemy", true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player" ) {
            ai.seePlayer = false;
        }
    }
}
