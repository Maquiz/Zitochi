using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeEnemey : MonoBehaviour {
    public AICharacter ai;
    public Animator anim;
    public BoxCollider2D see, close;
    public List<GameObject> enemies = new List<GameObject>();

    private void Update()
    {
        if (enemies.Count > 0)
        {
            ai.target = enemies[0];
        }
        else {
            ai.target = null;
        }
        while(enemies.Contains(null)) {
            enemies.Remove(null);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
    if (ai._TEAM == Character.TEAM.TEAM1 && collision.tag == "Team2") {
            GameObject go = collision.gameObject;
            if (!enemies.Contains(go)){
                ai.seePlayer = true;
                // ai.target = collision.gameObject;            
                enemies.Add(go);
                anim.SetBool("SeeEnemy", true);

            }
  
        }
        else if (ai._TEAM == Character.TEAM.TEAM2 && collision.tag == "Team1") {
            GameObject go = collision.gameObject;
            if (!enemies.Contains(go))
            {
                ai.seePlayer = true;
                //  ai.target = collision.gameObject;
                enemies.Add(go);
                anim.SetBool("SeeEnemy", true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if(ai._TEAM == Character.TEAM.TEAM1 && collision.tag == "Team2") {
            ai.seePlayer = false;
            enemies.Remove(collision.gameObject);
            anim.SetBool("SeeEnemy", false);

        }
        else if (ai._TEAM == Character.TEAM.TEAM2 && collision.tag == "Team1")
        {
            ai.seePlayer = false;
            enemies.Remove(collision.gameObject);
            anim.SetBool("SeeEnemy", false);
        }

    }
}
