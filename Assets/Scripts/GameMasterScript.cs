﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour {
    public float xmin = 0f;
    public float xmax = 0f;
    public float ymin = 0f;
    public float ymax = 0f;

    public float blackzone_xmax = 0f;
    public float blackzone_ymax = 0f;

    public float numberRocks = 1000;
    public GameObject RockPrefab;

    public AudioSource playerAudio;
    public AudioSource radioAudio;


    // Use this for initialization
    void Start () {
        Random r = new Random();
        for (int i = 0; i < numberRocks; i++)
        {
            float tx =Random.Range(xmin, xmax);
            float ty =Random.Range(ymin, ymax);
            while (tx < blackzone_xmax && ty < blackzone_ymax)
            {
                tx = Random.Range(xmin, xmax);
                ty = Random.Range(ymin, ymax);
            }
            Instantiate(RockPrefab, new Vector3(Random.Range(xmin, xmax), Random.Range(-ymin, -ymax), 0), Quaternion.identity);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
