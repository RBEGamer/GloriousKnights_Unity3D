using UnityEngine;
using System.Collections;

public class goal : MonoBehaviour {


	public GameObject explosion_holder;
	private Vector3 pp;
	private bool in_pp;
	private ParticleSystem ps;
	void OnCollisionEnter(Collision other) {

		if(other.gameObject.tag.Contains("ball")) {
			explosion_holder.transform.position = other.contacts[0].point;
			ps.Play();
			in_pp = false;
			if(other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_1){game_manager.score_player_1++;}
			else if(other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_2){game_manager.score_player_2++;}
			else if(other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_3){game_manager.score_player_3++;}
			else if(other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_4){game_manager.score_player_4++;}
			else{other.collider.gameObject.GetComponent<ball>().last_contact = vars.player_id.none;}
			other.collider.gameObject.GetComponent<ball>().spawn(); //spawn ball
		}
	}




	// Use this for initialization
	void Start () {
		pp = GameObject.Find("pause_position").transform.position;
		ps = explosion_holder.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!in_pp){
			if(!ps.isPlaying){
			explosion_holder.transform.position = pp;
			in_pp = true;
			}
		}
	}
}
