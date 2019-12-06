using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeSpikeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController.checkpoint |= other.gameObject.name == "Player";
    }
}
