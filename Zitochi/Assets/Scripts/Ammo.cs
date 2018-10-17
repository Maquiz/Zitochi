using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour{
    public int damage;
    private float size;
    public Character shooter;
    private Vector2 dir;
    private Rigidbody2D rb;
    public float speed;
    public GameObject impactEffect;
    public bool isLooter;
    public bool isParent;
   

    enum STATE {ALIVE, DEAD, PAUSED};
    STATE _STATE;

    void Start(){
            rb = this.GetComponent<Rigidbody2D>();
            //Shooting twoards the click or touch
            var mouse = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            dir = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
            this.transform.Rotate(0, 0, 90);//This needs to be calculated to aim at arrow
            this.rb.AddForce(dir.normalized * speed);
    }

    void OnTriggerEnter2D(Collider2D coll){
        Character enemy = coll.gameObject.GetComponent<Character>();
        if(enemy != null) { 
            if (enemy._TEAM == Character.TEAM.TEAM2){
                coll.gameObject.SendMessage("ApplyDamage", damage);
                Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
        if (isLooter) {
            if (coll.gameObject.tag == "powerup") {
                print("POWERUP" + shooter.name);
                //On collision tell the shooter what type of element was consummed
                //Consumable.TYPE c = coll.GetComponent<Consumable>().type;
                // powerUp(c);
                Destroy(this.gameObject);
            }
        }
        if (coll.gameObject.tag == "Ground") {
            Instantiate(impactEffect, transform.position, transform.rotation);
            print("hit ground");
            Destroy(this.gameObject);
        }
    }

    public void powerUp(Consumable.TYPE t) {
        if (t == Consumable.TYPE.FIRE) {
            print("hit Fire");
            shooter.getPower(Character.TYPE.FIRE);
        }
        else if (t == Consumable.TYPE.WHITE) {
            shooter.getPower(Character.TYPE.WHITE);
        }
        else if (t == Consumable.TYPE.ICE){
            shooter.getPower(Character.TYPE.ICE);
        }
        else if (t == Consumable.TYPE.EARTH){
            shooter.getPower(Character.TYPE.EARTH);
        }
        else if (t == Consumable.TYPE.POISION){
            shooter.getPower(Character.TYPE.POISION);
        }
        else if (t == Consumable.TYPE.SPIRIT){
            shooter.getPower(Character.TYPE.SPIRIT);
        }
    }

    void OnBecameInvisible(){
        Destroy(gameObject);
    }
}