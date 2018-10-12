using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int health;
    public float speed;
    public GameObject gun;
    private Weapon weapon;
    private Rigidbody2D body;
    public GameObject deathEffect;
    public GameObject hitEffect;
    bool isPlayer;

    public enum TEAM  {TEAM1, TEAM2};
    public TEAM _TEAM;
    enum STATE {ALIVE, DEAD, PAUSED };
    STATE _STATE;
    

    public Character() {}

    Character(string N, int H, float S,TEAM T) {
        name = N;
        health = H;
        speed = S;
        _TEAM = T;
    }

	// Use this for initialization
	void Start () {
        _STATE = STATE.ALIVE;
        weapon = gun.GetComponent<Weapon>();
        body = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            weapon.Fire();
        }
    }

    void OnCollisionEnter2D(Collision2D coll){

        //This is flagging null object reference
/*        var team = coll.gameObject.GetComponent<Character>()._TEAM;
        if (team != null)
        {
            if (team == Character.TEAM.TEAM2)
            {
                takeHit(25, 6f);
            }
        }*/
    }

    public void kill(){
        _STATE = STATE.DEAD;
        Instantiate(deathEffect, transform.position, transform.rotation);
        DestroyObject(this.gameObject);
        //Change Animation
    }

    public void takeHit(int d,float speed) {
        ApplyDamage(d);
        //Opposite of collision
        Instantiate(hitEffect, transform.position, transform.rotation);
        body.velocity = new Vector2(-10, -10);
    }

    public void ApplyDamage(int d) {
        this.health -= d;
        print("Damage Applied");
        if (health <= 0) {
            kill();
        }
    }

    public void ApplyEffect(Effect e){
        e.doEffect();
    }
}
