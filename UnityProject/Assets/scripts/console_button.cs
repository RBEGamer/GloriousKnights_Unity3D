using UnityEngine;
using System.Collections;

public class console_button : MonoBehaviour {


  public GameObject btn_image_normal;
  public GameObject btn_image_pressed;
  public GameObject btn_image_selected;

  private AudioSource asource;



  public void set_selected()
  {
    asource.Play();
    btn_image_selected.SetActive(true);
    btn_image_pressed.SetActive(false);
    btn_image_normal.SetActive(false);
  }


  public void set_normal()
  {
    btn_image_selected.SetActive(false);
    btn_image_pressed.SetActive(false);
    btn_image_normal.SetActive(true);
  }

  public void set_pressed()
  {
    btn_image_selected.SetActive(false);
    btn_image_pressed.SetActive(true);
    btn_image_normal.SetActive(false);
  }
	// Use this for initialization
	void Awake () {
    asource = this.GetComponent<AudioSource>();
    set_normal();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
