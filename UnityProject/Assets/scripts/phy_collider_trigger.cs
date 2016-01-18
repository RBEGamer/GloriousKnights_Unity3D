using UnityEngine;
using System.Collections;

public class phy_collider_trigger : MonoBehaviour {
  public GameObject adv_script_holder;
  private adv_playercontroller adv;
	// Use this for initialization
	void Start () {
    adv = this.adv_script_holder.GetComponent<adv_playercontroller>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnTriggerEnter(Collider other)
  {

    if (other.gameObject.tag == "player_1_punsh")
    {
      adv.punshed(GameObject.Find(vars.player_id.player_1.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
    }

    if (other.gameObject.tag == "player_2_punsh")
    {
      adv.punshed(GameObject.Find(vars.player_id.player_2.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
    }


    if (other.gameObject.tag == "player_3_punsh")
    {
      adv.punshed(GameObject.Find(vars.player_id.player_3.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
    }

    if (other.gameObject.tag == "player_4_punsh")
    {
      adv.punshed(GameObject.Find(vars.player_id.player_4.ToString()).GetComponent<adv_playercontroller>().physics_container.transform.position);
    }
 
  }

    void OnTriggerExit(Collider other)
    {
       
       
            adv.reset_punsh_state();
        

        
 
    }
}
