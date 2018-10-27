using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter :  Character {
    // Use this for initialization
    public int moveSpeed = 1;
    public GameObject target;
    public GameObject dEffect;
    public GameObject l;
    public GameObject h;

    void Start(){
        deathEffect = dEffect;
        hasDrop = true;
        loot = l;
        maxHealth = health;
        HealthBar = h;

    }
    // Update is called once per frame
    void Update () {
        //Movement, should be turning when players position changes orientaion
        if (target != null){
            transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        }
       
    }
}
