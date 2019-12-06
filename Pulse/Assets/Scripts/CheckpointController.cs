using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{

    private AudioSource spaceSound;
    private GameObject player;
    public static bool checkpoint;

    // Start is called before the first frame update
    void Start()
    {
        spaceSound = GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        checkpoint = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.z > 32)
        {
            spaceSound.volume = 1 - ((transform.position.z - player.transform.position.z) / 20) - 0.4f;
        }
        if (player.transform.position.z > 52)
        {
            spaceSound.volume = 1 + ((transform.position.z - player.transform.position.z) / 20) - 0.4f;
        }
    }
}
