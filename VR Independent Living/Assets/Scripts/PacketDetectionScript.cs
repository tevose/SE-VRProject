using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacketDetectionScript : MonoBehaviour
{
    public static bool packetDetected;
    public GameObject joiningAvatar;

    
    // Start is called before the first frame update
    void Start()
    {
        packetDetected = false;
 
    }

    // Update is called once per frame
    void Update()
    {
        if (packetDetected){
            joiningAvatar.SetActive(true);
        }
        else if (!packetDetected){
            joiningAvatar.SetActive(false);
        }
        
    }
}
