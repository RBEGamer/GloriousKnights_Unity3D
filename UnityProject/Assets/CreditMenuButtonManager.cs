using UnityEngine;
using System.Collections;
using InControl;
public class CreditMenuButtonManager : MonoBehaviour {

  private InputDevice inputDevice;
  console_button start_btn;
  public GameObject exit_game_button;
  private AudioSource asource;
	// Use this for initialization
	void Start () {
    start_btn = exit_game_button.GetComponent<console_button>();
    asource = this.GetComponent<AudioSource>();
    inputDevice = (InputManager.Devices.Count > 0) ? InputManager.Devices[0] : null;
    start_btn.set_selected();
	}

    public void make_visible()
    {

    
        start_btn.set_selected();
    }


    // Update is called once per frame
    void Update () {
    if (inputDevice != null)
    {
 

      if (inputDevice.Action1.WasPressed)
      {
        game_manager.gstate = vars.game_state.main_menu;
      }
    }
	}
}
