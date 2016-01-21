using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class game_manager : MonoBehaviour {
	private AudioSource asource;
	public static vars.game_state gstate;	
	public static int score_player_1 = 0;
	public static int score_player_2 = 0;
	public static int score_player_3 = 0;
	public static int score_player_4 = 0;
	public static float game_time = 60.0f;
	public static float curr_game_time = 60.0f;
	public float add_time_0 = 10.0f;
	public float add_time_1 = 20.0f;
	public bool player_spawned;
	public float spaw_time = 5.0f;
	public static float curr_spawn_time = 5.0f;

  public static int volume  =100;

  public GameObject player1_scrpit_obj;
  public GameObject player2_scrpit_obj;
  public GameObject player3_scrpit_obj;
  public GameObject player4_scrpit_obj;


	public static vars.add_time_phase adtp;
	public static int player_count = 0;
	public List<vars.win_player> winner_score_list = new List<vars.win_player>();
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        asource = this.GetComponent<AudioSource>();
		spaw_time += 1; //damit immer erst die spieler un dann der ball gespawned wird
		this.name = "game_manager";
    game_manager.gstate = vars.game_state.game_starting;

        player1_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
        player2_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
        player3_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
        player4_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
        GameObject.Find("ball").GetComponent<ball>().set_to_pause_pos();
    }
	public void generate_win_table(){
        //create score list
        winner_score_list.Clear();
		vars.win_player tmp = new vars.win_player();
    if (player1_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
    {
		tmp.player = vars.player_id.player_1;
		tmp.score = game_manager.score_player_1;
		winner_score_list.Add(tmp);
		}
		if(player2_scrpit_obj.GetComponent<adv_playercontroller>().is_playing){
		tmp.player = vars.player_id.player_2;
		tmp.score = game_manager.score_player_2;
		winner_score_list.Add(tmp);
		}
		if(player3_scrpit_obj.GetComponent<adv_playercontroller>().is_playing){
		tmp.player = vars.player_id.player_3;
		tmp.score = game_manager.score_player_3;
		winner_score_list.Add(tmp);
		}
		if(player4_scrpit_obj.GetComponent<adv_playercontroller>().is_playing){
		tmp.player = vars.player_id.player_4;
		tmp.score = game_manager.score_player_4;
		winner_score_list.Add(tmp);
		}
		//sort score list from high to low
		vars.win_player temp;
		for (int write = 0; write < winner_score_list.Count; write++) {
			for (int sort = 0; sort < winner_score_list.Count - 1; sort++) {
				if (winner_score_list[sort].score > winner_score_list[sort + 1].score) {
					temp = winner_score_list[sort + 1];
					winner_score_list[sort + 1] = winner_score_list[sort];
					winner_score_list[sort] = temp;
				}
			}
		}
        winner_score_list.Reverse();
		game_manager.gstate = vars.game_state.win_sequenze;
		GameObject.Find("UI_MAIN_HOLDER").GetComponent<ingame_gui_manager>().refresh_count_ui();
	}


	public void reset_game(){
		player_spawned = false;
		game_manager.curr_game_time = game_manager.game_time;
		game_manager.curr_spawn_time = spaw_time;
		adtp = vars.add_time_phase.none;
		gstate = vars.game_state.spawn;

    player1_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
    player2_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
    player3_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
    player4_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
		GameObject.Find("ball").GetComponent<ball>().set_to_pause_pos();
		game_manager.score_player_1 = 0;
		game_manager.score_player_2 = 0;
		game_manager.score_player_3 = 0;
		game_manager.score_player_4 = 0;
		game_manager.player_count = 0;
		GameObject.Find("UI_MAIN_HOLDER").GetComponent<ingame_gui_manager>().refresh_count_ui();
		asource.Play();
	}

	// Update is called once per frame
	void Update () {
		if(gstate == vars.game_state.spawn){
			if( !player_spawned){
				//spawn player
        if (player1_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
        {
          player1_scrpit_obj.GetComponent<adv_playercontroller>().spawn();
				}

        if (player2_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
        {
          player2_scrpit_obj.GetComponent<adv_playercontroller>().spawn();
				}
        if (player3_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
        {
            player3_scrpit_obj.GetComponent<adv_playercontroller>().spawn();
					}
        if (player4_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
        {
              player4_scrpit_obj.GetComponent<adv_playercontroller>().spawn();
						}

				player_spawned = true;
			}

			if(game_manager.curr_spawn_time <= 0.0f){
				gstate  = vars.game_state.playing;
        curr_game_time = game_manager.game_time;
				//spawn ball
				GameObject.Find("ball").GetComponent<ball>().spawn();
				GameObject.Find("UI_MAIN_HOLDER").GetComponent<ingame_gui_manager>().refresh_count_ui();
			}else{game_manager.curr_spawn_time -= Time.deltaTime;}
			}



		if(game_manager.curr_game_time <= 0.0f && gstate == vars.game_state.playing){
			if(game_manager.score_player_1 == game_manager.score_player_2 && //||
			   game_manager.score_player_3 == game_manager.score_player_4 && //||
			   game_manager.score_player_1 == game_manager.score_player_3 && //||
			   game_manager.score_player_1 == game_manager.score_player_4 && //||
			   game_manager.score_player_2 == game_manager.score_player_3 && //||
			   game_manager.score_player_2 == game_manager.score_player_4){
				if(game_manager.adtp == vars.add_time_phase.none){game_manager.adtp = vars.add_time_phase.addt0; curr_game_time += add_time_0;}
				else if(game_manager.adtp == vars.add_time_phase.addt0){game_manager.adtp = vars.add_time_phase.addt1; curr_game_time += add_time_1;}
				else if(game_manager.adtp == vars.add_time_phase.addt1 && game_manager.curr_game_time <= 0.0f){generate_win_table();}
			}else{generate_win_table();}
		}else{
			game_manager.curr_game_time -= Time.deltaTime;
		}







	}
}
