using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour {

    public WayPoint e;
    public enum TYPE { WHITE, ICE, FIRE, EARTH, POISION, SPIRIT,HEAL, COIN , POWER};
    public TYPE type;
    public Sprite[] elements;
    public bool respawnable;
    public float lifeTime;
    private SpriteRenderer spriteRender;
    private CircleCollider2D _collider;
    private IEnumerator coroutine;
    public GameObject consumeEffect;
    // Use this for initialization
    void Start () {
        
        spriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        _collider = this.gameObject.GetComponent<CircleCollider2D>();

        //Switch art to correct element
        switch (type) {
            case TYPE.WHITE:
                spriteRender.sprite = elements[0];
                break;
            case TYPE.ICE:
                spriteRender.sprite = elements[1];
                break;
            case TYPE.FIRE:
                spriteRender.sprite = elements[2];
                break;
            case TYPE.EARTH:
                spriteRender.sprite = elements[3];
                break;
            case TYPE.POISION:
                spriteRender.sprite = elements[4];
                break;
            case TYPE.SPIRIT:
                spriteRender.sprite = elements[5];
                break;
            case TYPE.HEAL:
                spriteRender.sprite = elements[6];
                break;
            case TYPE.COIN:
                spriteRender.sprite = elements[7];
                break; 
            case TYPE.POWER:
                break;
        }

        //Destroys the power up after a certain amount of time, defaults to 1000 seconds if not changed from 0
        if (!respawnable) {  
            if (lifeTime == 0) { lifeTime = 1000; }
            coroutine = despawn(lifeTime);
            StartCoroutine(coroutine);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Attempt at stopping collision with the powerups and players
        if (this.type != TYPE.HEAL && (//collision.gameObject.tag == "Player" || 
                collision.gameObject.tag == "T1" || collision.gameObject.tag == "T2")) {
            Physics2D.IgnoreCollision(collision.collider, _collider, true);

        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (this.type == TYPE.COIN && (other.gameObject.tag == "Team1" ))
        {
           // print("Got  a Crystal");
            Instantiate(consumeEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if ((this.type == TYPE.HEAL || this.type== TYPE.COIN) && (other.gameObject.tag == "Bullet1" || 
                other.gameObject.tag == "Bullet1" || other.gameObject.tag == "powerShot" || other.gameObject.tag == "Team2") ){
            Physics2D.IgnoreCollision(other.GetComponent<BoxCollider2D>(), _collider, true);
        }
        else if (other.gameObject.tag == "powerShot" || other.gameObject.tag == "Team1") {
            if (other.gameObject.tag == "pwoershot")
            {
                Instantiate(consumeEffect, transform.position, transform.rotation);
                other.GetComponent<Ammo>().powerUp(type);
                Destroy(this.gameObject);
            }
            else {

                Instantiate(consumeEffect, transform.position, transform.rotation);
                other.GetComponent<Character>().getConsumePower(type);
                Destroy(this.gameObject);
            }
        }
        
        //
        if (this.type != TYPE.HEAL && (other.gameObject.tag == "Player" || 
                other.gameObject.tag == "Team1" || other.gameObject.tag == "Team2")) {
            Physics2D.IgnoreCollision(other.GetComponent<BoxCollider2D>(), _collider,true);
        }
        //Heal
        if (this.type == TYPE.HEAL && (other.gameObject.tag == "Team1" || other.gameObject.tag == "Team2") && type != TYPE.POWER
                && other.gameObject.GetComponent<Character>().health != other.gameObject.GetComponent<Character>().maxHealth)  {
            other.GetComponent<Character>().ApplyDamage(-50);
            spriteRender.enabled = false;
            _collider.enabled = false;
            coroutine = respawn(lifeTime);
            StartCoroutine(coroutine);
        }

    
    }

    private IEnumerator respawn(float time) {
        yield return new WaitForSeconds(time);
        spriteRender.enabled = true;
        _collider.enabled = true;
    }

    private IEnumerator despawn(float time) {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

}
