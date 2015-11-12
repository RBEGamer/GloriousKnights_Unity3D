using UnityEngine;
using System.Collections;

public class goal_positioner : MonoBehaviour
{
    public float max_change_time_r = 20.0f;
    public float max_change_time_l = 20.0f;
    public float curr_change_time_r = 0.0f;
    public float curr_change_time_l = 10.0f;

    public float max_scale_time_r = 10.0f;
    public float max_scale_time_l = 10.0f;
    public float curr_scale_time_r = 0.0f;
    public float curr_scale_time_l = 0.0f;
    public bool scale_timer_en_r = false;
    public bool scale_timer_en_l = false;

    private float rndnum = 0.0f;
    public int rnd_anteil_pos_0 = 35;
    public int rnd_anteil_pos_1 = 35;
    public int rnd_anteil_pos_2 = 30;




    public Vector3 max_scale = new Vector3(0.5f, 1.0f, 1.15f);
    public Vector3 med_scale = new Vector3(0.5f, 0.76f, 0.88f);
    public Vector3 min_scale = new Vector3(0.5f, 0.53f, 0.61f);


    public GameObject goal_r;
    public GameObject goal_l;

    public GameObject goal_pos_l_up;
    public GameObject goal_pos_l_middle;
    public GameObject goal_pos_l_down;

    public GameObject goal_pos_r_up;
    public GameObject goal_pos_r_middle;
    public GameObject goal_pos_r_down;
    // Use this for initialization
    void Start ()
    {
        goal_r.transform.position = goal_pos_r_middle.transform.position;
        goal_l.transform.position = goal_pos_l_middle.transform.position;

        goal_r.transform.localScale = med_scale;
        goal_l.transform.localScale = med_scale;
    }
	
	// Update is called once per frame
    void Update()
    {

        if (game_manager.gstate == vars.game_state.playing)
        {
            if (curr_change_time_r > max_change_time_r)
            {
                curr_change_time_r = 0.0f;
                scale_timer_en_r = true;
                //vhange here
                rndnum = (int)Random.Range(1.0F, 101.0F);
                if ( rndnum < (100 - rnd_anteil_pos_1 - rnd_anteil_pos_2))
                {
                    goal_r.transform.position = goal_pos_r_up.transform.position;
                }
                else if(rndnum >= rnd_anteil_pos_1 && rndnum < (100 - rnd_anteil_pos_2))
                {
                    goal_r.transform.position = goal_pos_r_middle.transform.position;
                }
                else
                {
                    goal_r.transform.position = goal_pos_r_down.transform.position;
                }
            }
            else
            {
                curr_change_time_r += Time.deltaTime;
            }









            if (curr_change_time_l > max_change_time_l)
            {
                curr_change_time_l = 0.0f;
                scale_timer_en_l = true;
                //vhange here
                rndnum = (int)Random.Range(1.0F, 101.0F);
                if ( rndnum < (100 - rnd_anteil_pos_1 - rnd_anteil_pos_2))
                {
                    goal_l.transform.position = goal_pos_l_up.transform.position;
                }
                else if (rndnum >= rnd_anteil_pos_1 && rndnum < (100 - rnd_anteil_pos_2))
                {
                    goal_l.transform.position = goal_pos_l_middle.transform.position;
                }
                else
                {
                    goal_l.transform.position = goal_pos_l_down.transform.position;
                }
            }
            else
            {
                curr_change_time_l += Time.deltaTime;
            }






            if (scale_timer_en_l)
            {
                if (curr_scale_time_l >= max_scale_time_l)
                {
                    curr_scale_time_l = 0.0f;
                    scale_timer_en_l = false;

                    rndnum = (int)Random.Range(1.0F, 101.0F);
                    if ( rndnum < (100 - rnd_anteil_pos_1 - rnd_anteil_pos_2))
                    {
                        goal_l.transform.localScale = min_scale;
                    }
                    else if (rndnum >= rnd_anteil_pos_1 && rndnum < (100 - rnd_anteil_pos_2))
                    {
                        goal_l.transform.localScale = med_scale;
                    }
                    else
                    {
                        goal_l.transform.localScale = max_scale;
                    }

                }
                else
                {
                    curr_scale_time_l += Time.deltaTime;
                }
            }





            if (scale_timer_en_r)
            {
                if (curr_scale_time_r >= max_scale_time_r)
                {
                    curr_scale_time_r = 0.0f;
                    scale_timer_en_r = false;

                    rndnum = (int)Random.Range(1.0F, 101.0F);
                    if ( rndnum < (100 - rnd_anteil_pos_1 - rnd_anteil_pos_2))
                    {
                        goal_r.transform.localScale = min_scale;
                    }
                    else if (rndnum >= rnd_anteil_pos_1 && rndnum < (100 - rnd_anteil_pos_2))
                    {
                        goal_r.transform.localScale = med_scale;
                    }
                    else
                    {
                        goal_r.transform.localScale = max_scale;
                    }

                }
                else
                {
                    curr_scale_time_r += Time.deltaTime;
                }
            }



        }

    }

}
