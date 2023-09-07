using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{

    public AudioSource AudioSource;

    public AudioClip AudioClip;

    public void playClip()
    {
        AudioSource.clip = AudioClip;
        AudioSource.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
