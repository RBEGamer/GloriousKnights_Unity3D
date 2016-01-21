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
  public Transform spawn_pos;
  public vars.player_statistics player_stat;

    public GameObject dust_particle_obj;
    ParticleSystem dust_particle;
    //NEW
    //MOVE GROUND
    public Vector3 player_move_speed_ground;
    public Vector3 player_move_speed_ground_max;
    //MOVE GROUND CARRY
    public Vector3 player_move_speed_ground_carry;
    public Vector3 player_move_speed_ground_max_carry;
    public float animation_running_miltiploer = 0.25f;
    //MOVE AIR
    public Vector3 player_move_speed_air;
    public Vector3 player_move_speed_air_max;
    //MOVE AIR CARRY
    public Vector3 player_move_speed_air_carry;
    public Vector3 player_move_speed_air_max_carry;
    //JUMP 1st
    public Vector3 first_jump_force;
    public bool is_reachead_first_jump;
    //jump second
    public Vector3 second_jump_force;
    public bool is_reachead_second_jump;

    //wall collide
    public Vector3 ball_collide_vel_left;
    public Vector3 ball_collide_vel_right;
    public Vector3 player_collide_vel_left;
    public Vector3 player_collide_vel_right;

    //wall collide tierm
    public float wall_control_disable_max = 1.0f;
    private float wall_control_disable =  1.0f;
    public bool wall_control_disabled;
    public float punsh_speed = 2.0f;

  public GameObject ball_contact_collider_pbj;
  public float decarry_fly_speed = 2.0f;


  public GameObject physics_container;
  public Transform pause_pos;
  private BoxCollider my_collider;
  private BoxCollider ball_contact_collider;


  public float max_ball_contakt_disable = 1.0f;
  private float ball_contact_tierm;

  public int punsh_counter_max = 3;
  private int curr_punsh_counter = 0;

  public float knock_back_time = 3.0f;
  private float knoc_back_time_curr;
  public bool is_kocked;
  public float punsh_knockback_intense = 3.0f;

    public float punsh_disable_timer_max = 0.2f;
    float punsh_disable_timer = 0.2f;


    private float goal_control_disable_timer = 2.0f;
    public float goal_control_disable_timer_max = 2.0f;
    public bool goal_control_disabled = false;


    bool punshed_state = false;
  public void punshed(Vector3 source_pos)
  {
        if(!punshed_state) {
            curr_punsh_counter++;



            animator.SetBool("running", false);
            animator.SetTrigger("hit");

            ball_instance.decarry();
            
            
                is_kocked = true;



            rd.velocity = new Vector3(0.0f, rd.velocity.y, 0.0f);
            if (source_pos.x > physics_container.transform.position.x)
            {
                rd.AddForce(new Vector3(-punsh_knockback_intense, 0.0f, 0.0f));
                rotate_left();
            }
            else
            {
                rd.AddForce(new Vector3(punsh_knockback_intense, 0.0f, 0.0f));
                rotate_right();
            }
   
    }
  }

    public void goaled()
    {
        goal_control_disable_timer = goal_control_disable_timer_max;
        goal_control_disabled = true;

    }

    public void collide_with_left_wall()
    {
        animator.SetTrigger("collided");
        rd.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        rd.AddForce(player_collide_vel_left);
        wall_control_disabled = true;
        wall_control_disable = wall_control_disable_max;
        if(ball_instance.carried_by != player_id)
        {
            return;
        }
        ball_instance.decarry_collide(ball_collide_vel_left);

    }

    public void collide_with_right_wall()
    {
        animator.SetTrigger("collided");
        rd.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        rd.AddForce(player_collide_vel_right);
        wall_control_disabled = true;
        wall_control_disable = wall_control_disable_max;
        if (ball_instance.carried_by != player_id)
        {
            return;
        }
        ball_instance.decarry_collide(ball_collide_vel_right);

    }




    public void enabel_dance()
    {
        animator.SetBool("dance_1", true);
        rotate_front();
    }


    public void rotate_front()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }


    public void rotate_left()
    {
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
    }



    public void rotate_right()
    {
        this.transform.rotation = Quaternion.Euler(0, 270, 0);
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
        animator.SetBool("dance_1", false);
        ball_contact_collider.enabled = true;
    punsh_collider.enabled = false;
    physics_container.transform.position = spawn_pos.transform.position;
    rd.useGravity = true;
    rd.velocity = Vector3.zero;
  //  physics_container.transform.position = new Vector3(physics_container.transform.position.x,physics_container.transform.position.y, 0.0f);
        
  }

  public bool right_oriented = false;

  public void play_pickup()
  {
    animator.SetTrigger("pick");
  }

	// Use this for initialization
	void Start () {
  //  this.transform.rotation = Quaternion.Euler(0, 90, 0);
    ball_contact_collider = ball_contact_collider_pbj.GetComponent<BoxCollider>();
        dust_particle = dust_particle_obj.GetComponent<ParticleSystem>();

        dust_particle.Stop();
  //  spawn_pos = physics_container.transform.position;
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
    
    }
        set_pause_pos();
        animator.SetFloat("punsh_speed", punsh_speed);
        animator.SetBool("dance_1", false);
     //   set_pause_pos();
        rotate_front();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputDevice == null) { return; }

        if (inputDevice.MenuWasPressed && game_manager.gstate == vars.game_state.pause_menu && player_id == vars.player_id.player_1)
        {
            game_manager.gstate = vars.game_state.playing;
        }
        else if (inputDevice.MenuWasPressed && game_manager.gstate == vars.game_state.playing && player_id == vars.player_id.player_1)
        {
            game_manager.gstate = vars.game_state.pause_menu;
        }


        if (game_manager.gstate == vars.game_state.playing || game_manager.gstate == vars.game_state.spawn) {
          
        }else{
            return;
        }

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


        if (wall_control_disabled)
        {
            wall_control_disable -= Time.deltaTime;
            if(wall_control_disable < 0.0f)
            {
                wall_control_disabled = false;
                wall_control_disable = wall_control_disable_max;
            }
        }

        if (goal_control_disabled)
        {
            goal_control_disable_timer -= Time.deltaTime;

            if(goal_control_disable_timer <= 0.0f)
            {
                goal_control_disabled = false;
                goal_control_disable_timer = goal_control_disable_timer_max;
            }

        }


        //MOVE
        if ((inputDevice.LeftStick.X < -0.1f || inputDevice.DPad.Left) && !is_kocked && !wall_control_disabled && !goal_control_disabled)
        {
            right_oriented = false;
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            if (ball_instance.carried_by == player_id) { ball_instance.roate_left(); }
            if (rd.velocity.x < 0.0f) { rd.velocity = new Vector3(0.0f, rd.velocity.y, rd.velocity.z); }
            if(ball_instance.carried_by == player_id)
            {
                if (rd.velocity.x < player_move_speed_ground_max_carry.x && this.transform.position.y <= 0.1f)
                {
                    rd.AddForce(player_move_speed_ground_carry);
                }
                else if (rd.velocity.x < player_move_speed_air_max_carry.x && this.transform.position.y > 0.1f)
                {
                    rd.AddForce(player_move_speed_air_carry);
                }
            }
            else
            {
                if (rd.velocity.x < player_move_speed_ground_max.x && this.transform.position.y <= 0.1f)
                {
                    rd.AddForce(player_move_speed_ground);
                }
                else if (rd.velocity.x < player_move_speed_air_max.x && this.transform.position.y > 0.1f)
                {
                    rd.AddForce(player_move_speed_air);
                }
            }



           



        }
        else if ((inputDevice.LeftStick.X > 0.1f || inputDevice.DPad.Right) && !is_kocked && !wall_control_disabled && !goal_control_disabled)
        {
            right_oriented = true;
            this.transform.rotation = Quaternion.Euler(0, 270, 0);


            if(ball_instance.carried_by == player_id) { ball_instance.roate_right(); }
            if (rd.velocity.x > 0.0f) { rd.velocity = new Vector3(0.0f, rd.velocity.y, rd.velocity.z); }
            if (ball_instance.carried_by == player_id)
            {
                if (rd.velocity.x > -player_move_speed_ground_max_carry.x && this.transform.position.y <= 0.1f)
                {
                    rd.AddForce(-player_move_speed_ground_carry);
                }
                else if (rd.velocity.x > -player_move_speed_air_max_carry.x && this.transform.position.y > 0.1f)
                {
                    rd.AddForce(-player_move_speed_air_carry);
                }
            }
            else
            {
                if (rd.velocity.x > -player_move_speed_ground_max.x && this.transform.position.y <= 0.1f)
                {
                    rd.AddForce(-player_move_speed_ground);
                }
                else if (rd.velocity.x > -player_move_speed_air_max.x && this.transform.position.y > 0.1f)
                {
                    rd.AddForce(-player_move_speed_air);
                }
            }
              

        }


        //Playeranimation auf boden
        if (this.transform.position.y < 0.1f)
        {
            if (rd.velocity.x > 0.0f || rd.velocity.x < 0.0f)
            {
                animator.SetBool("running", true);
                dust_particle.Play();
            }
            else
            {
                animator.SetBool("running", false);
                dust_particle.Stop();
            }
            animator.SetFloat("speed", animation_running_miltiploer * rd.velocity.x);
            //player animation in der luft
        }
        else
        {
            animator.SetBool("running", false);
            dust_particle.Stop();
            //           animator.SetFloat("speed", 0.0f);
        }
    
        //PUNSH
        if (btn_triggered(inputDevice, vars.player_controls.punsh) && !btn_triggered(inputDevice, vars.player_controls.punsh, true) && ball_instance.carried_by != player_id && !is_kocked && !wall_control_disabled && !goal_control_disabled)
        {
            animator.SetTrigger("punsh");
            punsh_collider.enabled = true;
            rd.velocity = new Vector3(0.0f, rd.velocity.y, 0.0f);
            punsh_disable_timer = punsh_disable_timer_max;
      

            }


        if(punsh_disable_timer > 0.0f)
            {
                punsh_disable_timer -= Time.deltaTime;
            }
            else
            {
                punsh_disable_timer = punsh_disable_timer_max;
                punsh_collider.enabled = false;
            }


        
        //THROW
        if (btn_triggered(inputDevice, vars.player_controls.carry_down) && !btn_triggered(inputDevice, vars.player_controls.carry_down, true) && !is_kocked && !wall_control_disabled && !goal_control_disabled)
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
 

            if (btn_triggered(inputDevice, vars.player_controls.jump) && !btn_triggered(inputDevice, vars.player_controls.jump, true) && !is_kocked && !wall_control_disabled && !goal_control_disabled)
            {
                //je nach höhe entscheiden
                if (!is_reachead_first_jump && !is_reachead_second_jump)
                {
                    animator.SetTrigger("jump_up");
                    rd.AddForce(first_jump_force);
                    is_reachead_first_jump = true;
                is_reachead_second_jump = false;

                }
            else if (is_reachead_first_jump && !is_reachead_second_jump)
                {
                    rd.AddForce(second_jump_force);
                    is_reachead_second_jump = true;
                }
            }


        if (this.transform.position.y < 0.25f)
            {
            if (is_reachead_second_jump && this.transform.position.y <= 0.1f)
            {
                is_reachead_first_jump = false;
                is_reachead_second_jump = false;
            }
        }




    }



    public void reset_jump_triggers()
    {
        is_reachead_first_jump = false;
        is_reachead_second_jump = false;
    }


  public void set_pause_pos()
  {
    rd.useGravity = false;
    rd.velocity = Vector3.zero;
        reset_jump_triggers();
        reset_punsh_state();
    physics_container.transform.position = pause_pos.transform.position;
    dust_particle.Stop();
        animator.SetBool("running", false);
        animator.SetBool("falling", false);
        animator.SetBool("dance_1", false);
        //animator.SetTrigger("idle");
        rotate_front();
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
