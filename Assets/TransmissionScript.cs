using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionScript : MonoBehaviour {
    // Use this for initialization
    Vector3 upPosition;
    Vector3 downPosition;
    Vector3 startPos;
    Vector3 endPos;
    bool isUp;
    float startTime;
    float journeyLength;
    float speed = 20;
	void Start () {
        upPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, 0 +300, 1));
        downPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, -300, 1));
        this.transform.position = downPosition;
        isUp = false;


    }
	
	// Update is called once per frame
	void Update () {
        if(journeyLength>0f)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
        }
    }

    public void toggleDisplay()
    {
        if(isUp)
        {
            journeyLength = Vector3.Distance(upPosition, downPosition);
            startPos = upPosition;
            endPos = downPosition;
            isUp = false;
        }
        else
        {
            journeyLength = Vector3.Distance(downPosition, upPosition);
            startPos = downPosition;
            endPos = upPosition;
            isUp = true;
        }
        startTime = Time.time;
    }
}
