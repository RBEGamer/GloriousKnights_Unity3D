using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ingame_gui_manager : MonoBehaviour {
	
	public GameObject score_1_text;
	public GameObject score_2_text;
	public GameObject score_3_text;
	public GameObject score_4_text;
	public GameObject time;
	private Text sp1t;
	private Text sp2t;
	private Text sp3t;
	private Text sp4t;
	private Text tt;

  public GameObject sign_holder_1;
  public GameObject sign_holder_2;
  public GameObject sign_holder_3;
  public GameObject sign_holder_4;
    

  public GameObject player1_scrpit_obj;
  public GameObject player2_scrpit_obj;
  public GameObject player3_scrpit_obj;
  public GameObject player4_scrpit_obj;


	public GameObject time_add_text;
	private Text time_add_text_text;


	public GameObject spawn_text_obj;
	private Text sto;

	public void refresh_count_ui(){
		if(game_manager.gstate == vars.game_state.playing || game_manager.gstate == vars.game_state.spawn){
			tt.enabled = true;
			time_add_text_text.enabled = true;
      if (player1_scrpit_obj.GetComponent<adv_playercontroller>().is_playing) { sign_holder_1.SetActive(true); } else { sign_holder_1.SetActive(false); }
      if (player2_scrpit_obj.GetComponent<adv_playercontroller>().is_playing) { sign_holder_2.SetActive(true); } else { sign_holder_2.SetActive(false); }
      if (player3_scrpit_obj.GetComponent<adv_playercontroller>().is_playing) { sign_holder_3.SetActive(true); } else { sign_holder_3.SetActive(false); }
      if (player4_scrpit_obj.GetComponent<adv_playercontroller>().is_playing) { sign_holder_4.SetActive(true); } else { sign_holder_4.SetActive(false); }
		}else{
		
			tt.enabled = false;
			time_add_text_text.enabled = false;
		}


		if(game_manager.gstate == vars.game_state.spawn){
			sto.enabled = true;
      sign_holder_1.SetActive(false);
      sign_holder_2.SetActive(false);
      sign_holder_3.SetActive(false);
      sign_holder_4.SetActive(false);
   
		}else{
			sto.enabled = false;
		}

    if (game_manager.gstate == vars.game_state.main_menu || game_manager.gstate == vars.game_state.game_settings || game_manager.gstate == vars.game_state.credits || game_manager.gstate == vars.game_state.settings)
    {
      sign_holder_1.SetActive(false);
      sign_holder_2.SetActive(false);
      sign_holder_3.SetActive(false);
      sign_holder_4.SetActive(false);
            tt.enabled = false;
        }

	}
	// Use this for initialization
	void Start () {
		this.name = "UI_MAIN_HOLDER";
		sp1t = score_1_text.GetComponent<Text>();
		sp2t = score_2_text.GetComponent<Text>();
		sp3t = score_3_text.GetComponent<Text>();
		sp4t = score_4_text.GetComponent<Text>();
		tt = time.GetComponent<Text>();
		time_add_text_text = time_add_text.GetComponent<Text>();
		sto = spawn_text_obj.GetComponent<Text>();

    sign_holder_1.SetActive(false);
    sign_holder_2.SetActive(false);
    sign_holder_3.SetActive(false);
    sign_holder_4.SetActive(false);


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(game_manager.gstate == vars.game_state.playing){
      if (spawn_text_obj.activeSelf) { spawn_text_obj.SetActive(false); }
		sp1t.text = game_manager.score_player_1.ToString();
		sp2t.text = game_manager.score_player_2.ToString();
		sp3t.text = game_manager.score_player_3.ToString();
		sp4t.text = game_manager.score_player_4.ToString();
		tt.text = ((int)game_manager.curr_game_time).ToString();
		if(game_manager.adtp == vars.add_time_phase.none){time_add_text_text.text = "";}
		else if(game_manager.adtp == vars.add_time_phase.addt0){time_add_text_text.text = "verlängerung 1";}
		else if(game_manager.adtp == vars.add_time_phase.addt1){time_add_text_text.text = "verlängerung 2";}
		}


		if(game_manager.gstate == vars.game_state.spawn){
      spawn_text_obj.SetActive(true);
			if(game_manager.curr_spawn_time > 4.0f){
				sto.text = "" + ((int)game_manager.curr_spawn_time).ToString() + "";
			}else if(game_manager.curr_spawn_time >= 3.0f ){
				sto.text = "READY";
			}else if(game_manager.curr_spawn_time >= 1.5f){
				sto.text = "STEADY";
			}else if(game_manager.curr_spawn_time >= 0.0f){
				sto.text = "GO";
			}else{
				sto.text = "";
        spawn_text_obj.SetActive(false);
			}




		}
	}
}
