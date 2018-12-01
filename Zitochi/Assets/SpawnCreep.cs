using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCreep : MonoBehaviour {

    public GameObject creep;
    private IEnumerator coroutine;
	// Use this for initialization
	void Start () {
        coroutine = Spawn(15f);
        StartCoroutine(coroutine);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private IEnumerator Spawn(float time)
    {
        while (true)
        {
            Instantiate(creep, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Instantiate(creep, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
       

    }
}
