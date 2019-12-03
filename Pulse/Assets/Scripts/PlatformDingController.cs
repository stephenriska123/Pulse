using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDingController : MonoBehaviour
{

    private bool dingdongStart;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        dingdongStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlatformController.dingdongStarted && !dingdongStart)
        {
            if (transform.position.x < 1)
            {
                InvokeRepeating("appear", 1, 8);
                InvokeRepeating("disappear", 5.5f, 8);
            } else
            {
                InvokeRepeating("appear", 1.5f, 8);
                InvokeRepeating("disappear", 5.75f, 8);
            }
            dingdongStart = true;
        }
    }

    void appear()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    void disappear()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
