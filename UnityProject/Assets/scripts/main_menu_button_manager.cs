using UnityEngine;
using System.Collections;
using InControl;
public class main_menu_button_manager : MonoBehaviour {

  public enum selected_btn
  {
    start, exit, option, credits
  }
  private AudioSource asource;
  public GameObject start_game_button;
  public GameObject exit_game_button;
  public GameObject options_game_button;
  public GameObject credits_game_button;

  private InputDevice inputDevice;


  console_button start_btn;
  console_button exit_btn;
  console_button option_btn;
  console_button credit_btn;

  public GameObject main_menu_canvas;

  public selected_btn curr_selected;
	// Use this for initialization
	void Start () {
    asource = this.GetComponent<AudioSource>();
    inputDevice = (InputManager.Devices.Count > 0) ? InputManager.Devices[0] : null;
    //game_manager.gstate = vars.game_state.main_menu;
    curr_selected = selected_btn.start;
    start_btn = start_game_button.GetComponent<console_button>();
    exit_btn = exit_game_button.GetComponent<console_button>();
    option_btn = options_game_button.GetComponent<console_button>();
    credit_btn = credits_game_button.GetComponent<console_button>();
    set_btn_state();
	}


    public void make_visible()
    {

        set_btn_state();

    }


    // Update is called once per frame
    void Update () {
    if (inputDevice != null)
    {
      if (inputDevice.DPadDown.WasPressed  || (inputDevice.LeftStick.Down.State && !inputDevice.LeftStick.Down.LastState))
      {
        select_next_btn();
      }

      if (inputDevice.DPadUp.WasPressed || (inputDevice.LeftStick.Up.State && !inputDevice.LeftStick.Up.LastState))
      {
        select_prev_btn();
      }

      if (inputDevice.Action1.WasPressed)
      {
        click_btn();
      }
    }
	}



  public void select_next_btn()
  {
    switch (curr_selected)
    {
      case selected_btn.start:
        curr_selected = selected_btn.option;
        break;
      case selected_btn.exit:
        curr_selected = selected_btn.start;
        break;
      case selected_btn.option:
        curr_selected = selected_btn.credits;
        break;
      case selected_btn.credits:
        curr_selected = selected_btn.exit;
        break;
      default:
        break;
    }
    set_btn_state();
  }


  public void select_prev_btn()
  {
    switch (curr_selected)
    {
      case selected_btn.start:
        curr_selected = selected_btn.exit;
        break;
      case selected_btn.exit:
        curr_selected = selected_btn.credits;
        break;
      case selected_btn.option:
        curr_selected = selected_btn.start;
        break;
      case selected_btn.credits:
        curr_selected = selected_btn.option;
        break;
      default:
        break;
    }
    set_btn_state();
  }

  public void click_btn()
  {
    asource.Play();
    switch (curr_selected)
    {
      case selected_btn.start:
        start_btn.set_pressed();

        game_manager.gstate = vars.game_state.game_settings;
        //main_menu_canvas.SetActive(false);
        break;
      case selected_btn.exit:
        exit_btn.set_pressed();

        Application.Quit();
        break;
      case selected_btn.option:
        option_btn.set_pressed();
        main_menu_canvas.SetActive(false);
        game_manager.gstate = vars.game_state.settings;
                option_btn.set_normal();
                break;
      case selected_btn.credits:
        credit_btn.set_pressed();
        main_menu_canvas.SetActive(false);
        game_manager.gstate = vars.game_state.credits;
                credit_btn.set_normal();
                break;
      default:
        break;
    }
  }

  public void set_btn_state()
  {
    switch (curr_selected)
    {
      case selected_btn.start:
        start_btn.set_selected();
        exit_btn.set_normal();
        option_btn.set_normal();
        credit_btn.set_normal();
        break;
      case selected_btn.exit:
        start_btn.set_normal();
        exit_btn.set_selected();
        option_btn.set_normal();
        credit_btn.set_normal();
        break;
      case selected_btn.option:
        start_btn.set_normal();
        exit_btn.set_normal();
        option_btn.set_selected();
        credit_btn.set_normal();
        break;
      case selected_btn.credits:
        start_btn.set_normal();
        exit_btn.set_normal();
        option_btn.set_normal();
        credit_btn.set_selected();
        break;
      default:
        curr_selected = selected_btn.start;
        set_btn_state();
        break;
    }
  }
}
