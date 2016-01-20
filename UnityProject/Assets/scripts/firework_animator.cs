using UnityEngine;
using System.Collections;

public class firework_animator : MonoBehaviour {



    public GameObject firework_blue;
    public GameObject firework_green;
    public GameObject firework_red;
    public GameObject firework_yellow;

    public float replay_time = 2.0f;

    float replay_time_blue;
    float replay_time_green;
    float replay_time_red;
    float replay_time_yellow;


    bool is_playing_blue;
    bool is_playing_green;
    bool is_playing_red;
    bool is_playing_yellow;


    public bool enable_twice;
    // Use this for initialization
    void Start () {
        replay_time_blue = replay_time;
        replay_time_green = replay_time;
        replay_time_red = replay_time;
        replay_time_yellow = replay_time;

        is_playing_blue = true;
        is_playing_green = true;
        is_playing_red = true;
        is_playing_yellow = true;

    }
	
	// Update is called once per frame
	void Update () {
        if (!enable_twice) { return; }
        if (!is_playing_blue && replay_time_blue > 0.0f)
        {
            replay_time_blue -= Time.deltaTime;
            if(replay_time_blue <= 0.0f)
            {
                replay_time_blue = replay_time;
                is_playing_blue = true;
                firework_blue.GetComponent<ParticleSystem>().Play();
            }
        }


        if (!is_playing_green && replay_time_green > 0.0f)
        {
            replay_time_green -= Time.deltaTime;
            if (replay_time_green <= 0.0f)
            {
                replay_time_green = replay_time;
                is_playing_green = true;
                firework_green.GetComponent<ParticleSystem>().Play();
            }
        }




        if (!is_playing_red && replay_time_red > 0.0f)
        {
            replay_time_red -= Time.deltaTime;
            if (replay_time_red <= 0.0f)
            {
                replay_time_red = replay_time;
                is_playing_red = true;
                firework_red.GetComponent<ParticleSystem>().Play();
            }
        }


        if (!is_playing_yellow && replay_time_yellow > 0.0f)
        {
            replay_time_yellow -= Time.deltaTime;
            if (replay_time_yellow <= 0.0f)
            {
                replay_time_yellow = replay_time;
                is_playing_yellow = true;
                firework_yellow.GetComponent<ParticleSystem>().Play();
            }
        }

    }



    public void play_blue()
    {
        firework_blue.GetComponent<ParticleSystem>().Play();
        is_playing_blue = false;
    }

    public void play_green()
    {
        firework_green.GetComponent<ParticleSystem>().Play();
        is_playing_green = false;
    }

    public void play_red()
    {

        firework_red.GetComponent<ParticleSystem>().Play();
        is_playing_red = false;
    }

    public void play_yellow()
    {
        firework_yellow.GetComponent<ParticleSystem>().Play();
        is_playing_yellow = false;
    }
}
