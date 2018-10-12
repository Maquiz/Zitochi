using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour{
    public int damage;
    private float size;
    private Vector2 dir;
    private Rigidbody2D rb;
    public float speed;
    public GameObject impactEffect;

    enum STATE { ALIVE, DEAD, PAUSED };
    STATE _STATE;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        var mouse = Input.mousePosition;

        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        dir = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
       // print(dir);
        this.transform.Rotate(0,0,90); 
        this.rb.AddForce(dir * speed);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Character enemy = coll.gameObject.GetComponent<Character>();
        //This is flagging null object reference
        if(enemy != null) { 
            if (enemy._TEAM == Character.TEAM.TEAM2)
            {
                coll.gameObject.SendMessage("ApplyDamage", damage);

                Instantiate(impactEffect, transform.position, transform.rotation);

                Destroy(this.gameObject);
            }
        }
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}