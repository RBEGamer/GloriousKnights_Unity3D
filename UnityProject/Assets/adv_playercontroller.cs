using UnityEngine;
using System.Collections;
using InControl;
public class adv_playercontroller : MonoBehaviour {

  public bool is_playing;
  private InputDevice inputDevice;
  private Animator animator;
  private Rigidbody rd;
  public Transform head_position;
  public vars.player_id player_id;
  public int pid;
  private ball ball_instance;

  private BoxCollider punsh_collider;
  public GameObject punsh_collider_holder;
  private Vector3 spawn_pos;
  public vars.player_statistics player_stat;

  public Vector3 player_move_speed_ground;
  public Vector3 player_move_speed_air;
  public Vector3 player_move_speed_ground_carrying;
  public Vector3 player_move_speed_air_carrying;
  public float punsh_speed = 2.0f;
  public GameObject ball_contact_collider_pbj;
  public float decarry_fly_speed = 2.0f;
  public float jump_force_one = 1.0f;
  public float jump_force_two = 1.0f;

  public GameObject physics_container;
  public Transform pause_pos;
  private BoxCollider my_collider;
  private BoxCollider ball_contact_collider;
  public bool reached_first_jump;
  public bool reaced_second_jump;

  public float max_ball_contakt_disable = 1.0f;
  private float ball_contact_tierm;

  public int punsh_counter_max = 3;
  private int curr_punsh_counter = 0;

  public float knock_back_time = 3.0f;
  public float knoc_back_time_curr;
  public bool is_kocked;
  public float punsh_knockback_intense = 3.0f;

    public float punsh_disable_timer_max = 0.2f;
    float punsh_disable_timer = 0.2f;


    bool punshed_state = false;
  public void punshed(Vector3 source_pos)
  {
        if(!punshed_state) {
            curr_punsh_counter++;
            Debug.Log("punshed");


            animator.SetBool("running", false);
            animator.SetTrigger("hit");

            ball_instance.decarry();
            
            rd.velocity = Vector3.zero;
            is_kocked = true;




            if (source_pos.x > physics_container.transform.position.x)
            {
                rd.AddForce(new Vector3(-punsh_knockback_intense, 0.0f, 0.0f));
            }
            else
            {
                rd.AddForce(new Vector3(punsh_knockback_intense, 0.0f, 0.0f));
            }
   
    }
  }

    public void reset_punsh_state()
    {
        punshed_state = false;
    }


  public void reset_velocity_and_gravity()
  {
    rd.velocity = Vector3.zero;
    rd.useGravity = false;
  }

  public void spawn()
  {
    
    ball_contact_collider.enabled = true;
    punsh_collider.enabled = false;
    physics_container.transform.position = spawn_pos;
    rd.useGravity = true;
    rd.velocity = Vector3.zero;
    physics_container.transform.position = new Vector3(physics_container.transform.position.x,physics_container.transform.position.y, 0.0f);
        
  }

  public bool right_oriented = false;

  public void play_pickup()
  {
    animator.SetTrigger("pick");
  }

	// Use this for initialization
	void Start () {
    this.transform.rotation = Quaternion.Euler(0, 90, 0);
    ball_contact_collider = ball_contact_collider_pbj.GetComponent<BoxCollider>();
    spawn_pos = physics_container.transform.position;
    this.name = player_id.ToString();
    rd = physics_container.GetComponent<Rigidbody>();
    my_collider = physics_container.GetComponent<BoxCollider>();
    animator = this.GetComponent<Animator>();
    ball_instance = GameObject.Find("ball").GetComponent<ball>();
    punsh_collider = punsh_collider_holder.GetComponent<BoxCollider>();
    punsh_collider.enabled = false;
    switch (player_id)
    {
      case vars.player_id.player_1: pid = 0; break;
      case vars.player_id.player_2: pid = 1; break;
      case vars.player_id.player_3: pid = 2; break;
      case vars.player_id.player_4: pid = 3; break;
      default:
        break;
    }
    if (pid >= 0)
    {
      inputDevice = (InputManager.Devices.Count > pid) ? InputManager.Devices[pid] : null;
    }
    if (inputDevice != null)
    {
      is_playing = true;
    }
    else
    {
      set_pause_pos();
    }

    animator.SetFloat("punsh_speed", punsh_speed);
	}
	
	// Update is called once per frame
	void Update () {
    if (inputDevice == null) { return; }
 
    if (inputDevice.MenuWasPressed && game_manager.gstate == vars.game_state.pause_menu && player_id == vars.player_id.player_1)
    {
      game_manager.gstate = vars.game_state.playing;
    }
    else if (inputDevice.MenuWasPressed && game_manager.gstate == vars.game_state.playing && player_id == vars.player_id.player_1)
    {
      game_manager.gstate = vars.game_state.pause_menu;
    }
   

    if (game_manager.gstate != vars.game_state.playing || game_manager.gstate == vars.game_state.spawn) { return; }

    if (is_kocked)
    {
      knoc_back_time_curr -= Time.deltaTime;
      if (knoc_back_time_curr <= 0.0f)
      {
        knoc_back_time_curr = knock_back_time;
        is_kocked = false;
        animator.SetTrigger("end_knock");

      }
    }


    if(player_id == vars.player_id.player_1)
        {
            Debug.Log(inputDevice.LeftStick.X);
        }


    //MOVE
    if ((inputDevice.LeftStick.X < -0.1f || inputDevice.DPad.Left) && !is_kocked)
    {
      right_oriented = false;

      if (physics_container.transform.position.y < 0.2f)
      {
        animator.SetBool("running", true);
      }
      this.transform.rotation = Quaternion.Euler(0, 90, 0);
      if (ball_instance.carried_by == player_id)
      {
        animator.SetFloat("speed", player_move_speed_ground_carrying.x);
        rd.velocity = new Vector3(player_move_speed_ground_carrying.x, rd.velocity.y, player_move_speed_ground_carrying.z);


      }
      else
      {
        animator.SetFloat("speed", player_move_speed_ground.x);
        if (rd.velocity.y > 0.1f)
        {
          rd.velocity = new Vector3(player_move_speed_air.x, rd.velocity.y, player_move_speed_air.z);
        }
        else
        {
          rd.velocity = new Vector3(player_move_speed_ground.x, rd.velocity.y, player_move_speed_ground.z);
        }
        
      }
    }
    else if ((inputDevice.LeftStick.X > 0.1f || inputDevice.DPad.Right) && !is_kocked)
    {
      right_oriented = true;
      if (physics_container.transform.position.y < 0.2f)
      {
        animator.SetBool("running", true);
      }
  this.transform.rotation = Quaternion.Euler(0, 270, 0);
      if (ball_instance.carried_by == player_id)
      {
        animator.SetFloat("speed", player_move_speed_ground_carrying.x);
        rd.velocity = new Vector3(-player_move_speed_ground_carrying.x, rd.velocity.y, player_move_speed_ground_carrying.z);
      }
      else
      {
        animator.SetFloat("speed", player_move_speed_ground.x);
        if (rd.velocity.y > 0.1f)
        {
          rd.velocity = new Vector3(-player_move_speed_air.x, rd.velocity.y, player_move_speed_air.z);
        }
        else
        {
          rd.velocity = new Vector3(-player_move_speed_ground.x, rd.velocity.y, player_move_speed_ground.z);
        }

      }
     }
    else
    {
      animator.SetBool("running", false);
      rd.velocity = new Vector3(0.0f, rd.velocity.y, 0.0f);
    }

    //PUNSH
    if (btn_triggered(inputDevice, vars.player_controls.punsh) && !btn_triggered(inputDevice, vars.player_controls.punsh, true) && ball_instance.carried_by != player_id && !is_kocked)
    {
      punsh_collider.enabled = true;
      rd.velocity = Vector3.zero;
       punsh_disable_timer = punsh_disable_timer_max;
      animator.SetTrigger("punsh");
    }


    if(punsh_disable_timer > 0.0f)
        {
            punsh_disable_timer -= Time.deltaTime;
        }
        else
        {
            punsh_collider.enabled = false;
        }

    //THROW
    if (btn_triggered(inputDevice, vars.player_controls.carry_down) && !btn_triggered(inputDevice, vars.player_controls.carry_down, true) && !is_kocked)
    {
      if (ball_instance.carried_by == player_id)
      {
       
        if (right_oriented)
        {
          ball_contact_collider.enabled = false;
          ball_contact_tierm = max_ball_contakt_disable;
          ball_instance.decarry_fly(player_id, decarry_fly_speed);
        }
        else
        {
          ball_contact_collider.enabled = false;
          ball_contact_tierm = max_ball_contakt_disable;
          ball_instance.decarry_fly(player_id, -(decarry_fly_speed));
        }
        animator.SetTrigger("throw");
     
      }
    }

    //ball contact timer
    if (!ball_contact_collider.enabled && ball_contact_tierm >= 0.0f)
    {
      ball_contact_tierm -= Time.deltaTime;
     
    }
    if (!ball_contact_collider.enabled && ball_contact_tierm <= 0.0f)
    {
      ball_contact_collider.enabled = true;
    }


    //JUMP
    if (this.transform.position.y <= 0.25f)
    {
      if (reached_first_jump)
      {
       // animator.SetBool("falling", false);
      }
  
      reached_first_jump = false;
      reaced_second_jump = false;
    }

    if (btn_triggered(inputDevice, vars.player_controls.jump) && !btn_triggered(inputDevice, vars.player_controls.jump, true) && !is_kocked)
    {
            //je nach höhe entscheiden
            animator.SetTrigger("jump_up");
            if (!reached_first_jump)
      {
        Debug.Log("jump second");
        reached_first_jump = true;
        rd.velocity = new Vector3(rd.velocity.x, jump_force_two, 0.0f);
        //animator.SetBool("falling", true);
      }
      else if (this.transform.position.y <= 0.25f)
      {
        animator.SetTrigger("jump_up");
     rd.velocity = new Vector3(rd.velocity.x, jump_force_one, 0.0f);
        Debug.Log("jzmp fist");
        reached_first_jump = false;
        reaced_second_jump = false;
        
      //  animator.SetBool("falling", true);
      }

    }
    

	}






  public void set_pause_pos()
  {
    rd.useGravity = false;
    rd.velocity = Vector3.zero;
    physics_container.transform.position = pause_pos.transform.position;
  }







  public bool btn_triggered(InputDevice idc, vars.player_controls pcts, bool last_state = false)
  {
    if (!last_state)
    {
      switch (pcts)
      {
        case vars.player_controls.punsh:
          return idc.GetControlByName(vars.player_control_punsh_trigger_name);

        case vars.player_controls.jump:
          return idc.GetControlByName(vars.player_control_jump_trigger_name);

        case vars.player_controls.menu:
          return idc.GetControlByName(vars.player_control_menu_trigger_name);

        case vars.player_controls.carry_up:
          return idc.GetControlByName(vars.player_control_carry_up_trigger_name);

        case vars.player_controls.carry_down:
          return idc.GetControlByName(vars.player_control_carry_down_trigger_name);

        default:
          return false;
      }
    }
    else
    {
      switch (pcts)
      {
        case vars.player_controls.punsh:
          return idc.GetControlByName(vars.player_control_punsh_trigger_name).LastState;

        case vars.player_controls.jump:
          return idc.GetControlByName(vars.player_control_jump_trigger_name).LastState;

        case vars.player_controls.menu:
          return idc.GetControlByName(vars.player_control_menu_trigger_name).LastState;

        case vars.player_controls.carry_up:
          return idc.GetControlByName(vars.player_control_carry_up_trigger_name).LastState;

        case vars.player_controls.carry_down:
          return idc.GetControlByName(vars.player_control_carry_down_trigger_name).LastState;

        default:
          return false;
      }
    }
  }
}
