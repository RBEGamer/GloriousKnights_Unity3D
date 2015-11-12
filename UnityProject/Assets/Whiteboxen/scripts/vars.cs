using UnityEngine;
using System.Collections;
using InControl;

public class vars : MonoBehaviour
{


    public static string player_control_punsh_trigger_name = "RightBumper";
    public static string player_control_jump_trigger_name = "Action1";
    public static string player_control_menu_trigger_name = "Menu";
    public static string player_control_carry_up_trigger_name = "Action2";
    public static string player_control_carry_down_trigger_name = "Action3";
    public   enum player_controls
    {
       none, punsh, jump, menu, carry_up, carry_down 
    }
    

	public enum player_id{
		none, player_1, player_2, player_3, player_4
	}

	public enum team_id{
		none, team_blue, team_red, both
	}

	public enum game_state{
		playing, win_sequenze, spawn

	}

	public enum view_direction
	{
		none, front, back, left, right
	}

	public struct player_statistics
	{
		public uint ball_contacts;
		public uint jumps;
		public uint schlag;
		public uint punshed_player_1;
		public uint punshed_player_2;
		public uint punshed_player_3;
		public uint punshed_player_4;

	}


	public struct win_player
	{
		public int score;
		public vars.player_id player;
	}



	public enum add_time_phase{
		none, addt0, addt1
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
