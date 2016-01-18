using UnityEngine;
using System.Collections;

public class goal_positioner : MonoBehaviour
{
    public float max_change_time_r = 20.0f;

    public float curr_change_time_r = 0.0f;





    private float rndnum = 0.0f;
    public int rnd_anteil_pos_0 = 35;
    public int rnd_anteil_pos_1 = 35;
    public int rnd_anteil_pos_2 = 30;





    public GameObject goal_r;


 

    public GameObject goal_pos_r_up;
    public GameObject goal_pos_r_middle;
    public GameObject goal_pos_r_down;

    public GameObject goal_pos_l_up;
    public GameObject goal_pos_l_middle;
    public GameObject goal_pos_l_down;


    // Use this for initialization
    void Start()
    {
        goal_r.transform.position = goal_pos_r_middle.transform.position;
      

     
       
    }

    // Update is called once per frame
    void Update()
    {

        if (game_manager.gstate == vars.game_state.playing)
        {
            if (curr_change_time_r > max_change_time_r)
            {
                curr_change_time_r = 0.0f;
               
                //vhange here
                rndnum = (int)Random.Range(1.0F, 101.0F);
                if (rndnum > 50.0f)
                {


                    rndnum = (int)Random.Range(1.0F, 101.0F);
                    if (rndnum < (100 - rnd_anteil_pos_1 - rnd_anteil_pos_2))
                    {
                        goal_r.transform.position = goal_pos_r_up.transform.position;
                    }
                    else if (rndnum >= rnd_anteil_pos_1 && rndnum < (100 - rnd_anteil_pos_2))
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

                    rndnum = (int)Random.Range(1.0F, 101.0F);
                    if (rndnum < (100 - rnd_anteil_pos_1 - rnd_anteil_pos_2))
                    {
                        goal_r.transform.position = goal_pos_l_up.transform.position;
                    }
                    else if (rndnum >= rnd_anteil_pos_1 && rndnum < (100 - rnd_anteil_pos_2))
                    {
                        goal_r.transform.position = goal_pos_l_middle.transform.position;
                    }
                    else
                    {
                        goal_r.transform.position = goal_pos_l_down.transform.position;
                    }



                }



            }
            else
            {
                curr_change_time_r += Time.deltaTime;
            }









          











       



        }

    }

}
