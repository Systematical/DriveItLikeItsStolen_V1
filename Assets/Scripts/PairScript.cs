﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairScript : MonoBehaviour {
    public GameObject[] points;
    public SocketScript[] objectConnections;
    public SocketScript emptySocket;
    public int ID;
	// Use this for initialization
	void Start () {
        this.GetComponentInChildren<LineRenderer>().SetPosition(0, points[0].transform.position);
        this.GetComponentInChildren<LineRenderer>().SetPosition(1, points[1].transform.position);
        objectConnections = new SocketScript[2] { emptySocket, emptySocket };
        updateState();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MovePoint()
    {
        this.GetComponentInChildren<LineRenderer>().SetPosition(0, points[0].transform.position);
        this.GetComponentInChildren<LineRenderer>().SetPosition(1, points[1].transform.position);
    }

    void finalizePoint1(GameObject o )
    {
        objectConnections[1] = o.GetComponent<SocketScript>();
        updateState();
    }
    void finalizePoint0(GameObject o)
    {
        objectConnections[0] = o.GetComponent<SocketScript>();
        updateState();
    }

    void updateState()
    {
        CustomInputScript.updatePair(this, this.ID);
    }
}
