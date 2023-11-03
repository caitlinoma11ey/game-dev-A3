using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code adapted from https://discussions.unity.com/t/how-to-play-an-audio-file-after-another-finishes/131076/2

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip intro;
    [SerializeField]
    private AudioClip ghostNormal;
    [SerializeField]
    private AudioClip ghostScared;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = intro;
        StartCoroutine(playStartAudio());
    }

    IEnumerator playStartAudio()
    {
        audioSource.Play();

        yield return new WaitForSeconds(intro.length);

        audioSource.clip = ghostNormal;
        audioSource.loop = true;
        audioSource.Play();
    }

    public IEnumerator playScaredAudio()
    {
        audioSource.Stop();
        audioSource.clip = ghostScared;
        audioSource.loop = true;
        audioSource.Play();

        yield return new WaitForSecondsRealtime(10f);
    }

    public IEnumerator playNormalAudio()
    {
        audioSource.clip = ghostNormal;
        audioSource.loop = true;
        audioSource.Play();

        yield return null;
    }
}

