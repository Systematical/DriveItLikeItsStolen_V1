using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
    // Use this for initialization
    Rigidbody2D rigidbody2D;
    public float maxspeed=1000;
    public float power = 1000;
    public float friction = 3;
    public float turnpower =5;
    float direction = 1;
    float lastDirectionUpdate = 0f;

    void Start()
    {
     rigidbody2D = this.GetComponent<Rigidbody2D>();
        lastDirectionUpdate = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 curspeed = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y);
        if (curspeed.magnitude > maxspeed)
        {
            curspeed = curspeed.normalized;
            curspeed *= maxspeed;
        }

        if (CustomInputScript.GetKeyDown(Command.Drive))
        {
            rigidbody2D.AddForce(transform.up * power * direction);
            rigidbody2D.drag = friction;
        }
        if (CustomInputScript.GetKeyDown(Command.SwitchDirection))
        {
            if(Time.time - lastDirectionUpdate >= 1)
            {
                direction = -direction;
                lastDirectionUpdate = Time.time;
                Debug.Log(direction);
            }
        }
        if (CustomInputScript.GetKeyDown(Command.LeftTurn))
        {
            transform.Rotate(Vector3.forward * turnpower);
        }
        if (CustomInputScript.GetKeyDown(Command.RightTurn))
        {
            transform.Rotate(Vector3.forward * -turnpower);
        }

        noGas();

    }
    void noGas()
    {
        bool gas;
        if (CustomInputScript.GetKeyDown(Command.Drive))
        {
            gas = true;
        }
        else
        {
            gas = false;
        }

        if (!gas)
        {
            rigidbody2D.drag = friction * 2;
        }
    }

}
