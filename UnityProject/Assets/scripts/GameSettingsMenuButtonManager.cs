using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
public class GameSettingsMenuButtonManager : MonoBehaviour {

  public enum selected_btn
  {
    start, exit, up, down
  }
  private AudioSource asource;
  public GameObject start_game_button;
  public GameObject exit_game_button;
  public GameObject up_game_button;
  public GameObject down_game_button;
  public GameObject time_text_holder;
  private InputDevice inputDevice;

  int min_time = 30;
    public int set_game_time = 120;
  console_button start_btn;
  console_button exit_btn;
  console_button up_btn;
  console_button down_btn;

  public GameObject main_menu_canvas;

  public selected_btn curr_selected;
  // Use this for initialization
  void Start()
  {
    asource = this.GetComponent<AudioSource>();
    inputDevice = (InputManager.Devices.Count > 0) ? InputManager.Devices[0] : null;
    //game_manager.gstate = vars.game_state.main_menu;
    curr_selected = selected_btn.start;
    start_btn = start_game_button.GetComponent<console_button>();
    exit_btn = exit_game_button.GetComponent<console_button>();
    up_btn = up_game_button.GetComponent<console_button>();
    down_btn = down_game_button.GetComponent<console_button>();
    set_btn_state();
    game_manager.game_time = set_game_time;
    time_text_holder.GetComponent<Text>().text = game_manager.game_time.ToString() + " SECONDS";
  }


    public void make_visible()
    {

        set_btn_state();

    }


    // Update is called once per frame
    void Update()
  {
    if (inputDevice != null)
    {
      if (inputDevice.DPadDown.WasPressed || (inputDevice.LeftStick.Down.State && !inputDevice.LeftStick.Down.LastState))
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
        curr_selected = selected_btn.down;
        break;
      case selected_btn.down:
        curr_selected = selected_btn.up;
        break;
      case selected_btn.up:
        curr_selected = selected_btn.exit;
        break;
      case selected_btn.exit:
        curr_selected = selected_btn.start;
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
        curr_selected = selected_btn.up;
        break;
      case selected_btn.up:
        curr_selected = selected_btn.down;
        break;
      case selected_btn.down:
        curr_selected = selected_btn.start;
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

        game_manager.gstate = vars.game_state.spawn;
        main_menu_canvas.SetActive(false);
        break;
      case selected_btn.exit:
        game_manager.gstate = vars.game_state.main_menu;
        Application.Quit();
        break;
      case selected_btn.up:
        up_btn.set_pressed();

        game_manager.game_time += 10;
        time_text_holder.GetComponent<Text>().text = game_manager.game_time.ToString() + " SECONDS";
        break;
      case selected_btn.down:
        down_btn.set_pressed();

        if (game_manager.game_time >= min_time + 10)
        {
          game_manager.game_time -= 10;
        }
        time_text_holder.GetComponent<Text>().text = game_manager.game_time.ToString() + " SECONDS";
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
        up_btn.set_normal();
        down_btn.set_normal();
        break;
      case selected_btn.exit:
        start_btn.set_normal();
        exit_btn.set_selected();
        up_btn.set_normal();
        down_btn.set_normal();
        break;
      case selected_btn.up:
        start_btn.set_normal();
        exit_btn.set_normal();
        up_btn.set_selected();
        down_btn.set_normal();
        break;
      case selected_btn.down:
        start_btn.set_normal();
        exit_btn.set_normal();
        up_btn.set_normal();
        down_btn.set_selected();
        break;
      default:
        curr_selected = selected_btn.start;
        set_btn_state();
        break;
    }
  }
}
