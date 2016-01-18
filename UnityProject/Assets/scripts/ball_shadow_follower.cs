using UnityEngine;
using System.Collections;

public class ball_shadow_follower : MonoBehaviour {
    public Transform tartget_xz;
    public Transform target_height;
    public Transform pause_pos;
    public float height_offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(game_manager.gstate == vars.game_state.playing)
        {
            this.transform.position = new Vector3(tartget_xz.position.x,target_height.position.y + height_offset, tartget_xz.position.z);
        }
        else
        {
            this.transform.position = pause_pos.position;
        }
	}
}
