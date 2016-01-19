using UnityEngine;
using System.Collections;

public class ball_shadow : MonoBehaviour {
  public Transform following_source;
  public float y_height;
  public Transform target;
  public bool visible;
  public Transform pause_pos;
  public bool in_pause_pos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    if (game_manager.gstate == vars.game_state.playing || game_manager.gstate == vars.game_state.spawn)
    {
      target.transform.position = new Vector3(following_source.transform.position.x, y_height, following_source.transform.position.z);
      if (!visible)
      {
        visible = true;
        in_pause_pos = false;
      }
    }
    else
    {
      if (visible)
      {
        visible = false;
        
      }
    }


    if (!visible && !in_pause_pos)
    {
      target.transform.position = pause_pos.transform.position;
      in_pause_pos = true;
    }


	}
}
