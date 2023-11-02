using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip eating;

    [SerializeField]
    private AudioClip normal;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = eating;
    }

    public void PlayEatingClip()
    {
        audioSource.clip = eating;
        audioSource.Play();
    }

    public void PlayNormalClip()
    {
        audioSource.clip = normal;
        audioSource.Play();
    }
}
