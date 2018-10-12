using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject a;



    public void Fire() {
        Instantiate(a, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
       
    }
}
