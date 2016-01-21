using UnityEngine;
using System.Collections;


//COLLECT  playing players by is_playing with the player tag
public class ball : MonoBehaviour {
  public GameObject skull_ob;
  private Animator animator;
	private AudioSource asource;
	public vars.player_id last_contact;
	public int hits = 0;
	public Vector3 ball_carry_offset = new Vector3(0.0f,1.2f,0.0f);
	public vars.player_id carried_by;
	public vars.player_id carried_by_last_frame;
	private adv_playercontroller p1pc;
  private adv_playercontroller p2pc;
  private adv_playercontroller p3pc;
  private adv_playercontroller p4pc;

  public GameObject shader_holder;

  public GameObject player1_scrpit_obj;
  public GameObject player2_scrpit_obj;
  public GameObject player3_scrpit_obj;
  public GameObject player4_scrpit_obj;

    public GameObject einwurfball_obj;
    public ParticleSystem particles;
    public GameObject particle_holder;

    public GameObject ms_skull;
    public  Material ms_skull_mat;

	private Rigidbody rd;
	public Vector3 punsh_velocity_add = new Vector3(500,200,0);
	public Vector3 decarray_velocity_add = new Vector3(0.0f,50.0f,0.0f);
  Quaternion init_rot;

    public float decarry_timer_max = 0.2f;
    public float decarry_timer = 0.2f;

    public GameObject wicht_gameobject;

    public bool spawned;

    // Use this for initialization
    void Start () {
     //   spawned = false;
    //  ms_skull_mat = ms_skull.GetComponent<Material>();
    animator = skull_ob.GetComponent<Animator>();
		asource = this.GetComponent<AudioSource>();
		this.name = "ball";
		this.transform.position = GameObject.Find("pause_position").transform.position;
		rd = this.GetComponent<Rigidbody>();
    init_rot = rd.rotation;
		hits = 0;
		carried_by = vars.player_id.none;
		carried_by_last_frame = vars.player_id.none;
        particles = particle_holder.GetComponent<ParticleSystem>();
        p1pc = player1_scrpit_obj.GetComponent<adv_playercontroller>();
		p2pc = player2_scrpit_obj.GetComponent<adv_playercontroller>();
		p3pc = player3_scrpit_obj.GetComponent<adv_playercontroller>();
    p4pc = player4_scrpit_obj.GetComponent<adv_playercontroller>();
        rotate_font();
    set_to_pause_pos();
	}


	public void set_to_pause_pos(){
		this.transform.position = GameObject.Find("pause_position").transform.position;
		rd.useGravity = false;
		carried_by = vars.player_id.none;
		carried_by_last_frame = vars.player_id.none;
		last_contact = vars.player_id.none;
	}

	public void carry(vars.player_id pid){
		
            Debug.Log(pid.ToString());
            carried_by = pid;
			rd.useGravity = false;
      rd.rotation = init_rot;
			last_contact = pid;
		
	}


    public void rotate_font()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void roate_left()
    {
        this.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void roate_right()
    {
        this.transform.rotation = Quaternion.Euler(0, 270, 0);
    }

	public void decarry(){
       // this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z);
        rd.useGravity = true;

        decarry_timer = decarry_timer_max;
        this.GetComponent<SphereCollider>().enabled = false;
        

        carried_by = vars.player_id.none;
        rd.useGravity = true;
			rd.AddForce(decarray_velocity_add);
            play_hit_animation();
		
	}

    public void decarry_collide(Vector3 vel)
    {
        carried_by = vars.player_id.none;
        last_contact = vars.player_id.none;

        wall_collide_colluder_disable_timer = wall_collide_collider_disable_timer_max;
        wall_collide_collider_disabled = true;


        add_force(vel);

        play_hit_animation();
   
       

        //this.GetComponent<SphereCollider>().enabled = false;



                    

     

    }

    public void decarry_fly(vars.player_id pid, float _decarray_velocity_add){
    rd.useGravity = false;
        Debug.Log("decarry fly");
           
            carried_by = vars.player_id.none;
       // last_contact = pid;
      rd.velocity =  new Vector3(-_decarray_velocity_add, 0.0f,0.0f) ;
      play_hit_animation();
		
	}


	public bool is_carrying()
	{
		if(carried_by == vars.player_id.none){
			return false;

		}
		return true;
	}




    private float spawn_delay_time = 2.0f;
    public float spawn_delay_time_max = 2.0f;


    public void spawn()
    {
        Debug.Log("spawn");
        set_to_pause_pos();
        spawned = false;
        spawn_delay_time = spawn_delay_time_max;
        wicht_gameobject.GetComponent<wicht_animation_script>().throw_in();
        einwurfball_obj.GetComponent<Animator>().SetTrigger("einwurf");
        // spawn_real();
    }

	public void spawn_real(){

      


        Debug.Log("spawn-real");
        rotate_font();
        last_contact = vars.player_id.none;
        carried_by = vars.player_id.none;
        rd.useGravity = true;
        this.rd.velocity = Vector3.zero;

        float rndnum = (int)Random.Range(1.0F, 101.0F);

        if (rndnum < (100 - 33.0f - 33.0f))
        {
            this.transform.position = GameObject.Find("ball_spawn_pos_1").gameObject.transform.position;
        }
        else if (rndnum >= 33.0f && rndnum < (100 - 33.0f))
        {
            this.transform.position = GameObject.Find("ball_spawn_pos_2").gameObject.transform.position;
        }
        else
        {
            this.transform.position = GameObject.Find("ball_spawn_pos_3").gameObject.transform.position;
        }


      
    }
  

    public void enable_gravity()
    {
        rd.useGravity = true;
      //  rd.velocity = new Vector3(0.0f, rd.velocity.y, 0.0f);
        play_hit_animation();
    }



    public float wall_collide_collider_disable_timer_max = 1.0f;
    private float wall_collide_colluder_disable_timer = 1.0f;
    private bool wall_collide_collider_disabled = false;

    public void add_force(Vector3 vel)
    {
        Debug.Log("add force :" + vel);
        carried_by = vars.player_id.none;
        last_contact = vars.player_id.none;
        rd.velocity = vel;
        rd.useGravity = true;

        //rd.AddForce(vel);

        Debug.Log(rd.velocity);
    }




	void OnCollisionEnter(Collision other) {
     


        if (other.collider.gameObject.tag.Contains("wall")) {
      rd.useGravity = true;
            play_hit_animation();
        }

   
	}

  public void play_hit_animation()
  {
    asource.volume = 1.0f / game_manager.volume;
    asource.Play();
        rotate_font();
        // shader_holder.GetComponent<Material>().SetFloat("EmitConst_1", 3.0f);
        animator.SetTrigger("hit");
        // shader_holder.GetComponent<Material>().SetFloat("EmitConst_1", 0.0f);


  }

	void OnTriggerEnter(Collider other) {
    Debug.Log("ball trigger enter by" + other.transform.parent.gameObject.tag);

    if (other.transform.parent.gameObject.tag == "Player" && !wall_collide_collider_disabled && game_manager.gstate == vars.game_state.playing)
    {
            //NUR WENN NICHT GECARRIED WIRD DANN WECHSELN
            if (!is_carrying())
            {
                last_contact = other.transform.parent.gameObject.GetComponent<adv_playercontroller>().player_id;
            }
  


     if(carried_by == vars.player_id.none  ){
       if (!other.transform.parent.gameObject.GetComponent<adv_playercontroller>().is_kocked && !is_carrying())
       {
         other.transform.parent.gameObject.GetComponent<adv_playercontroller>().play_pickup(); //play pickup animation
         carry(other.transform.parent.gameObject.GetComponent<adv_playercontroller>().player_id);
       }

     }
      
    
    }

	}




    public void update_particle_color()
    {
        switch (last_contact)
        {
            case vars.player_id.none:
                particles.startColor = Color.black;
                ms_skull_mat.SetColor("_EmitColor", Color.black);
                break;
            case vars.player_id.player_1:
                particles.startColor = Color.blue;
                ms_skull_mat.SetColor("_EmitColor", Color.blue);
                break;
            case vars.player_id.player_2:
                particles.startColor = Color.green;
                ms_skull_mat.SetColor("_EmitColor", Color.green);
                break;
            case vars.player_id.player_3:
                particles.startColor = Color.red;
                ms_skull_mat.SetColor("_EmitColor", Color.red);
                break;
            case vars.player_id.player_4:
                particles.startColor = Color.yellow;
                ms_skull_mat.SetColor("_EmitColor", Color.yellow);
                break;
            default:
                break;
        }
    


    }


	// Update is called once per frame
	void FixedUpdate () {
    if (this.transform.position.y < 0.0f)
    {
         
            spawn_real();
        }
        update_particle_color();
    if (game_manager.gstate == vars.game_state.playing)
        {
            if (decarry_timer > 0.0f)
            {
                decarry_timer -= Time.deltaTime;
            }
            else
            {
                this.GetComponent<SphereCollider>().enabled = true;
            }



            if(!spawned && spawn_delay_time > 0.0f)
            {
                spawn_delay_time -= Time.deltaTime;

                if (spawn_delay_time <= 0.0f)
                {
                    spawn_delay_time = spawn_delay_time_max;
                    spawned = true;
                    Debug.Log("213");
                    spawn_real();
                }
            }
       

            if(wall_collide_collider_disabled && wall_collide_colluder_disable_timer > 0.0f)
            {
                wall_collide_colluder_disable_timer -= Time.deltaTime;

                if(wall_collide_colluder_disable_timer <= 0.0f)
                {
                    wall_collide_colluder_disable_timer = wall_collide_collider_disable_timer_max;
                    wall_collide_collider_disabled = false;
                }
            }


        }





        if (carried_by != vars.player_id.none){
            rd.useGravity = false;
			switch (carried_by) {
			case vars.player_id.player_1:if(p1pc != null){this.transform.position = p1pc.gameObject.transform.position + ball_carry_offset;}break;
			case vars.player_id.player_2:if(p1pc != null){this.transform.position = p2pc.gameObject.transform.position + ball_carry_offset;}break;
			case vars.player_id.player_3:if(p1pc != null){this.transform.position = p3pc.gameObject.transform.position + ball_carry_offset;}break;
			case vars.player_id.player_4:if(p1pc != null){this.transform.position = p4pc.gameObject.transform.position + ball_carry_offset;}break;
			default:
			break;
			}
        }
        else
        {
          //  rd.useGravity = true;
        }
	}
}
