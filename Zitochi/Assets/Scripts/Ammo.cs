using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour{
    public int damage;
    public float lifeTime;
    private float size;
    public Character shooter;
    private Vector2 dir;
    private Rigidbody2D rb;
    public float speed;
    public GameObject impactEffect;
    public bool isLooter;
    public bool isParent;
    public bool overHead;
    public int team;
    public bool aiAim;
    public Transform aimPos;
    private IEnumerator coroutine;

    enum STATE {ALIVE, DEAD, PAUSED};
    STATE _STATE;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        //Shooting twoards the click or touch
        if (!aiAim) {
            var mouse = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            dir = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.Rotate(0, 0, angle);//This needs to be calculated to aim at arrow
            this.rb.AddForce(dir.normalized * speed);
        }
        else {

            dir = new Vector2(aimPos.position.x - shooter.gameObject.transform.position.x, aimPos.position.y - shooter.gameObject.transform.position.y);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            this.transform.Rotate(0, 0, angle);//This needs to be calculated to aim at arrow
            this.rb.AddForce(dir.normalized * speed);
        }

        coroutine =  lifeSpan(lifeTime);
        StartCoroutine(coroutine);

    }

    void OnTriggerEnter2D(Collider2D coll){
        Character enemy = coll.gameObject.GetComponent<Character>();
        if(enemy != null) {
            if (team == 1) {
                if (enemy._TEAM == Character.TEAM.TEAM2) {
                    coll.gameObject.SendMessage("ApplyDamage", damage);
                    Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(this.gameObject);
                }
            }
            if (team == 2) {
                if (enemy._TEAM == Character.TEAM.TEAM1) {
                    coll.gameObject.SendMessage("ApplyDamage", damage);
                    Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(this.gameObject);
                }
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
        if (coll.gameObject.layer == 15 && this.gameObject.tag != "powerShot") {
            BreakableGround bg = coll.gameObject.GetComponent<BreakableGround>();
            bg.destroyGround();
           Destroy(this.gameObject);

        }
        if (coll.gameObject.tag == "Ground") {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void powerUp(Consumable.TYPE t) {
        switch (t) {
            case Consumable.TYPE.FIRE:
                print("hit Fire");
                shooter.getPower(Character.TYPE.FIRE);
                break;
            case Consumable.TYPE.WHITE:
                shooter.getPower(Character.TYPE.WHITE);
                break;
            case Consumable.TYPE.ICE:
                shooter.getPower(Character.TYPE.ICE);
                break;
            case Consumable.TYPE.EARTH:
                shooter.getPower(Character.TYPE.EARTH);
                break;
            case Consumable.TYPE.POISION:
                shooter.getPower(Character.TYPE.POISION);
                break;
            case Consumable.TYPE.SPIRIT:
                shooter.getPower(Character.TYPE.SPIRIT);
                break;
        }
    }



    private IEnumerator lifeSpan(float time)
    {
  
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
       
    }
    //Chage to Lifespan instead of destroying once leaving screen
    void OnBecameInvisible(){
       // Destroy(gameObject);
    }
}