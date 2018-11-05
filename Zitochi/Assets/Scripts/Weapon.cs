using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public GameObject[] ammo;
    public GameObject currentAmmo;
    public Transform overHeadSpot;

    private void Start(){
        currentAmmo = ammo[1];
    }

    public void switchAmmo(int ammoCode) {
        currentAmmo = ammo[ammoCode];
    }

    public void Fire(Character shooter) {
        Ammo am = currentAmmo.GetComponent<Ammo>();
        if (am.overHead)
        {
            GameObject a = Instantiate(currentAmmo, overHeadSpot.position, Quaternion.identity);
            a.GetComponent<Ammo>().shooter = shooter;
        }
        else {
            GameObject a = Instantiate(currentAmmo, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            a.GetComponent<Ammo>().shooter = shooter;
        }

    }
}
