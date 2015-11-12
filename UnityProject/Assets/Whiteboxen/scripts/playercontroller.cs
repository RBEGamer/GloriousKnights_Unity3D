using UnityEngine;
using System.Collections;
using InControl;
using System.Collections.Generic;
public class playercontroller : MonoBehaviour {
	public bool is_playing;
	private Vector3 spawn_pos;
	public vars.player_id player_id;
	public bool isGrounded = false;
	public GameObject spawn_particle_system_holder;
	public GameObject player_model_holder;
	private InputDevice inputDevice;
	Rigidbody rd;
	public ball ball_instance;
	private int  pid = -1;
	public vars.view_direction view_dir;
	public GameObject camera_position_corrention_point;


	//MOVEMENT
	public float speed_multiplier_ground = 15.0f;
	public float speed_multiplier_air = 10.0f;
	public float jump_height = 60.0f;
	public float jump_height_ground_trigger = 0.3f;
	public float spawn_particle_system_speed = 4.0f;
	public float rotation_update_timer_toggle = 0.2f;
	private float rotation_update_timer_curr = 0.0f;
	public float roation_change_velocity = 0.1f;
	public bool enable_fron_rotation = false;
	//PUNSH
	public GameObject punsh_obj;
	private BoxCollider punsh_collider;
	private Vector3 punsh_start_position;
	public Vector3 punsh_direction_range = new Vector3(0.0f,-0.52f,0.27f);
	public Vector3 punsh_collision_force = new Vector3(1.0f,0.5f, 0.0f);
	private bool punsh_state = false;
	public float punsh_duration = 0.2f;
	private float curr_punsh_duration = 0.2f;
	private float curr_punsh_cooldown = 0.2f;
	public float punsh_cooldown_duration = 0.2f;
	public bool punsh_cooldown_state = false;
	//RUMBLE
	public float rumble_timer_current = 0.0f;
	public float rumble_timer_max = 0.3f;
	private bool rumble_active = false;
	public float rumble_intense = 5.0f;
	public vars.player_statistics player_stat;
	//CARRY DOWN
	public GameObject TrajectoryPointPrefeb;
	private int numOfTrajectoryPoints = 30;
	private List<GameObject> trajectoryPoints;
	public GameObject dot_parent_holder;
	public Transform throwDirectionObject;
	public Vector3  throw_parable_velocity = new Vector3(0.0f, 2.0f, 4.0f);
	public int show_points = 10;
	public Vector3 throw_parable_min = new Vector3(0.0f, 2.0f, 4.0f);
	public Vector3 throw_parable_max = new Vector3(0.0f, 5.0f, 6.0f);
	public bool throw_points_cleared = false;
	public Vector3 throw_ball_multiplier = new Vector3(40.0f, 50.0f, 0.0f);
    //KNOCK DODWN
    private float knock_down_timer_current = 2.0f;
    public float knock_down_timer_max = 2.0f;
    public int curr_knocks = 0;
    public int max_knocks = 5;
    public bool knocked_down = false;
    public float knock_down_time_limit = 7.0f;
    public float last_nock_time = 0.0f;
    public bool is_knock_limit_running = false;
    //ICH WERDE GETROFFEN ALSO ALLES AUS MEINER SICHT
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player " + player_id.ToString() + " collided :"+  other.gameObject.name);
        if (other.gameObject.tag.Contains("_punsh"))
        {


            if (other.gameObject.tag.Contains("_punsh"))
            {
                if(other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<playercontroller>().player_id != player_id)
                {
                    if (other.gameObject.transform.parent.gameObject.transform.parent.transform.position.x >= this.transform.position.x) {
                        rd.AddForce(new Vector3(-punsh_collision_force.x, punsh_collision_force.y, punsh_collision_force.z));
                        curr_knocks++;
                        if (!is_knock_limit_running)
                        {
                            is_knock_limit_running = true;
                            last_nock_time = 0.0f;
                        }
                      
                    }
                    else
                    {
                        rd.AddForce(new Vector3(punsh_collision_force.x, punsh_collision_force.y, punsh_collision_force.z));
                        curr_knocks++;
                        if (!is_knock_limit_running)
                        {
                            is_knock_limit_running = true;
                            last_nock_time = 0.0f;
                        }
                    }
                   

                }
            }
            else if(other.gameObject.tag == "Player")
            {
                if(other.gameObject.GetComponent<playercontroller>().player_id != player_id)
                {
                    if (other.gameObject.transform.position.x >= this.transform.position.x)
                    {
                        rd.AddForce(new Vector3(-punsh_collision_force.x, punsh_collision_force.y, punsh_collision_force.z));
                        curr_knocks++;
                        if (!is_knock_limit_running)
                        {
                            is_knock_limit_running = true;
                            last_nock_time = 0.0f;
                        }
                    }
                    else
                    {
                        rd.AddForce(new Vector3(punsh_collision_force.x, punsh_collision_force.y, punsh_collision_force.z));
                        curr_knocks++;
                        if (!is_knock_limit_running)
                        {
                            is_knock_limit_running = true;
                            last_nock_time = 0.0f;
                        }
                    }


    

                }
   
            }
            
            Debug.Log("punshed");
            //rumblen
            rumble();
            //backkick
            //wenn ball dann verlieren
            if (curr_knocks >= max_knocks)
            {
                curr_knocks = 0;
                knock_down_timer_current = knock_down_timer_max;
                knocked_down = true;
            }
            if (ball_instance.carried_by == player_id && ball_instance.carried_by != vars.player_id.none && knocked_down)
            {
                ball_instance.decarry(player_id, player_id);



            }




            ////refresh stats
            //vars.player_id pid = othert.gameObject.GetComponent<playercontroller>().player_id;
            //if (pid == vars.player_id.player_1)
            //{
            //    this.player_stat.punshed_player_1++;
            //}
            //else if (pid == vars.player_id.player_2)
            //{
            //    this.player_stat.punshed_player_2++;
            //}
            //else if (pid == vars.player_id.player_3)
            //{
            //    this.player_stat.punshed_player_3++;
            //}
            //else if (pid == vars.player_id.player_4)
            //{
            //    this.player_stat.punshed_player_4++;
            //}


        }


    }


	void OnCollisionEnter(Collision other) {

		if(other.gameObject.tag.Contains("ground")) {
			isGrounded = true;
		}
      

	}
	void OnCollisionStay(Collision other) {

		if(other.gameObject.tag.Contains("ground")) {
			isGrounded = true;
		}
	}
	void OnCollisionExit(Collision other) {
		if(other.gameObject.tag.Contains("ground")) {
			isGrounded = false;
		}
	}



	public void do_punsh(){
		if(!punsh_state && !punsh_cooldown_state){
			punsh_collider.enabled = true;
			punsh_obj.transform.localPosition = punsh_direction_range;
			curr_punsh_duration = punsh_duration;
			player_stat.schlag++;
			punsh_state = true;

		}

	}

	public void spawn(){
		player_stat.ball_contacts = 0;
		player_stat.jumps = 0;
		player_stat.schlag = 0;
		player_stat.punshed_player_1 = 0;
		player_stat.punshed_player_2 = 0;
		player_stat.punshed_player_3 = 0;
		player_stat.punshed_player_4 = 0;
		is_playing = true;
		rd.useGravity = true;
		this.transform.position = spawn_pos;
		rd.velocity = new Vector3(0.0f,0.0f,0.0f);
		spawn_particle_system_holder.GetComponent<ParticleSystem>().Play();
        curr_knocks = 0;
        knocked_down = false;
    }


	private void instatiate_points(){
		trajectoryPoints = new List<GameObject>();
		// TrajectoryPoints are instatiated
		for(int i=0;i<numOfTrajectoryPoints;i++)
		{
			GameObject dot= (GameObject) Instantiate(TrajectoryPointPrefeb);
			dot.transform.SetParent(dot_parent_holder.transform);
			dot.GetComponent<Renderer>().enabled = false;
			trajectoryPoints.Insert(i,dot);
		}
	}
	public void rumble(){
		inputDevice.Vibrate(rumble_intense);
		rumble_active = true;
	}


	public void set_model_roation_to_front(){
		player_model_holder.transform.rotation =  Quaternion.Euler(270, 0, 0);
		view_dir = vars.view_direction.front;
	}
	public void set_pause_pos(){
		this.transform.position = GameObject.Find("pause_position").gameObject.transform.position; //set the position to the pause pos outside the screen
		rd.useGravity = false;
		set_model_roation_to_front();
	}
	// Use this for initialization
	void Start () {
		ball_instance = GameObject.Find("ball").GetComponent<ball>();;
		punsh_collider = punsh_obj.GetComponent<BoxCollider>();
		punsh_start_position = punsh_obj.transform.localPosition;
		punsh_collider.enabled = false;
		instatiate_points();
		rd = this.GetComponent<Rigidbody>();
		is_playing = false;
		this.name =  player_id.ToString();
		spawn_pos = this.transform.position; //save the spawn pos
		set_pause_pos();
		spawn_particle_system_holder.GetComponent<ParticleSystem>().playbackSpeed = spawn_particle_system_speed;
		spawn_particle_system_holder.GetComponent<ParticleSystem>().loop = false;
		spawn_particle_system_holder.GetComponent<ParticleSystem>().Stop();


		switch (player_id) {
		case vars.player_id.player_1: pid = 0; break;
		case vars.player_id.player_2: pid = 1; break;
		case vars.player_id.player_3: pid = 2; break;
		case vars.player_id.player_4: pid = 3; break;
		default:
			break;
		}
		if(pid >= 0){
			inputDevice = (InputManager.Devices.Count > pid) ? InputManager.Devices[pid] : null;
		}
		if(inputDevice != null){
			is_playing = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(inputDevice != null && game_manager.gstate != vars.game_state.win_sequenze){
			//ROTATE CHARAKTER IN MOVING DIRECTION
			if(rotation_update_timer_curr >= rotation_update_timer_toggle){
				rotation_update_timer_curr = 0.0f;
				if(rd.velocity.x > -roation_change_velocity && rd.velocity.x < roation_change_velocity){
					if(enable_fron_rotation){	player_model_holder.transform.rotation =  Quaternion.Euler(270, 0, 0);view_dir = vars.view_direction.front;}
				}else  if(rd.velocity.x > roation_change_velocity){
					player_model_holder.transform.rotation =  Quaternion.Euler(270, 90, 0);
					view_dir = vars.view_direction.right;
				}else if(rd.velocity.x < -roation_change_velocity){
					player_model_holder.transform.rotation =  Quaternion.Euler(270, -90, 0);
					view_dir = vars.view_direction.left;
				}else{
					if(enable_fron_rotation){	player_model_holder.transform.rotation =  Quaternion.Euler(270, 0, 0);view_dir = vars.view_direction.front;}
				}
			}else{
				rotation_update_timer_curr += Time.deltaTime;
			}

			//MOVE GROUND
			if (inputDevice.LeftStick.Left && isGrounded && !inputDevice.LeftTrigger && !knocked_down)
            {
				rd.AddForce(new Vector3(1.0f,0.0f,0.0f)*speed_multiplier_ground);
			}
			if (inputDevice.LeftStick.Right && isGrounded && !inputDevice.LeftTrigger && !knocked_down)
            {
				rd.AddForce(new Vector3(-1.0f,0.0f,0.0f)*speed_multiplier_ground);
			}
			//MOVE AIR
			if (inputDevice.LeftStick.Left && !isGrounded && !inputDevice.LeftTrigger && !knocked_down)
            {
				rd.AddForce(new Vector3(1.0f,0.0f,0.0f)*speed_multiplier_air);
			}
			if (inputDevice.LeftStick.Right && !isGrounded && !inputDevice.LeftTrigger && !knocked_down)
            {
				rd.AddForce(new Vector3(-1.0f,0.0f,0.0f)*speed_multiplier_air);
			}

			// JUMP X/A
			if (inputDevice.Action1 && this.transform.position.y <= jump_height_ground_trigger && !inputDevice.LeftTrigger && !knocked_down)
			{
              //  rd.velocity = new Vector3(0.0f, 0.0f, 0.0f);
				rd.AddForce(new Vector3(0.0f, 1.0f*jump_height, 0.0f));
				player_stat.jumps++;
			}

			//RESET GAME
			if(inputDevice.MenuWasPressed && (game_manager.gstate == vars.game_state.playing || game_manager.gstate == vars.game_state.win_sequenze) && player_id == vars.player_id.player_1){
				GameObject.Find("game_manager").GetComponent<game_manager>().reset_game();
			}

			//PUNSH 
			if(inputDevice.RightTrigger && !punsh_state && game_manager.gstate == vars.game_state.playing && !knocked_down)
            {
				do_punsh();
			}

			//CARRY UP
			if(inputDevice.Action2 && game_manager.gstate == vars.game_state.playing && !knocked_down)
            {
				ball_instance.carry(player_id, player_id, this.transform.position);
			}

			//CARRY DOWN AND FLY
			if(ball_instance.carried_by == player_id && game_manager.gstate == vars.game_state.playing){
				//last state = true und now state = true
					if(inputDevice.LeftTrigger.LastState && inputDevice.LeftTrigger)
                {
						//throw_parable_min
						float ty = (float)(inputDevice.LeftStick.X); //-1 - +1 -> 0 - 1
						if(ty > 0 && view_dir == vars.view_direction.left){
							throw_parable_velocity = Vector3.Lerp(new Vector3(throw_parable_min.x,throw_parable_min.y,-throw_parable_min.z),new Vector3(throw_parable_max.x,throw_parable_max.y,-throw_parable_max.z),ty);
					}else if(ty < 0 && view_dir == vars.view_direction.right){
							throw_parable_velocity = Vector3.Lerp(new Vector3(throw_parable_min.x,throw_parable_min.y,throw_parable_min.z),new Vector3(throw_parable_max.x,throw_parable_max.y,throw_parable_max.z),-ty);	
						}
					setTrajectoryPoints(new Vector3(throwDirectionObject.position.z, throwDirectionObject.position.y, throwDirectionObject.position.x), throw_parable_velocity);
					//last state = true und now state = false -> werfen
					}else if(!inputDevice.LeftTrigger && inputDevice.LeftTrigger.LastState){
						ball_instance.decarry_fly(player_id, player_id, new Vector3(throw_parable_velocity.z *throw_ball_multiplier.x, throw_parable_velocity.y * throw_ball_multiplier.y, throw_parable_velocity.x * throw_ball_multiplier.z));
						throw_points_cleared = false;
						clearTrajectoryPoints();
					}
				}else{
					clearTrajectoryPoints();
				}

		
		


		


			//PUNSH TIMER
			if(punsh_state){
				if(curr_punsh_duration <= 0.0f){
					punsh_collider.enabled = false;
					punsh_obj.transform.localPosition = punsh_start_position;
					//curr_punsh_duration = punsh_duration;
					punsh_state = false;
					punsh_cooldown_state = true;
					curr_punsh_cooldown = punsh_cooldown_duration;
				}else{
					curr_punsh_duration -= Time.deltaTime;
				}
			}

			//PUNSH COOLDOWN TIMER
			if(punsh_cooldown_state){
				if(curr_punsh_cooldown <= 0.0f){
					punsh_cooldown_state = false;
				}else{
					curr_punsh_cooldown -= Time.deltaTime;
				}
			}

			//RUMBLE TIMER (WINDOWS ONLY)
			if(rumble_timer_current >= rumble_timer_max && rumble_active){
				rumble_timer_current = 0.0f;
				inputDevice.Vibrate(0.0f);
				rumble_active = false;
			}else{
				if(rumble_active){
					rumble_timer_current += Time.deltaTime;
				}
			}

            //KNOCK DOWN TIMER
            if(knock_down_timer_current > 0.0f && knocked_down)
            {
             knock_down_timer_current -= Time.deltaTime;
            }else if(knocked_down)
            {
                knocked_down = false;
            }

            if (last_nock_time >= knock_down_time_limit && curr_knocks > 0)
            {
                is_knock_limit_running = false;
                curr_knocks = 0;
            }
            else
            {
                if( game_manager.gstate == vars.game_state.playing)
                {
                    last_nock_time += Time.deltaTime;
                }
              
            }






		}
        else{
			if(game_manager.gstate == vars.game_state.spawn){
			if(inputDevice != null){
					is_playing = true;
				game_manager.player_count++;
				GameObject.Find("UI_MAIN_HOLDER").GetComponent<ingame_gui_manager>().refresh_count_ui();
				}else{
					if(pid >= 0){
						inputDevice = (InputManager.Devices.Count > pid) ? InputManager.Devices[pid] : null;
					}
				}
			}

		}
	}







	void setTrajectoryPoints(Vector3 pStartPosition , Vector3 pVelocity ){
		float velocity = Mathf.Sqrt((pVelocity.z * pVelocity.z) + (pVelocity.y * pVelocity.y));
		float angle = Mathf.Rad2Deg*(Mathf.Atan2(pVelocity.y , pVelocity.z));
		float fTime = 0;
		fTime += 0.1f;
		if(show_points > numOfTrajectoryPoints){show_points = numOfTrajectoryPoints;}
		for (int i = 0 ; i < numOfTrajectoryPoints ; i++){
			float dz = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
			float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
			Vector3 pos = new Vector3(pStartPosition.z + dz, pStartPosition.y + dy ,0 );
			trajectoryPoints[i].transform.position = pos ;
			if(i < show_points){
				trajectoryPoints[i].GetComponent<Renderer>().enabled = true;
			}else{
				trajectoryPoints[i].GetComponent<Renderer>().enabled = false;
			}
			if(i == numOfTrajectoryPoints-1){
				camera_position_corrention_point.transform.position = pos;
			}
			trajectoryPoints[i].transform.eulerAngles = new Vector3(Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude)*fTime,pVelocity.z)*Mathf.Rad2Deg,0,0);
			fTime += 0.1f;
		}
		throw_points_cleared = false;
	}
	
	void clearTrajectoryPoints() {
		if(!throw_points_cleared){
		for (int i = 0 ; i < numOfTrajectoryPoints ; i++)
		{
			trajectoryPoints[i].GetComponent<Renderer>().enabled = false;
		}
			camera_position_corrention_point.transform.position = this.transform.position;
			throw_points_cleared = true;
		}
	}
}
