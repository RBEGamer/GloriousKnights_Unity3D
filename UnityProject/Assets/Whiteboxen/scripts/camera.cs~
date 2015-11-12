using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public GameObject p1go;
	public GameObject p2go;
	public GameObject p3go;
	public GameObject p4go;

	public GameObject p1ccp;
	public GameObject p2ccp;
	public GameObject p3ccp;
	public GameObject p4ccp;

	private playercontroller p1pc;
	private playercontroller p2pc;
	private playercontroller p3pc;
	private playercontroller p4pc;
	public Vector3 smallest_pos;
	public Vector3 biggest_pos;
	// Use this for initialization
	void Start () {
		p1pc = p1go.GetComponent<playercontroller>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	//nur alle 5 frames

		//kleinste x
		if(p1go.transform.position.x < smallest_pos.x && p1pc.is_playing){smallest_pos.x = p1go.transform.position.x;}
		if(p2go.transform.position.x < smallest_pos.x && p1pc.is_playing){smallest_pos.x = p2go.transform.position.x;}
		if(p3go.transform.position.x < smallest_pos.x && p1pc.is_playing){smallest_pos.x = p3go.transform.position.x;}
		if(p4go.transform.position.x < smallest_pos.x && p1pc.is_playing){smallest_pos.x = p4go.transform.position.x;}
		//kleinste y
		if(p1go.transform.position.y < smallest_pos.y && p1pc.is_playing){smallest_pos.y = p1go.transform.position.y;}
		if(p2go.transform.position.y < smallest_pos.y && p1pc.is_playing){smallest_pos.y = p2go.transform.position.y;}
		if(p3go.transform.position.y < smallest_pos.y && p1pc.is_playing){smallest_pos.y = p3go.transform.position.y;}
		if(p4go.transform.position.y < smallest_pos.y && p1pc.is_playing){smallest_pos.y = p4go.transform.position.y;}


	}
}
