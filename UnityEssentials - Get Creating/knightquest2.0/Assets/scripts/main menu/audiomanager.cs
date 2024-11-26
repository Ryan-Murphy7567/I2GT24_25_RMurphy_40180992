using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public AudioSource attackAudio;
    public AudioSource deathAudio;
    // Saudio script for my combat sfx to enable components on prefabs 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play_attackAudio()
    {
        attackAudio.Play();
    }

    public void play_deathAudio()
    {
        deathAudio.Play();
    }
}
