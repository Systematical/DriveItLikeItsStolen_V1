using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceScript : MonoBehaviour {
    public int policespeed = 20;
    public AudioClip crashAudio;
	// Use this for initialization
	void Start () {
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * policespeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.SendMessage("ApplyDamage", 10);
        this.GetComponent<AudioSource>().PlayOneShot(crashAudio);
        
        Destroy(this.gameObject);
    }
}
