using UnityEngine;
using System.Collections;

public class vars : MonoBehaviour {



	public enum player_id{
		none, player_1, player_2, player_3, player_4
	}

	public enum team_id{
		team_blue, team_red, both
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
