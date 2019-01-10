using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter :  Character {

    public int moveSpeed = 1;
    public bool isShooter;
   
    private IEnumerator coroutine;
    private bool canShoot;
    public bool isTower;
    private SpriteRenderer charSprite;
    public Animator _aiController;
    public bool _isCreep;
    public float _cooldown;

    public List<GameObject> goals;
    private GameObject goal;

    void Start(){
        hasDrop = true;
        maxHealth = health;
        seePlayer = false;
        //Needs to become more generic
        charSprite = this.GetComponent<SpriteRenderer>();
        canShoot = true;
        if (_isCreep)
        {
            goals.Add( GameObject.FindGameObjectWithTag("goal1"));
            goals.Add(GameObject.FindGameObjectWithTag("goal2"));
            if (_TEAM == TEAM.TEAM1)
            {
                goal = goals[1];
            }
            else
            {
                goal = goals[0];
                charSprite.flipX = true;

            }
        }
    
       // weapon = gun.GetComponent<Weapon>();
        if (!isTower) {
            hasDrop = true;
        }
    }


    // Update is called once per frame
    void Update () {

        if ( _isCreep && (_aiController.GetBool("SeeEnemy") == false || _aiController.GetBool("CloseToEnemy") == false)) {
            //Move Towards target
            
            transform.position += (goal.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;

        }
        if (target != null) {
            if (health < maxHealth && target != null && seePlayer == false)
            {
                seePlayer = true;

            }
            if (seePlayer)
            {


                if (isShooter && canShoot)
                {
       
                    //Need to make cooldown more dynamic
                    coroutine = cooldown(_cooldown);
                    StartCoroutine(coroutine);
                    print(this.name +  " sees " + target.gameObject.name);
                }

                if (target != null)
                {
                    //Move Towards target
                    transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position += new Vector3(-1f, 0f).normalized * moveSpeed * Time.deltaTime;
                }

                
                if (target.transform.position.x <= gameObject.transform.position.x && !isTower )
                {
                    print("flipped character true");
                    charSprite.flipX = true;
                    // transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else if (target.transform.position.x >= gameObject.transform.position.x && !isTower)
                {
                    charSprite.flipX = false;
                    // transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
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