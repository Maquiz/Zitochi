using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherTrigger : MonoBehaviour {

    private Smasher s;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            s = collision.gameObject.GetComponent<Smasher>();
            s.hitTop = true;
        }
    }
}
