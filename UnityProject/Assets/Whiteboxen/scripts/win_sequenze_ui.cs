using UnityEngine;
using System.Collections;

public class win_sequenze_ui : MonoBehaviour {

	public GameObject win_pos_holder;
	private game_manager gm;
public enum wstate
	{
		waiting_for_win, goto_win_plattform, show_win_animations
	}

	public wstate win_state;
	// Use this for initialization
	void Start () {
		gm =GameObject.Find("game_manager").GetComponent<game_manager>();
		win_state = wstate.waiting_for_win;
		win_pos_holder.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(win_state == wstate.waiting_for_win){
			win_pos_holder.SetActive(false);
			if(game_manager.gstate == vars.game_state.win_sequenze){
				win_state = wstate.goto_win_plattform;
			}
		}

		if(win_state == wstate.show_win_animations){
			//bla bla
		}


		if(win_state == wstate.goto_win_plattform){
			win_pos_holder.SetActive(true);
			if(gm.winner_score_list.Count > 0){
			if(gm.winner_score_list[0].player == vars.player_id.player_1){
				GameObject.Find(vars.player_id.player_1.ToString()).transform.position = GameObject.Find("1st_place").transform.position;
				GameObject.Find(vars.player_id.player_1.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[0].player == vars.player_id.player_2){
				GameObject.Find(vars.player_id.player_2.ToString()).transform.position = GameObject.Find("1st_place").transform.position;
				GameObject.Find(vars.player_id.player_2.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[0].player == vars.player_id.player_3){
				GameObject.Find(vars.player_id.player_3.ToString()).transform.position = GameObject.Find("1st_place").transform.position;
				GameObject.Find(vars.player_id.player_3.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[0].player == vars.player_id.player_4){
				GameObject.Find(vars.player_id.player_4.ToString()).transform.position = GameObject.Find("1st_place").transform.position;
				GameObject.Find(vars.player_id.player_4.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			}

			if(gm.winner_score_list.Count > 1){
			if(gm.winner_score_list[1].player == vars.player_id.player_1){
				GameObject.Find(vars.player_id.player_1.ToString()).transform.position = GameObject.Find("2nd_place").transform.position;
				GameObject.Find(vars.player_id.player_1.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[1].player == vars.player_id.player_2){
				GameObject.Find(vars.player_id.player_2.ToString()).transform.position = GameObject.Find("2nd_place").transform.position;
				GameObject.Find(vars.player_id.player_2.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[1].player == vars.player_id.player_3){
				GameObject.Find(vars.player_id.player_3.ToString()).transform.position = GameObject.Find("2nd_place").transform.position;
				GameObject.Find(vars.player_id.player_3.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[1].player == vars.player_id.player_4){
				GameObject.Find(vars.player_id.player_4.ToString()).transform.position = GameObject.Find("2nd_place").transform.position;
				GameObject.Find(vars.player_id.player_4.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			}

			if(gm.winner_score_list.Count > 2){
			if(GameObject.Find("game_manager").GetComponent<game_manager>().winner_score_list[2].player == vars.player_id.player_1){
				GameObject.Find(vars.player_id.player_1.ToString()).transform.position = GameObject.Find("3rd_place").transform.position;
				GameObject.Find(vars.player_id.player_1.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[2].player == vars.player_id.player_2){
				GameObject.Find(vars.player_id.player_2.ToString()).transform.position = GameObject.Find("3rd_place").transform.position;
				GameObject.Find(vars.player_id.player_2.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[2].player == vars.player_id.player_3){
				GameObject.Find(vars.player_id.player_3.ToString()).transform.position = GameObject.Find("3rd_place").transform.position;
				GameObject.Find(vars.player_id.player_3.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			if(gm.winner_score_list[2].player == vars.player_id.player_4){
				GameObject.Find(vars.player_id.player_4.ToString()).transform.position = GameObject.Find("3rd_place").transform.position;
				GameObject.Find(vars.player_id.player_4.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
			}
			}


			if(gm.winner_score_list.Count > 3){
				if(gm.winner_score_list[3].player == vars.player_id.player_1){
					GameObject.Find(vars.player_id.player_1.ToString()).transform.position = GameObject.Find("4th_place").transform.position;
					GameObject.Find(vars.player_id.player_1.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
				}
				if(gm.winner_score_list[3].player == vars.player_id.player_2){
					GameObject.Find(vars.player_id.player_2.ToString()).transform.position = GameObject.Find("4th_place").transform.position;
					GameObject.Find(vars.player_id.player_2.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
				}
				if(gm.winner_score_list[3].player == vars.player_id.player_3){
					GameObject.Find(vars.player_id.player_3.ToString()).transform.position = GameObject.Find("4th_place").transform.position;
					GameObject.Find(vars.player_id.player_3.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
				}
				if(gm.winner_score_list[3].player == vars.player_id.player_4){
					GameObject.Find(vars.player_id.player_4.ToString()).transform.position = GameObject.Find("4th_place").transform.position;
					GameObject.Find(vars.player_id.player_4.ToString()).GetComponent<playercontroller>().set_model_roation_to_front();
				}
			}

			win_state = wstate.show_win_animations;

		}
	}
}
