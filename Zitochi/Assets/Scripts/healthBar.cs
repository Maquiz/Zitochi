using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour {

    public GameObject chars;
    public AICharacter character;
    Vector3 localScale;

    // Use this for initialization
    void Start() {
       // character = chars.GetComponent<AICharacter>(); 
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update() {
        // float ratio = character.health / character.maxHealth;
        // print(ratio);
        //  localScale.x = ratio;
        localScale.x = character.health;
        transform.localScale = localScale;
    }
}
