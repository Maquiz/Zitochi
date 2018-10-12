using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter :  Character {
    // Use this for initialization
    public int moveSpeed = 1;
    public GameObject target;
    public GameObject dEffect;

    void Start(){
        deathEffect = dEffect;
    }
    // Update is called once per frame
    void Update () {
        transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
    }
}
