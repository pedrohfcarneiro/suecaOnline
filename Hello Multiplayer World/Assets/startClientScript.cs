using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class startClientScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<NetworkManager>().StartClient();
        }
    }
}
