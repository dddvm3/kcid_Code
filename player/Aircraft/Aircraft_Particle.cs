using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aircraft_Particle : MonoBehaviour
{
    public static Aircraft_Particle Instance;
    [Header("照名字排")]
    public GameObject Run_Left;
    public GameObject Run_Right;
    public AudioSource FlySound;
    bool doonce;
    void Awake()
    {
        Instance = this;
    }
    void PlayRun_Left()
    {
        GameObject run = Instantiate(Run_Left, transform);
        run.SetActive(true);
        run.transform.parent = null;
    }
    public void FlySOundFadeOut()
    {
        // FlySound.Stop();
        // FlySound.volume = 0;
        AudioFadeScript.Instance.StopAllCoroutines();
        if(FlySound.isPlaying)
        {
            // doonce = false;
            StartCoroutine(AudioFadeScript.FadeOut(FlySound,0.01f));
        }
    }
    public void FlySoundPlay()
    {
        
        // FlySound.Stop();
        // FlySound.volume = 0;
        AudioFadeScript.Instance.StopAllCoroutines();
        // doonce = true;
        if(!FlySound.isPlaying)
        StartCoroutine(AudioFadeScript.FadeIn(FlySound,0.01f));
    }

    void PlayRun_Right()
    {
        GameObject run = Instantiate(Run_Right, transform);
        run.SetActive(true);
        run.transform.parent = null;
    }
}