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
    public float maxTurn = 10;
    float direction = 1;
    float lastDirectionUpdate = 0f;
    
    public GameObject hpBar;
    public GameObject gameMaster;

    public float maxHP = 100f;
    float health = 100f;

    public AudioClip driveAudio;
    public AudioClip crashAudio;
    public AudioClip turnAudio;
    void Start()
    {
     rigidbody2D = this.GetComponent<Rigidbody2D>();
        lastDirectionUpdate = Time.time;
        health = maxHP;



        //driveAudio = Resources.Load("Sounds/drive.wav", typeof(AudioClip)) as AudioClip;
        //crashAudio = Resources.Load("Sounds/crash.wav", typeof(AudioClip)) as AudioClip;
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
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                this.GetComponent<AudioSource>().PlayOneShot(driveAudio);
            }
        }
        else
        {
            this.GetComponent<AudioSource>().Stop();
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
            rigidbody2D.AddTorque(turnpower * curspeed.magnitude / maxspeed);
            if (rigidbody2D.angularVelocity > maxTurn)
            {
                rigidbody2D.angularVelocity = maxTurn;
            }
        }
        if (CustomInputScript.GetKeyDown(Command.RightTurn))
        {
            rigidbody2D.AddTorque(-turnpower * curspeed.magnitude / maxspeed);
            if (rigidbody2D.angularVelocity > maxTurn)
            {
                rigidbody2D.angularVelocity = maxTurn;
            }
            // transform.Rotate(Vector3.forward * -turnpower);
        }

        if (CustomInputScript.GetKeyDown(Command.RightBrake))
        {
            if(rigidbody2D.velocity.magnitude > 0)
            {
                //Debug.Log(rigidbody2D.velocity.magnitude);
                float yVel = transform.InverseTransformDirection(rigidbody2D.velocity).y;
                if (yVel<4)
                {
                    rigidbody2D.velocity = new Vector2(0, 0);
                }
                else
                {
                    rigidbody2D.AddForce(transform.up * -1 * power);
                    rigidbody2D.AddTorque(-turnpower);
                }
            }

        }
        if (CustomInputScript.GetKeyDown(Command.LeftBrake))
        {
            if (rigidbody2D.velocity.magnitude > 0)
            {
                float yVel = transform.InverseTransformDirection(rigidbody2D.velocity).y;
                if (yVel < 4)
                {
                    rigidbody2D.velocity = new Vector2(0, 0);
                }
                else
                {
                    rigidbody2D.AddForce(transform.up * -1 * power);
                    rigidbody2D.AddTorque(turnpower);
                }
            }

        }
        if(!CustomInputScript.GetKeyDown(Command.LeftTurn) && !CustomInputScript.GetKeyDown(Command.RightTurn) && !CustomInputScript.GetKeyDown(Command.LeftBrake) && !CustomInputScript.GetKeyDown(Command.RightBrake)  )
        {
            rigidbody2D.angularVelocity = 0;
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
    
    void ApplyDamage(int amnt)
    {
        Debug.Log(health);
        health -= amnt;
        Debug.Log(health);
        Debug.Log(health / maxHP);
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(health / maxHP * 100, 19);
        hpBar.transform.position = new Vector2(hpBar.transform.position.x - amnt/2, hpBar.transform.position.y);

        this.GetComponent<AudioSource>().Stop();

        if(health<=0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        this.GetComponent<AudioSource>().PlayOneShot(crashAudio);
        if (coll.gameObject.tag == "Finish")
            SceneManager.LoadScene("GameOverScreen");

    }
}
