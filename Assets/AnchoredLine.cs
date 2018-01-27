using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoredLine : MonoBehaviour {
    public GameObject anchor1;
    public GameObject anchor2;

    public GameObject[] links;
	// Use this for initialization
	void Start () {
        //links[0].GetComponent<HingeJoint2D>().connectedBody = anchor1.GetComponent<Rigidbody2D>();
        //links[1].GetComponent<HingeJoint2D>().connectedBody = links[0].GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
