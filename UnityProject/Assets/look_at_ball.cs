using UnityEngine;
using System.Collections;

public class look_at_ball : MonoBehaviour {



    public  GameObject target;
    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public GameObject render_obj;
    public GameObject original_obj;

    public Transform default_view_point;
    public  bool look_at_enabled;
    // Use this for initialization
    void Start () {
        hide_visible();
        look_at_enabled = false;
    }



    public void make_visible()
    {

        render_obj.GetComponent<MeshRenderer>().enabled = true;

        original_obj.GetComponent<SkinnedMeshRenderer>().enabled = false;

        look_at_enabled = true;


    }
	

    public void hide_visible()
    {

        render_obj.GetComponent<MeshRenderer>().enabled = false;

        original_obj.GetComponent<SkinnedMeshRenderer>().enabled = true;

        look_at_enabled = false;
    }
	// Update is called once per frame
	void Update () {



        if (look_at_enabled) {
            targetPoint = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z) - transform.position;
        } else
        {
            targetPoint = default_view_point.transform.position - transform.position;
        }
         
            targetRotation = Quaternion.LookRotation(-targetPoint, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);



        
       




	}
}
