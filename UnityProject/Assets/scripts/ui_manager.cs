using UnityEngine;
using System.Collections;

public class ui_manager : MonoBehaviour {


  public GameObject MainMenuHolder;
  public GameObject PauseMenuHolder;
  public GameObject CreditsMenuHolder;
  public GameObject SettingsMenuHolder;
  public GameObject GameSettingsMenuHolder;
    public GameObject GameStartScreenHolder;

    public GameObject MainMenuHolder_scr;
    public GameObject PauseMenuHolder_scr;
    public GameObject CreditsMenuHolder_scr;
    public GameObject SettingsMenuHolder_scr;
    public GameObject GameSettingsMenuHolder_scr;
    public GameObject GameStartScreenHolder_scr;
    // Use this for initialization

        void Awake()
    {
        game_manager.gstate = vars.game_state.game_starting;


    MainMenuHolder.SetActive(true);
    PauseMenuHolder.SetActive(true);
    GameStartScreenHolder.SetActive(true);
    CreditsMenuHolder.SetActive(true);
    SettingsMenuHolder.SetActive(true);
    GameSettingsMenuHolder.SetActive(true);

	}
	
	// Update is called once per frame
	void Update () {
        //hier je nach state die uis blenden

        if (game_manager.gstate == vars.game_state.game_starting)
        {
            if (!GameStartScreenHolder.activeSelf) { GameStartScreenHolder.SetActive(true); }
            if (MainMenuHolder.activeSelf) { MainMenuHolder.SetActive(false); }
            if (PauseMenuHolder.activeSelf) { PauseMenuHolder.SetActive(false); }
            if (CreditsMenuHolder.activeSelf) { CreditsMenuHolder.SetActive(false); }
            if (SettingsMenuHolder.activeSelf) { SettingsMenuHolder.SetActive(false); }
            if (GameSettingsMenuHolder.activeSelf) { GameSettingsMenuHolder.SetActive(false); }
            GameStartScreenHolder_scr.GetComponent<GameStartingMenuButtonManager>().make_visible();
        }

        if (game_manager.gstate == vars.game_state.main_menu)
    {
      if (GameStartScreenHolder.activeSelf) { GameStartScreenHolder.SetActive(false); }
      if (!MainMenuHolder.activeSelf) { MainMenuHolder.SetActive(true); }
      if (PauseMenuHolder.activeSelf) { PauseMenuHolder.SetActive(false); }

      if (CreditsMenuHolder.activeSelf) { CreditsMenuHolder.SetActive(false); }
      if (SettingsMenuHolder.activeSelf) { SettingsMenuHolder.SetActive(false); }
      if (GameSettingsMenuHolder.activeSelf) { GameSettingsMenuHolder.SetActive(false); }
            MainMenuHolder_scr.GetComponent<main_menu_button_manager>().make_visible();
    }

    if (game_manager.gstate == vars.game_state.pause_menu)
    {
            if (GameStartScreenHolder.activeSelf) { GameStartScreenHolder.SetActive(false); }
            if (MainMenuHolder.activeSelf) { MainMenuHolder.SetActive(false); }
      if (!PauseMenuHolder.activeSelf) { PauseMenuHolder.SetActive(true); }
       if (CreditsMenuHolder.activeSelf) { CreditsMenuHolder.SetActive(false); }
      if (SettingsMenuHolder.activeSelf) { SettingsMenuHolder.SetActive(false); }
      if (GameSettingsMenuHolder.activeSelf) { GameSettingsMenuHolder.SetActive(false); }
            PauseMenuHolder_scr.GetComponent<PauseMenuButtonManager>().make_visible();
    }

    if (game_manager.gstate == vars.game_state.credits)
    {
            if (GameStartScreenHolder.activeSelf) { GameStartScreenHolder.SetActive(false); }
            if (MainMenuHolder.activeSelf) { MainMenuHolder.SetActive(false); }
      if (PauseMenuHolder.activeSelf) { PauseMenuHolder.SetActive(false); }
      if (!CreditsMenuHolder.activeSelf) { CreditsMenuHolder.SetActive(true); }
      if (SettingsMenuHolder.activeSelf) { SettingsMenuHolder.SetActive(false); }
      if (GameSettingsMenuHolder.activeSelf) { GameSettingsMenuHolder.SetActive(false); }
            CreditsMenuHolder_scr.GetComponent<CreditMenuButtonManager>().make_visible();
    }

    if (game_manager.gstate == vars.game_state.settings)
    {
            if (GameStartScreenHolder.activeSelf) { GameStartScreenHolder.SetActive(false); }
            if (MainMenuHolder.activeSelf) { MainMenuHolder.SetActive(false); }
      if (PauseMenuHolder.activeSelf) { PauseMenuHolder.SetActive(false); }
      if (CreditsMenuHolder.activeSelf) { CreditsMenuHolder.SetActive(false); }
      if (!SettingsMenuHolder.activeSelf) { SettingsMenuHolder.SetActive(true); }
      if (GameSettingsMenuHolder.activeSelf) { GameSettingsMenuHolder.SetActive(false); }
            SettingsMenuHolder_scr.GetComponent<OptionMenuButton>().make_visible();
    }
    if (game_manager.gstate == vars.game_state.game_settings)
    {
            if (GameStartScreenHolder.activeSelf) { GameStartScreenHolder.SetActive(false); }
            if (MainMenuHolder.activeSelf) { MainMenuHolder.SetActive(false); }
      if (PauseMenuHolder.activeSelf) { PauseMenuHolder.SetActive(false); }
      if (CreditsMenuHolder.activeSelf) { CreditsMenuHolder.SetActive(false); }
      if (SettingsMenuHolder.activeSelf) { SettingsMenuHolder.SetActive(false); }
      if (!GameSettingsMenuHolder.activeSelf) { GameSettingsMenuHolder.SetActive(true); }
            GameSettingsMenuHolder_scr.GetComponent<GameSettingsMenuButtonManager>().make_visible();
    }

    if (game_manager.gstate == vars.game_state.playing || game_manager.gstate == vars.game_state.win_sequenze || game_manager.gstate == vars.game_state.spawn)
    {
            if (GameStartScreenHolder.activeSelf) { GameStartScreenHolder.SetActive(false); }
            if (MainMenuHolder.activeSelf) { MainMenuHolder.SetActive(false); }
      if (PauseMenuHolder.activeSelf) { PauseMenuHolder.SetActive(false); }
       if (CreditsMenuHolder.activeSelf) { CreditsMenuHolder.SetActive(false); }
      if (SettingsMenuHolder.activeSelf) { SettingsMenuHolder.SetActive(false); }
      if (GameSettingsMenuHolder.activeSelf) { GameSettingsMenuHolder.SetActive(false); }
    
    }

       
    }
}
