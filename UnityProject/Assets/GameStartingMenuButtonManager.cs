using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;
public class GameStartingMenuButtonManager : MonoBehaviour {

    private InputDevice inputDevice;

    public bool started_timer;
    public float timer = 0.0f;
    public float max_timer = 10.0f;

    private AudioSource asource;
    // Use this for initialization
    void Start()
    {
        
        asource = this.GetComponent<AudioSource>();
        inputDevice = (InputManager.Devices.Count > 0) ? InputManager.Devices[0] : null;
        started_timer = true;
        timer = max_timer;
    }

    public void make_visible()
    {

        started_timer = true;
       // timer = max_timer;
    }


    // Update is called once per frame
    void Update()
    {


        if(timer > 0.0f )
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (started_timer)
            {
                started_timer = false;
                timer = max_timer;
                game_manager.gstate = vars.game_state.main_menu;
            }
         
        }

        if (inputDevice == null)
        {
            Application.Quit();
        }
    }
}
