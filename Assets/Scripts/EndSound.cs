using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EndSound : MonoBehaviour
{
    public AudioSource _audiosource;

    void Start()
    {
        _audiosource.Stop();
    }
    
}
