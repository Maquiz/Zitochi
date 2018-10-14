using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour {

    public WayPoint e;
    public enum TYPE { WHITE, ICE, FIRE, EARTH, POISION, SPIRIT };
    public TYPE type;
    public Sprite[] elements;
    public SpriteRenderer spriteRender;
    // Use this for initialization
    void Start () {
        spriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        if (type == TYPE.WHITE)
        {
            spriteRender.sprite = elements[0];
        }
        else if (type == TYPE.ICE)
        {
            spriteRender.sprite = elements[1];
        }
        else if (type == TYPE.FIRE)
        {
            spriteRender.sprite = elements[2];
        }
        else if (type == TYPE.EARTH)
        {
            spriteRender.sprite = elements[3];
        }
        else if (type == TYPE.POISION)
        {
            spriteRender.sprite = elements[4];
        }
        else if (type == TYPE.SPIRIT)
        {
            spriteRender.sprite = elements[5];
        }
    }
	
	// Update is called once per frame
	void Update () {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //  other.gameObject.SendMessage("ApplyEffect", e);
        if (other.gameObject.tag == "powerShot") {
            other.GetComponent<Ammo>().powerUp(type);
            Destroy(this.gameObject);
        }
     
    }

}
