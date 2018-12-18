using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundCleanup : MonoBehaviour {

    public Transform birdspawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Vector3 a = birdspawn.position;
        a.y += Random.Range(-2.0f, 2.0f);
        collision.gameObject.transform.position = a;
    }
}
