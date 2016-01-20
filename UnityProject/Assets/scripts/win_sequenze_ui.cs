using UnityEngine;
using System.Collections;

public class win_sequenze_ui : MonoBehaviour {

	public GameObject win_pos_holder;
	private game_manager gm;

  public GameObject player1_scrpit_obj;
  public GameObject player2_scrpit_obj;
  public GameObject player3_scrpit_obj;
  public GameObject player4_scrpit_obj;
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
      //REMOVE BALL FROM HEAD
      GameObject.Find("ball").GetComponent<ball>().carried_by = vars.player_id.none;
      GameObject.Find("ball").GetComponent<ball>().set_to_pause_pos();

      player1_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();
      player2_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();
      player3_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();
      player4_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();


            player1_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();
            player2_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();
            player3_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();
            player4_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();



            if (gm.winner_score_list.Count > 0){
			if(gm.winner_score_list[0].player == vars.player_id.player_1){

    
             player1_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("1st_place").transform.position;
				
			}
			if(gm.winner_score_list[0].player == vars.player_id.player_2){
        player2_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("1st_place").transform.position;

			}
			if(gm.winner_score_list[0].player == vars.player_id.player_3){
        player3_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("1st_place").transform.position;
				
			}
			if(gm.winner_score_list[0].player == vars.player_id.player_4){
        player4_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("1st_place").transform.position;
			
			}
			}

			if(gm.winner_score_list.Count > 1){
			if(gm.winner_score_list[1].player == vars.player_id.player_1){
        player1_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("2nd_place").transform.position;

			}
			if(gm.winner_score_list[1].player == vars.player_id.player_2){
        player2_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("2nd_place").transform.position;
			
			}
			if(gm.winner_score_list[1].player == vars.player_id.player_3){
        player3_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("2nd_place").transform.position;
			
			}
			if(gm.winner_score_list[1].player == vars.player_id.player_4){
        player4_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("2nd_place").transform.position;
			
			}
			}

			if(gm.winner_score_list.Count > 2){
			if(GameObject.Find("game_manager").GetComponent<game_manager>().winner_score_list[2].player == vars.player_id.player_1){
        player1_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("3rd_place").transform.position;
			
			}
			if(gm.winner_score_list[2].player == vars.player_id.player_2){
        player2_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("3rd_place").transform.position;
		
			}
			if(gm.winner_score_list[2].player == vars.player_id.player_3){
        player3_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("3rd_place").transform.position;
			
			}
			if(gm.winner_score_list[2].player == vars.player_id.player_4){
        player4_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("3rd_place").transform.position;
			
			}
			}


			if(gm.winner_score_list.Count > 3){
				if(gm.winner_score_list[3].player == vars.player_id.player_1){
          player1_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("4th_place").transform.position;
				
				}
				if(gm.winner_score_list[3].player == vars.player_id.player_2){
          player2_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("4th_place").transform.position;
				
				}
				if(gm.winner_score_list[3].player == vars.player_id.player_3){
          player3_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("4th_place").transform.position;
					
				}
				if(gm.winner_score_list[3].player == vars.player_id.player_4){
          player4_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = GameObject.Find("4th_place").transform.position;
					
				}
			}

			win_state = wstate.show_win_animations;

		}
	}
}
