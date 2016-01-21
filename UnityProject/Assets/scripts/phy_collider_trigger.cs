using UnityEngine;
using System.Collections;

public class phy_collider_trigger : MonoBehaviour {
  public GameObject adv_script_holder;
  private adv_playercontroller adv;
    private Rigidbody rd;
	// Use this for initialization
	void Start () {
        rd = this.GetComponent<Rigidbody>();

    adv = this.adv_script_holder.GetComponent<adv_playercontroller>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider other)
  {
        if(other.gameObject.tag == "player") { return; }
        if (other.gameObject.tag == "ground") {
            adv.reset_jump_triggers();

        }






        Debug.Log("Ich bin Player " + adv.player_id.ToString() + " coliide with " + other.gameObject.tag);



        if(other.gameObject.name == "wall_left_trigger")
        {
            adv.collide_with_left_wall();
        }

        if (other.gameObject.name == "wall_right_trigger")
        {
            adv.collide_with_right_wall();
        }

        if (other.gameObject.tag.Contains("punsh"))
        {

            Debug.Log("Ich bin Player " + adv.player_id.ToString() + " und ich wurde von player " + other.gameObject.tag + " geschlagen!");


            if (other.gameObject.tag == "player_1_punsh")
            {
                if (adv.player_id != vars.player_id.player_1)
                {
                    adv.punshed(GameObject.Find(vars.player_id.player_1.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
                }
            }

            if (other.gameObject.tag == "player_2_punsh")
            {
                if (adv.player_id != vars.player_id.player_2)
                {
                    adv.punshed(GameObject.Find(vars.player_id.player_2.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
                }
            }


            if (other.gameObject.tag == "player_3_punsh")
            {
                if (adv.player_id != vars.player_id.player_3)
                {
                    adv.punshed(GameObject.Find(vars.player_id.player_3.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
                }
            }

            if (other.gameObject.tag == "player_4_punsh")
            {
                if (adv.player_id != vars.player_id.player_4)
                {
                    adv.punshed(GameObject.Find(vars.player_id.player_4.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
                }
            }
        }
  }

    void OnTriggerExit(Collider other)
    {
       
       
            adv.reset_punsh_state();
        

        
 
    }
}
