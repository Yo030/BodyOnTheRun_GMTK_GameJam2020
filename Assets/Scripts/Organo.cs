using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    private BoxCollider MyColider;
    public Vector3 MyStartScale;
    public Vector3 MyScaleWhenPlaced = new Vector3(3.5f, 3.5f, 3.5f);

    public Transform Player;
    public bool HitingOrganPlacement;

    public Image MyUIImage;
    public Sprite SolidSprite;
    public Sprite GhostSprite;

    //public AudioClip SonidoAgarrar;
    private AudioSource MyAudioSource;
    public AudioClip Agarre;
    public AudioClip Risa;
    public AudioClip Placed;
    public TiempoJuego _tiempojuego;

    private Color MyBlue = new Color(.0f, 1.0f, 0.9709249f, 0.4196078f);
    private Color MyWhite = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color MyWhtieTransparent = new Color(1.0f, 1.0f, 1.0f, 0.5529412f);
    private Color MyGreen = new Color(0.04401779f, 1.0f, 0.0f, 0.4196078f);

    private bool CallOnce = false;

    public void Start()
    {
        MyAudioSource = GetComponent<AudioSource>();
        MyUIImage.sprite = GhostSprite;
        rb = GetComponent<Rigidbody>();
        TiempoOriginal = TiempoDeEscape;
        MyColider = GetComponent<BoxCollider>();
        MyStartScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

        MyPlacement.SetActive(false);
    }

    public void Update()
    {
        if(_tiempojuego.GameTime < 1 && MyAudioSource.isPlaying == false && CallOnce == false)
        {
            PlaySound(Risa);
            CallOnce = true;
        }
        if(_tiempojuego.Ganar == true)
        {
            return;
        }
        else if(Puesto)
        {
            MyUIImage.sprite = SolidSprite;
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
        PlaySound(Agarre);
        MyColider.enabled = false;
        MyUIImage.color = MyBlue;
        EstaAgarrado = true;
        this.transform.SetParent(_parent);
        rb.useGravity = false;
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.localRotation = Quaternion.Euler(-12.5f, -75f, 80f);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        MyPlacement.SetActive(true);
    }

    public void Soltar(string _nombrepuesto)
    {
        EstaAgarrado = false;
        if (_nombrepuesto == MyPlacement.transform.name)
        {
            MyUIImage.color = MyWhite;
            MyPlacement.SetActive(true);
            PuestoEnLugar();
        }
        else
        {
            PlaySound(Risa);
            MyUIImage.color = MyWhtieTransparent;
            MyColider.enabled = true;
            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            Vector3 PlayerDirection = Player.position - transform.position;
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.localScale = MyStartScale;
            MyPlacement.SetActive(false);
        }
    }

    public void PuestoEnLugar()
    {
        PlaySound(Placed);
        MyPlacement.GetComponent<BoxCollider>().enabled = false;
        this.transform.SetParent(MyPlacement.transform);
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.None;
        this.transform.position = MyPlacement.transform.position;
        this.transform.rotation = MyPlacement.transform.rotation;
        Puesto = true;
        this.transform.localScale = MyScaleWhenPlaced;
        DesactivarBrazos();
        MyPlacement.GetComponent<MeshRenderer>().enabled = false;
    }
    public void FueraDeLugar()
    {
        PlaySound(Risa);
        MyUIImage.color = MyWhtieTransparent;
        MyUIImage.sprite = GhostSprite;
        MyUIImage.color = MyWhtieTransparent;
        MyPlacement.GetComponent<BoxCollider>().enabled = true;
        MyColider.enabled = true;
        Puesto = false;
        this.transform.SetParent(null);
        rb.useGravity = true;
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        this.transform.localScale = MyStartScale;
        ActivarBrazos();
        MyPlacement.SetActive(false);
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

    public void Placement()
    {
        PlaySound(Placed);
        if (HitingOrganPlacement == MyPlacement)
        {
            Debug.Log("SEEEE PEGO");
            PuestoEnLugar();
        }
        else
        {
            Debug.Log("NOOOO PEGO");
            MyColider.enabled = true;
            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            this.transform.localScale = MyStartScale;
        }
    }

    public void PlaySound(AudioClip _sonido)
    {
        MyAudioSource.clip = _sonido;
        MyAudioSource.Play();
    }
}
