using UnityEngine;
using System.Collections;

public class wicht_animation_script : MonoBehaviour {


    private Animator animator;


    public GameObject look_at_head;
    private look_at_ball look_at_head_script;

    private float timer;
    public float timer_max = 7.0f;

    Random rnd;


    public float throw_fllow_timer_max_throw = 4f;
    public float throw_fllow_timer_max_laught= 4f;
    public float throw_follow_timer = 1.0f;
    public  bool throw_fllow_disabled = false;

    private float last_wait_time;
    public void throw_in()
    {
        throw_follow_timer = throw_fllow_timer_max_throw;
        throw_fllow_disabled = true;
        last_wait_time += throw_fllow_timer_max_throw;
        look_at_head_script.hide_visible();
       animator.SetTrigger("throw_in");
      

    }
    

	// Use this for initialization
	void Start () {

        throw_fllow_disabled = false;

        look_at_head_script = look_at_head.GetComponent<look_at_ball>();
        look_at_head_script.hide_visible();
        animator = this.gameObject.GetComponent<Animator>();
        timer = timer_max;
        throw_follow_timer = throw_fllow_timer_max_laught;
        rnd = new Random();
    }
	
	// Update is called once per frame
	void Update () {
        if (game_manager.gstate == vars.game_state.playing)
        {


            if (throw_fllow_disabled )
            {
                throw_follow_timer -= Time.deltaTime;

               if(throw_follow_timer < 0.0f)
                {
                    //throw_follow_timer = last_wait_time;
                    throw_fllow_disabled = false;
                    look_at_head_script.make_visible();

                }


            }








            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {

                timer = timer_max;

                if (Random.Range(1.0f, 101.0f) >= 50.0f)
                {
                    look_at_head_script.hide_visible();
                    throw_follow_timer = throw_fllow_timer_max_laught;
                    last_wait_time += throw_fllow_timer_max_laught;
                    throw_fllow_disabled = true;
                    animator.SetTrigger("laught");

                }

            }
        }
	}
}
