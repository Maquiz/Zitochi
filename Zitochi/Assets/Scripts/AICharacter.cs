using System.Collections;
using UnityEngine;

public class AICharacter :  Character {
    // Use this for initialization
    public int moveSpeed = 1;
    public GameObject target;
    public bool isShooter;
    public bool seePlayer;
    private IEnumerator coroutine;
    private bool canShoot;
    public bool isTower;
    private SpriteRenderer charSprite;

    void Start(){
        hasDrop = true;
        maxHealth = health;
        seePlayer = false;
        //Needs to become more generic
        target = GameObject.Find("Player");
        charSprite = this.GetComponent<SpriteRenderer>();
        canShoot = true;
        if (!isTower) {
            hasDrop = true;
        }
    }

    // Update is called once per frame
    void Update () {
        //Movement, should be turning when players position changes orientaion
        if (health < maxHealth && target != null) {
            seePlayer = true;
        }
        if (seePlayer) {
            if (isShooter && canShoot) {
                //Calling to many times. Need a cooldown or way to stagger shots.
                //The bullets were hitting themselves.
                coroutine = cooldown(2.0f);
                StartCoroutine(coroutine);
            }

            if (target != null) {
                transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
            }

            //Flipping the character to chase is causing the healthbar to flip
            if (target.transform.position.x >= gameObject.transform.position.x && !isTower) {
                charSprite.flipX = true ;
               // transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if(!isTower){
                charSprite.flipX = false;
               // transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private IEnumerator cooldown(float time) {
        canShoot = false;
        weapon.AIFire(this, target.transform);
        //print("Coroutine ended: " + Time.time + " seconds");
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
}