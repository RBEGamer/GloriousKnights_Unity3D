using UnityEngine;
using System.Collections;

public class goal : MonoBehaviour {


    public enum colors
    {
        neutral, p1c, p2c, p3c, p4c

    }


    public colors goal_color;
    public float color_timer;
    public float color_timer_max = 10.0f;

	public GameObject explosion_holder;
	private Vector3 pp;
	private bool in_pp;
	private ParticleSystem ps;
	private AudioSource asource;

  public GameObject hut_anim_holder;
  public GameObject pumkin_anim_holder;
  private Animator hut_animator;
  private Animator pumking_animator;


	int points = 1;
	void OnCollisionEnter(Collision other) {

		if(other.gameObject.tag.Contains("ball")) {
		
  


            if(other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_1 && (goal_color == colors.neutral || goal_color == colors.p1c)){
            }


            else if (other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_2 && (goal_color == colors.neutral || goal_color == colors.p2c)){
            }
            

            else if (other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_3 && (goal_color == colors.neutral || goal_color == colors.p3c)){
            }


            else if (other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_4 && (goal_color == colors.neutral || goal_color == colors.p4c)){
            }else
            {
                other.collider.gameObject.GetComponent<ball>().last_contact = vars.player_id.none;
             //   other.collider.GetComponent<ball>().decarry();
                return;
            }

            if (other.collider.gameObject.GetComponent<ball>().is_carrying()){
				points = 1;
			}else{
				points = 2;
			}


            explosion_holder.transform.position = other.contacts[0].point;
            ps.Play();

            in_pp = false;

            if (other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_1) { game_manager.score_player_1 += points; asource.Play(); hut_animator.SetTrigger("goal"); pumking_animator.SetTrigger("goal"); }
      else if (other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_2) { game_manager.score_player_2 += points; asource.Play(); hut_animator.SetTrigger("goal"); pumking_animator.SetTrigger("goal"); }
      else if (other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_3) { game_manager.score_player_3 += points; asource.Play(); hut_animator.SetTrigger("goal"); pumking_animator.SetTrigger("goal"); }
      else if (other.collider.gameObject.GetComponent<ball>().last_contact == vars.player_id.player_4) { game_manager.score_player_4 += points; asource.Play(); hut_animator.SetTrigger("goal"); pumking_animator.SetTrigger("goal"); }
			else{other.collider.gameObject.GetComponent<ball>().last_contact = vars.player_id.none;}
			other.collider.gameObject.GetComponent<ball>().spawn(); //spawn ball
		}

	}


    void update_color()
    {
        switch (goal_color)
        {
            case colors.neutral:
                this.gameObject.GetComponent<Renderer>().material.color = Color.gray;
                break;
            case colors.p1c:
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                break;
            case colors.p2c:
                this.gameObject.GetComponent<Renderer>().material.color = Color.green;
                break;
            case colors.p3c:
                this.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case colors.p4c:
                this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        goal_color = colors.neutral;
        update_color();





    hut_animator = hut_anim_holder.GetComponent<Animator>();
    pumking_animator = pumkin_anim_holder.GetComponent<Animator>();

		pp = GameObject.Find("pause_position").transform.position;
		ps = explosion_holder.GetComponent<ParticleSystem>();
		asource = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {


        if(color_timer > 0.0f)
        {
            color_timer -= Time.deltaTime;
        }
        else
        {
            color_timer = color_timer_max;

            

         


            switch (goal_color)
            {
                case colors.neutral:
                    goal_color = colors.p1c;
                    break;
                case colors.p1c:
                    goal_color = colors.p2c;
                    break;
                case colors.p2c:
                    goal_color = colors.p3c;
                    break;
                case colors.p3c:
                    goal_color = colors.p4c;
                    break;
                case colors.p4c:
                    goal_color = colors.neutral;
                    break;
                default:
                    break;
            }
            update_color();

        }




		if(!in_pp){
			if(!ps.isPlaying){
			explosion_holder.transform.position = pp;
			in_pp = true;
			}
		}
	}
}
