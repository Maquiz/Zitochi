using UnityEngine;

public class BreakableGround : MonoBehaviour {

    public GameObject destructionEffect;

    public void destroyGround() {
        Instantiate(destructionEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
