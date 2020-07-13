using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    public Camera Cam;
    public float GrabRange;
    public float ForwardGrabRange;
    public float PlaceOrgenRange;
    public float AngleToStartGrab = 45.5f;
    public float LookingDownGrabRange = 4.2f;

    public Transform Hand;

    public bool GrabingOrgan = false;
    public bool OrganInRange = false;
    public Organo OrganoAgarrando;

    public Image HandOutline;
    public Image GrabIndicator;
    public Sprite HandOpen;
    public Sprite HandClose;
    public Sprite HandOpenOutline;
    public Sprite HandCloseOutline;

    private Color MyBlue = new Color(.0f, 1.0f, 0.9709249f, 0.4196078f);
    private Color MyRed = new Color(.0f, 1.0f, 0.9709249f, 0.4196078f);
    private Color MyGreen = new Color(0.04401779f, 1.0f, 0.0f, 0.4196078f);

    private void Start()
    {
        HandOutline.enabled = false;
    }

    void Update()
    {
        if(GrabingOrgan == false)                                                                       //SI NO ESTA AGARRANDO ALGO EL RAYCAST ES CORTO
        {
            GrabIndicator.sprite = HandOpen;
            HandOutline.sprite = HandOpenOutline;
            if (Cam.transform.localEulerAngles.x > AngleToStartGrab)
            {
                GrabRange = LookingDownGrabRange;
            }
            else
            {
                GrabRange = ForwardGrabRange;
            }
        }
        if (GrabingOrgan)                                                                               //SI SE ESTA AGARRANDO ALGO EL RAYCAST SE HACE LARGO
        {
            GrabIndicator.sprite = HandClose;
            HandOutline.sprite = HandCloseOutline;
            GrabRange = PlaceOrgenRange;
        }

        RaycastHit hit;                                                                                 //CREA EL RAYCAST
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, GrabRange))         //REVISA SI EL RAYCAST TOCA ALGO
        {
            //Debug.Log(hit.transform.name);

            if (hit.transform.tag == "Organo")                                                           //SI EL RAYCASR TOCA A UN ORGANO
            {
                OrganInRange = true;
                HandOutline.enabled = true;
                HandOutline.color = MyGreen;
            }
            if(hit.transform.tag == "PuestoOrgano" && GrabingOrgan)
            {
                HandOutline.color = MyBlue;
                HandOutline.enabled = true;
            }
        }
        else
        {
            OrganInRange = false;
            HandOutline.enabled = false;
            HandOutline.color = Color.red;
        }

        if (Input.GetMouseButtonDown(0) && OrganInRange)
        {
            if(hit.transform.GetComponent<Organo>() != null)
            {
                GrabingOrgan = true;
                OrganoAgarrando = hit.transform.GetComponent<Organo>();
                if (OrganoAgarrando.Puesto == false)
                {
                    OrganoAgarrando.Agarrar(Hand);
                }
            }
        }

        if (GrabingOrgan)
        {
            if(Input.GetMouseButtonUp(0))
            {
                GrabingOrgan = false;
                OrganInRange = false;
                OrganoAgarrando.Soltar(CheckForOrganPlacement());
                OrganoAgarrando = null;
            }
        }


        if (GrabingOrgan)
        {
            Debug.DrawRay(Cam.transform.position, Cam.transform.forward * GrabRange, Color.blue);
        }
        else
        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * GrabRange, Color.red);
    }

    public string CheckForOrganPlacement()
    {
        float _GrabRange = PlaceOrgenRange;

        RaycastHit _hit;                                                                                 //CREA EL RAYCAST
        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * _GrabRange, Color.yellow, 60f);
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out _hit, _GrabRange))         //REVISA SI EL RAYCAST TOCA ALGO
        {
            if (_hit.transform.tag == "PuestoOrgano")                                                    //SI EL RAYCASR TOCA A UN LUGARQUE SE PONE EL ORGANO
            {
                //Debug.Log(_hit.transform.name);
                return (_hit.transform.name.ToString());
            }
            return (null);
        }
        else
        {
            return (null);
        }
    }
}
