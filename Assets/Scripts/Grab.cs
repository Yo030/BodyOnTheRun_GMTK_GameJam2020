using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    public Camera Cam;
    public float GrabRange;
    public float ForwardGrabRange;
    public float AngleToStartGrab = 45.5f;
    public float LookingDownGrabRange = 4.2f;

    public Transform Hand;

    public bool GrabingOrgan = false;
    public bool OrganInRange = false;
    private Organo OrganoAgarrando;

    public Image GrabIndicator;

    void Update()
    {
        if (Cam.transform.localEulerAngles.x > AngleToStartGrab)
        {
            GrabRange = LookingDownGrabRange;
        }
        else
        {
            GrabRange = ForwardGrabRange;
        }

        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, GrabRange))
        {
            if(hit.transform.tag == "Organo")
            {
                OrganInRange = true;
                GrabIndicator.color = Color.green;
            }
        }
        else
        {
            OrganInRange = false;
            GrabIndicator.color = Color.red;
        }

        if (Input.GetMouseButtonDown(0) && OrganInRange)
        {
            GrabingOrgan = true;
            OrganoAgarrando = hit.transform.GetComponent<Organo>();
            if(OrganoAgarrando.Puesto == false)
            {
                OrganoAgarrando.Agarrar(Hand);
            }
        }

        if (GrabingOrgan)
        {
            if(Input.GetMouseButtonUp(0))
            {
                GrabingOrgan = false;
                OrganoAgarrando.Soltar();
                OrganoAgarrando = null;
            }
        }

        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * GrabRange, Color.red);
    }
}
