using UnityEngine;
using System.Collections;

public class wicht_animation_script : MonoBehaviour {


    private Animator animator;


    public float timer;
    public float timer_max = 7;

    Random rnd;


	// Use this for initialization
	void Start () {
        animator = this.gameObject.GetComponent<Animator>();
        timer = timer_max;
        rnd = new Random();
    }
	
	// Update is called once per frame
	void Update () {
        if (game_manager.gstate == vars.game_state.playing)
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }
            else
            {

                timer = timer_max;

                if (Random.Range(1.0f, 101.0f) >= 50.0f)
                {
                    animator.SetTrigger("laught");
                }

            }
        }
	}
}
