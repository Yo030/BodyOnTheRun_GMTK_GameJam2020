using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerManager : MonoBehaviour
{
    private VideoPlayer _videoplayer;
    private AudioSource _audioSource;
    public TiempoJuego _Tiempojuego;
    public VideoClip SinPulso;
    public AudioClip Muerto;

    public void Start()
    {
        _videoplayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponent<AudioSource>();
        _videoplayer.Play();
    }

    public void Update()
    {
        if(_Tiempojuego.GameTime < 1)
        {
            _audioSource.clip = Muerto;
            if(_audioSource.isPlaying == false)
            {
                _audioSource.Play();
            }

            _videoplayer.clip = SinPulso;
            _videoplayer.isLooping = false;
        }
    }
}
