using UnityEngine;
using System.Collections;

public class win_sequenze_ui : MonoBehaviour {

	public GameObject win_pos_holder;
	private game_manager gm;

  public GameObject player1_scrpit_obj;
  public GameObject player2_scrpit_obj;
  public GameObject player3_scrpit_obj;
  public GameObject player4_scrpit_obj;

    public GameObject firework_holder;




    public float time_to_go_back_to_menu_max = 15.0f;
    private float time_to_go_back_to_menu = 15.0f;
    private bool timer_started;
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
        time_to_go_back_to_menu = time_to_go_back_to_menu_max;
        timer_started = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {




        if (timer_started)
        {
            time_to_go_back_to_menu -= Time.deltaTime;
            if(time_to_go_back_to_menu <= 0.0f)
            {
                time_to_go_back_to_menu = time_to_go_back_to_menu_max;
                timer_started = false;
                game_manager.gstate = vars.game_state.main_menu;

                firework_holder.GetComponent<firework_animator>().stop_all();
                player1_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
                player2_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
                player3_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
                player4_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();


                gm.reset_game();
            }
        }



		if(win_state == wstate.waiting_for_win){
			win_pos_holder.SetActive(false);
			if(game_manager.gstate == vars.game_state.win_sequenze){
				win_state = wstate.goto_win_plattform;
			}
		}

		if(win_state == wstate.show_win_animations){
            //bla bla
            firework_holder.GetComponent<firework_animator>().stop_all();
            firework_holder.GetComponent<firework_animator>().play_all();
            player1_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();
            player2_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();
            player3_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();
            player4_scrpit_obj.GetComponent<adv_playercontroller>().enabel_dance();

         
        }


        if (win_state == wstate.goto_win_plattform){
			win_pos_holder.SetActive(true);
            timer_started = true;
            time_to_go_back_to_menu = time_to_go_back_to_menu_max;
            gm.generate_win_table();
      //REMOVE BALL FROM HEAD
      GameObject.Find("ball").GetComponent<ball>().carried_by = vars.player_id.none;
      GameObject.Find("ball").GetComponent<ball>().set_to_pause_pos();

            player1_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
            player2_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
            player3_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();
            player4_scrpit_obj.GetComponent<adv_playercontroller>().set_pause_pos();



      player1_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();
      player2_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();
      player3_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();
      player4_scrpit_obj.GetComponent<adv_playercontroller>().reset_velocity_and_gravity();



            for (int i = 0; i < gm.winner_score_list.Count; i++)
            {

                Transform current_place;
                Debug.Log( i.ToString() + " : player:" +gm.winner_score_list[i].player +" score:" + gm.winner_score_list[i].score);

                switch (i)
                {
                        case 0:
                        
                        current_place = GameObject.Find("1st_place").transform;
                        break;

                    case 1:
                        current_place = GameObject.Find("2nd_place").transform;
                        break;

                    case 2:
                        current_place = GameObject.Find("3rd_place").transform;
                        break;


                    case 3:
                        current_place = GameObject.Find("4th_place").transform;
                        break;

                    default:
                        current_place = GameObject.Find("pause_position").transform;
                        break;
                }


                if(gm.winner_score_list[i].player == vars.player_id.player_1 && player1_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
                {
                    player1_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = current_place.position;
                }
                else if (gm.winner_score_list[i].player == vars.player_id.player_2 && player2_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
                {
                    player2_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = current_place.position;
                }
                else if (gm.winner_score_list[i].player == vars.player_id.player_3 && player3_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
                {
                    player3_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = current_place.position;
                }
                else if (gm.winner_score_list[i].player == vars.player_id.player_4 && player4_scrpit_obj.GetComponent<adv_playercontroller>().is_playing)
                {
                    player4_scrpit_obj.GetComponent<adv_playercontroller>().physics_container.transform.position = current_place.position;
                }



            }




			win_state = wstate.show_win_animations;

		}
	}
}
