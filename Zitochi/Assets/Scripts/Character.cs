using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    bool isPlayer;
    public int health;
    public int maxHealth;
    public float speed;
    public GameObject gun;
    private Weapon weapon;
    private Rigidbody2D body;
    public GameObject deathEffect;
    public GameObject hitEffect;
    public GameObject HealthBar;

    public enum TYPE { WHITE , ICE, FIRE, EARTH, POISION, SPIRIT};
    public TYPE type1, type2;
    public Sprite[] elements;
    public Image element1, element2;

    public bool hasDrop;
    public GameObject loot;

    public enum TEAM  {TEAM1, TEAM2};
    public TEAM _TEAM;
    enum STATE {ALIVE, DEAD, PAUSED };
    STATE _STATE;
    

    public Character() {}
    
    //Constructor
    Character(string N, int H, float S,TEAM T) {
        name = N;
        health = H;
        speed = S;
        _TEAM = T;
    }

	// Use this for initialization
	void Start (){
        _STATE = STATE.ALIVE;
        weapon = gun.GetComponent<Weapon>();
        body = gameObject.GetComponent<Rigidbody2D>();
        type1 = TYPE.WHITE;
        type2 = TYPE.WHITE;
        maxHealth = health;
	}
	
	// Update is called once per frame
	void Update (){
        
        //Attack 1
        if (Input.GetButtonDown("Fire1")){
            if (type1 == TYPE.EARTH) {
                weapon.switchAmmo(2);
            }
            else{
                weapon.switchAmmo(1);
            }
            weapon.Fire(this);
        }

        //Attack 2
        if (Input.GetButtonDown("Fire2")){
            if (type2 == TYPE.WHITE) {
                weapon.switchAmmo(0);   
            }
            else if (type1 == TYPE.EARTH && type2 == TYPE.EARTH) {
                weapon.switchAmmo(3);
            }
            weapon.Fire(this);

        }

        //UI for different Elements, can be written better
        //For loop looping through type?

        if (type1 == TYPE.WHITE){
            element1.sprite = elements[0];
        }
        else if (type1 == TYPE.ICE){
           element1.sprite = elements[1];
        }
        else if (type1 == TYPE.FIRE){
            element1.sprite = elements[2];
        }
        else if (type1 == TYPE.EARTH){
            element1.sprite = elements[3];
            type1 = TYPE.EARTH;
        }
        else if (type1 == TYPE.POISION){
            element1.sprite = elements[4];
        }
        else if (type1 == TYPE.SPIRIT)
        {
            element1.sprite = elements[5];
        }

        if (type2 == TYPE.WHITE){
            element2.sprite = elements[0];
        }
        else if (type2 == TYPE.ICE){
            element2.sprite = elements[1];
        }
        else if (type2 == TYPE.FIRE){
            element2.sprite = elements[2];
        }
        else if (type2 == TYPE.EARTH){
            element2.sprite = elements[3];
        }
        else if (type2 == TYPE.POISION){
            element2.sprite = elements[4];
        }
        else if (type2 == TYPE.SPIRIT)
        {
            element2.sprite = elements[5];
        }
    }

    void OnCollisionEnter2D(Collision2D coll){

        //This is flagging null object reference
        if (coll.gameObject.GetComponent<AICharacter>() != null){
            var team = coll.gameObject.GetComponent<AICharacter>()._TEAM;
            if (team != null){
                if (team == Character.TEAM.TEAM2) {
                    takeHit(25, 6f);
                }
            }
        }
    }


    public void kill(){
        _STATE = STATE.DEAD;
        Instantiate(deathEffect, transform.position, transform.rotation);
        DestroyObject(this.gameObject);
        //Change Animation
        if (hasDrop) {
            Instantiate(loot, transform.position, transform.rotation);
        }
    }

    public void getPower(TYPE t){
        //Need to switch ammo type here or make a reference to which kind of ammo it is
        if (type1 == TYPE.WHITE)
        {
            if (t == TYPE.FIRE){
                type1 = TYPE.FIRE;
            }
            else if (t == TYPE.EARTH){
                type1 = TYPE.EARTH;
            }
            else if (t == TYPE.ICE){
                type1 = TYPE.ICE;
            }
            else if (t == TYPE.POISION){
                type1 = TYPE.POISION;
            }
            else if (t == TYPE.SPIRIT){
                type1 = TYPE.SPIRIT;
            }
        }
        else {
            if (t == TYPE.FIRE){
                type2 = TYPE.FIRE;
            }
            else if (t == TYPE.EARTH){
                type2 = TYPE.EARTH;
            }
            else if (t == TYPE.ICE){
                type2 = TYPE.ICE;
            }
            else if (t == TYPE.POISION){
                type2 = TYPE.POISION;
            }
            else if (t == TYPE.SPIRIT){
                type2 = TYPE.SPIRIT;
            }
        }
    }

    public void takeHit(int d,float speed){
        ApplyDamage(d);
        //Opposite of collision
        Instantiate(hitEffect, transform.position, transform.rotation);
        //body.velocity = new Vector2(-10, -10);
    }

    public void ApplyDamage(int d){
        this.health -= d;

        if (HealthBar != null) {
             Vector3 h = HealthBar.transform.localScale ;
            h.x  = (float)((float)health / (float)maxHealth);
            HealthBar.transform.localScale = h;
        }
        if (health <= 0) {
            kill();
        }
    }

    public void ApplyEffect(Effect e){
        e.doEffect();
    }
}
