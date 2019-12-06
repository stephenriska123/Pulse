using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    private Vector3 startPosition;
    private Vector3 endPosition;
    private AudioSource musicController;
    private GameObject player;
    private bool snapReady;
    private bool dingdongReady;
    private bool beatReady;
    public static bool finalStarted;
    public static bool snapStarted;
    public static bool dingdongStarted;
    public static bool finalStart;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position;
        musicController = GameObject.FindWithTag("MusicController").GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        snapStarted = false;
        dingdongReady = false;
        beatReady = false;
        finalStarted = false;
        snapStarted = false;
        dingdongStarted = false;
        finalStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        endPosition.y = startPosition.y + 2 * Mathf.Sin(Time.timeSinceLevelLoad * 1.575f);
        if (!musicController.isPlaying && endPosition.y > 3.99)
        {
            musicController.Play();
        }
        transform.position = endPosition;

        if (player.transform.position.z > 5.5 && !snapReady)
        {
            Invoke("snaps", musicController.clip.length - musicController.time);
            snapReady = true;
        }

        if (player.transform.position.z > 18 && !dingdongReady)
        {
            Invoke("dingdongs", musicController.clip.length - musicController.time);
            dingdongReady = true;
        }

        if (player.transform.position.z > 60 && !beatReady)
        {
            var beat = Resources.Load("4") as AudioClip;
            musicController.clip = beat;
            musicController.Play();
            beatReady = true;
        }

        if (finalStart && !finalStarted)
        {
            Invoke("finalclip", musicController.clip.length - musicController.time);
            finalStart = false;
        }
    }

    void snaps()
    {
        var snap = Resources.Load("2") as AudioClip;
        musicController.clip = snap;
        musicController.Play();
        snapStarted = true;
    }

    void dingdongs()
    {
        var dingdong = Resources.Load("3") as AudioClip;
        musicController.clip = dingdong;
        musicController.Play();
        dingdongStarted = true;
    }

    void finalclip()
    {
        var finalClip = Resources.Load("5") as AudioClip;
        musicController.clip = finalClip;
        musicController.Play();
        finalStarted = true;
    }
}
