using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {
    public bool isPlayer;
    public bool isImmune;
    public int health;
    public int maxHealth;
    public float speed;

    public GameObject movement;
    public CircleCollider2D moveCollide;
    public GameObject gun;
    public Weapon weapon;
    private Rigidbody2D body;

    public GameObject target;
    public bool seePlayer;

    public GameObject deathEffect;
    public GameObject hitEffect;

    public GameObject HealthBar;

    public enum TYPE { WHITE , ICE, FIRE, EARTH, POISION, SPIRIT};
    public TYPE type1, type2;
    public Sprite[] elements;
    public Image element1, element2;
    public Image cd1, cd2;
    private float cdt1, cdt2;

    public bool hasDrop;
    public GameObject loot;

    private IEnumerator coroutine;

    public enum TEAM  {TEAM1, TEAM2};
    public TEAM _TEAM;
    enum STATE {ALIVE, DEAD, PAUSED };
    private bool canShoot2,canShoot1;
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
	void Start () {
        _STATE = STATE.ALIVE;
        weapon = gun.GetComponent<Weapon>();
        body = gameObject.GetComponent<Rigidbody2D>();
        type1 = TYPE.WHITE;
        type2 = TYPE.WHITE;
        
        maxHealth = health;
        if (isPlayer) {
            canShoot1 = true;
            canShoot2 = true;
            movement.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //CoolDowns
        
        //Attack 1
        if (Input.GetButtonDown("Fire1") && canShoot1) {
            if (type1 == TYPE.EARTH) {
                weapon.switchAmmo(2);
                coroutine = cooldownFire1(0.25f);
                StartCoroutine(coroutine);
            } else if (type1 == TYPE.SPIRIT) {
                weapon.switchAmmo(5);
                coroutine = cooldownFire1(0.25f);
                StartCoroutine(coroutine);
            }
            else {
                weapon.switchAmmo(1);
                coroutine = cooldownFire1(0.25f);
                StartCoroutine(coroutine);
            }
            
        }

        //Attack 2
        if (Input.GetButtonDown("Fire2") && canShoot2) {
            if (type2 == TYPE.WHITE) {
                weapon.switchAmmo(0);
                coroutine = cooldownFire(0.25f);
                StartCoroutine(coroutine);
            }
            else if (type1 == TYPE.EARTH && type2 == TYPE.EARTH) {
                weapon.switchAmmo(3);
                coroutine = cooldownFire(1.5f);
                StartCoroutine(coroutine);
            }
            else if ((type1 == TYPE.EARTH && type2 == TYPE.SPIRIT) ||(type2 == TYPE.EARTH && type1 == TYPE.SPIRIT)) {
                weapon.switchAmmo(4);
                coroutine = cooldownFire(2.0f);
                StartCoroutine(coroutine);

            }
            else if (type1 == TYPE.SPIRIT && type2 == TYPE.SPIRIT)
            {
                weapon.switchAmmo(6);
                coroutine = cooldownFire(2.0f);
                StartCoroutine(coroutine);

            }
        }
        //float speed = (transform.position - this.mLastPosition).magnitude / elapsedTime;
       // print("speed " +body.velocity);
        
        if (isPlayer) {
            checkType();
            shiftPower();
            dropPower();
        }

        //Cooldown Buttons
        if (canShoot1 == true) {
            cd1.fillAmount = 0;
        }
        else if (canShoot1 != true) {
            cd1.fillAmount += 1 / cdt1 * Time.deltaTime;
        }
        if (canShoot2 == true) {
            cd2.fillAmount = 0;
        }
        else if (canShoot2 != true) {
            cd2.fillAmount +=  1/ cdt2 * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        //This is flagging null object reference
        if (coll.gameObject.GetComponent<AICharacter>() != null) {
            var team = coll.gameObject.GetComponent<AICharacter>()._TEAM;
            if (team != null && !isImmune) {
                if (team == Character.TEAM.TEAM2 && _TEAM != TEAM.TEAM2) {
                 //   takeHit(25, 6f);
                }
            }
        }
    }

    public void kill() {
        _STATE = STATE.DEAD;
        Instantiate(deathEffect, transform.position, transform.rotation);
        if (isPlayer) {
            //This will be where we deal with players deaths
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
        else {

            DestroyObject(this.gameObject);
        }
        
        //Change Animation
        if (hasDrop && loot !=  null) {
            Instantiate(loot, transform.position, transform.rotation);
        }
    }

    public void dropPower() {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (type2 == TYPE.WHITE && type1 != TYPE.WHITE) {
                type1 = TYPE.WHITE;
                //Play particle effect for lose of power
            }
            else if (type2 != TYPE.WHITE) {
                type2 = TYPE.WHITE;
                //Play particle effect for lose of power
            }
        }
    }

    public void shiftPower() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            //print("hit shift");
            movement.SetActive(true);
            moveCollide.enabled = true;
            isImmune = true;
        }
        else {
            movement.SetActive(false);
            moveCollide.enabled = false;
            isImmune = false;
        }
    }
    public void getConsumePower(Consumable.TYPE t)
    {
        if (type1 == TYPE.WHITE)
        {
            switch (t)
            {
                case Consumable.TYPE.FIRE:
                    type1 = TYPE.FIRE;
                    break;
                case Consumable.TYPE.EARTH:
                    type1 = TYPE.EARTH;
                    break;
                case Consumable.TYPE.ICE:
                    type1 = TYPE.ICE;
                    break;
                case Consumable.TYPE.POISION:
                    type1 = TYPE.POISION;
                    break;
                case Consumable.TYPE.SPIRIT:
                    type1 = TYPE.SPIRIT;
                    break;
            }

        }
        else
        {
            switch (t)
            {
                case Consumable.TYPE.FIRE:
                    type2 = TYPE.FIRE;
                    break;
                case Consumable.TYPE.EARTH:
                    type2 = TYPE.EARTH;
                    break;
                case Consumable.TYPE.ICE:
                    type2 = TYPE.ICE;
                    break;
                case Consumable.TYPE.POISION:
                    type2 = TYPE.POISION;
                    break;
                case Consumable.TYPE.SPIRIT:
                    type2 = TYPE.SPIRIT;
                    break;
            }
        }
    }

        public void getPower(TYPE t) {
        if (type1 == TYPE.WHITE) {
            switch (t) {
                case TYPE.FIRE:
                    type1 = TYPE.FIRE;
                    break;
                case TYPE.EARTH:
                    type1 = TYPE.EARTH;
                    break;
                case TYPE.ICE:
                    type1 = TYPE.ICE;
                    break;
                case TYPE.POISION:
                    type1 = TYPE.POISION;
                    break;
                case TYPE.SPIRIT:
                    type1 = TYPE.SPIRIT;
                    break;
            }
 
        }
        else {
            switch (t)
            {
                case TYPE.FIRE:
                    type2 = TYPE.FIRE;
                    break;
                case TYPE.EARTH:
                    type2 = TYPE.EARTH;
                    break;
                case TYPE.ICE:
                    type2 = TYPE.ICE;
                    break;
                case TYPE.POISION:
                    type2 = TYPE.POISION;
                    break;
                case TYPE.SPIRIT:
                    type2 = TYPE.SPIRIT;
                    break;
            }
        }
    }

    public void checkType() {
        switch (type1) {
            case TYPE.WHITE:
                element1.sprite = elements[0];
                break;
            case TYPE.ICE:
                element1.sprite = elements[1];
                break;
            case TYPE.FIRE:
                element1.sprite = elements[2];
                break;
            case TYPE.EARTH:
                element1.sprite = elements[3];
                type1 = TYPE.EARTH;
                break;
            case TYPE.POISION:
                element1.sprite = elements[4];
                break;
            case TYPE.SPIRIT:
                element1.sprite = elements[5];
                break;
        }

        switch (type2) {
            case TYPE.WHITE:
                element2.sprite = elements[0];
                break;
            case TYPE.ICE:
                element2.sprite = elements[1];
                break;
            case TYPE.FIRE:
                element2.sprite = elements[2];
                break;
            case TYPE.EARTH:
                element2.sprite = elements[3];
                type2 = TYPE.EARTH;
                break;
            case TYPE.POISION:
                element2.sprite = elements[4];
                break;
            case TYPE.SPIRIT:
                element2.sprite = elements[5];
                break;
        }
    }

    public void takeHit(int d,float speed){
        ApplyDamage(d);
        //Opposite of collision
        Instantiate(hitEffect, transform.position, transform.rotation);
       // body.AddForce(new Vector2(1000f, 1000f));
        //body.velocity = new Vector2(-10, -10);
    }

    public void ApplyDamage(int d){
        if (d <= 0)
        {
            if (-d + this.health > this.maxHealth)
            {

                this.health += this.maxHealth - this.health;
            }
        }
        else {
            this.health -= d;
        }

        
     
       if (HealthBar != null) {
            //Issues with health bar scaling above 100
        
            Vector3 h = HealthBar.transform.localScale ;
            h.x  = (float)((float)health / (float)maxHealth);
           // print(this.gameObject.name +" health = " + h.x);
            HealthBar.transform.localScale = h;
        }
        if (health <= 0) {


            kill();
        }
    }

    public void ApplyEffect(Effect e){
        e.doEffect();
    }

    private IEnumerator cooldownFire(float time) {
        canShoot2 = false;
        cdt2 = time;
        //yield return new WaitForSeconds(time);
        weapon.Fire(this);
        //print("Coroutine ended: " + Time.time + " seconds");
        yield return new WaitForSeconds(time);
        canShoot2 = true;
    }

    private IEnumerator cooldownFire1(float time) {
        canShoot1 = false;
        cdt1 = time;
        //print(time);
        //yield return new WaitForSeconds(time);
        weapon.Fire(this);
        //print("Coroutine ended: " + Time.time + " seconds");
        yield return new WaitForSeconds(time);
        canShoot1 = true;
    }
}
