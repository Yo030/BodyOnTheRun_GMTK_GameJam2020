using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TiempoJuego : MonoBehaviour
{
    Text TimeText;
    public bool Ganar = false;
    public float GameTime;

    [Header("Pantallas")]
    public GameObject PantallaGanar;
    public GameObject PantallaPerder;

    [Header("ScripsParaDesactivar")]
    public Grab _grab;
    public PlayerMovement _playermovemnt;
    public MouseLook _mouselook;

    [Header("LugarDeOrganos")]
    public GameObject[] ListaDeOrganos;

    void Start()
    {
        TimeText = GetComponent<Text>();
        PantallaGanar.SetActive(false);
        PantallaPerder.SetActive(false);
    }

    void Update()
    {
        RevisarLugarDeOrganos();

        if(Ganar == true)
        {
            PantallaGanar.SetActive(true);
            DeactivateScripts();
            return;
        }

        if(GameTime > 0.9999 && Ganar == false)
        {
            GameTime -= Time.deltaTime;
            string Minutes = Mathf.FloorToInt(GameTime / 60).ToString("00");
            string Seconds = Mathf.FloorToInt(GameTime % 60).ToString("00");
            TimeText.text = string.Format("{0:00}:{1:00}", Minutes, Seconds);
        }
        else if(GameTime <= 0.9999)
        {
            PantallaPerder.SetActive(true);
            DeactivateScripts();
        }
    }

    public void DeactivateScripts()
    {
        _grab.enabled = false;
        _playermovemnt.enabled = false;
        _mouselook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RevisarLugarDeOrganos()
    {
        int _organospuestos = 0;
        for(int i=0; i< ListaDeOrganos.Length; i++)
        {
            Organo _revisarorgano = ListaDeOrganos[i].GetComponent<Organo>();
            if(_revisarorgano.Puesto == true)
            {
                _organospuestos++;
                if(_organospuestos==ListaDeOrganos.Length)
                {
                    Ganar = true;
                    for (int j = 0; j < ListaDeOrganos.Length; j++)
                    {
                        Organo __organos = ListaDeOrganos[i].GetComponent<Organo>();
                        __organos.enabled = false;
                    }
                }
            }
        }
    }
}
