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

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = intro;
        StartCoroutine(playAudio());
    }

    IEnumerator playAudio()
    {
        audioSource.Play();

        yield return new WaitForSeconds(intro.length);

        audioSource.clip = ghostNormal;
        audioSource.loop = true;
        audioSource.Play();
    }
}

