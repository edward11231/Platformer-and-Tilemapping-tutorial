﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float cameraDown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    { 
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);

        //if (Input.GetKeyDown(KeyCode.S))
            //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - cameraDown, this.transform.position.z);

        //if (Input.GetKeyUp(KeyCode.S))
            //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraDown, this.transform.position.z);
            
    }
}
