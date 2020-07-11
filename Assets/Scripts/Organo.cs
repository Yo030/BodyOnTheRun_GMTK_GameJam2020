using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organo : MonoBehaviour
{
    public bool Puesto = false;
    public bool EstaAgarrado = false;
    public GameObject[] Brazos;
    public GameObject MyPlacement;

    public float TiempoDeEscape;
    private float TiempoOriginal;

    private Rigidbody rb;
    private bool dentro_de_snap;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        TiempoOriginal = TiempoDeEscape;
    }

    public void Update()
    {
        if(Puesto)
        {
            TiempoDeEscape -= Time.deltaTime;
            if(TiempoDeEscape < 0)
            {
                TiempoDeEscape = TiempoOriginal;
                FueraDeLugar();
            }
        }
    }

    public void Agarrar(Transform _parent)
    {
        EstaAgarrado = true;
        this.transform.SetParent(_parent);
        rb.useGravity = false;
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.localRotation = Quaternion.Euler(-12.5f, -75f, 80f);
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Soltar()
    {
        EstaAgarrado = false;
        if (EstaAgarrado == false && dentro_de_snap == true)
        {
            PuestoEnLugar();
        }
        else
        {
            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void PuestoEnLugar()
    {
        this.transform.SetParent(MyPlacement.transform);
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.None;
        this.transform.position = MyPlacement.transform.position;
        this.transform.rotation = MyPlacement.transform.rotation;
        Puesto = true;
        DesactivarBrazos();
    }
    public void FueraDeLugar()
    {
        Puesto = false;
        this.transform.SetParent(null);
        rb.useGravity = true;
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        ActivarBrazos();
    }

    private void DesactivarBrazos()
    {
        for (int i = 0; i < Brazos.Length; i++)
        {
            Brazos[i].SetActive(false);
        }
    }

    private void ActivarBrazos()
    {
        for (int i = 0; i < Brazos.Length; i++)
        {
            Brazos[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == MyPlacement.name)
        {
            dentro_de_snap = true;
            Debug.Log("DENTRO");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == MyPlacement.name)
        {
            dentro_de_snap = false;
            Debug.Log("FUERA");
        }
    }

}
