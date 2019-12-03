using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeController : MonoBehaviour
{

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool snapStart;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x,
                                  transform.position.y < 3.5f ? transform.position.y + 1.5f : transform.position.y - 2.5f,
                                  transform.position.z);
        snapStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlatformController.snapStarted && !snapStart)
        {
            InvokeRepeating("snapDown", 1, 2);
            InvokeRepeating("snapUp", 2, 2);
            snapStart = true;
        }
    }

    void snapDown()
    {
        transform.position = endPosition;
    }

    void snapUp()
    {
        transform.position = startPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
