using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public Camera Cam;
    public float GrabRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, GrabRange))
        {
            if(hit.transform.tag == "Organo")
            {
                Debug.Log(hit.transform.name);
            }
        }

    }
}
