using UnityEngine;
using System.Collections;

public class wall_collider : MonoBehaviour {

    public GameObject p1_obj;
    public GameObject p2_obj;
    public GameObject p3_obj;
    public GameObject p4_obj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        if(other.gameObject.tag == "player_1")
        {
            if(GameObject.Find("Ball").GetComponent<ball>().carried_by == vars.player_id.player_1)
            {
                GameObject.Find("ball").GetComponent<ball>().decarry();
            }
        }

        if (other.gameObject.tag == "player_2")
        {
            if (GameObject.Find("Ball").GetComponent<ball>().carried_by == vars.player_id.player_2)
            {
                GameObject.Find("ball").GetComponent<ball>().decarry();
            }
        }

        if (other.gameObject.tag == "player_3")
        {
            if (GameObject.Find("Ball").GetComponent<ball>().carried_by == vars.player_id.player_3)
            {
                GameObject.Find("ball").GetComponent<ball>().decarry();
            }
        }

        if (other.gameObject.tag == "player_4")
        {
            if (GameObject.Find("Ball").GetComponent<ball>().carried_by == vars.player_id.player_4)
            {
                GameObject.Find("ball").GetComponent<ball>().decarry();
            }
        }
    }


}
