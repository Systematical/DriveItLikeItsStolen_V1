using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDragScript : MonoBehaviour {
    GameObject old;
    GameObject Nearest;
    public GameObject emptySocket;
    public int ID;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
        this.SendMessageUpwards("MovePoint");
    }

    private void OnMouseUp()
    {
        if(!(Nearest == emptySocket))
        {
            this.transform.position = Nearest.transform.position;
            this.SendMessageUpwards("MovePoint");
        }
        if (this.ID == 1)
        {
            this.SendMessageUpwards("finalizePoint1", Nearest);
        }
        else
        {
            this.SendMessageUpwards("finalizePoint0", Nearest);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SOCKET")
        {
            Nearest = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Nearest = emptySocket;
    }
}
