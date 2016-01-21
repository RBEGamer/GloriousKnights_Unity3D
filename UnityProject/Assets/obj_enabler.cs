using UnityEngine;
using System.Collections;

public class obj_enabler : MonoBehaviour {


    public GameObject[] obj_to_enable;




	// Use this for initialization
	void Awake () {
        for (int i = 0; i < obj_to_enable.Length; i++)
        {
            if (obj_to_enable[i].activeSelf) { continue; }
            obj_to_enable[i].SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
