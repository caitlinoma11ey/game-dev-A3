using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip pacEating;

    [SerializeField]
    private AudioClip pacNormal;

    [SerializeField]
    private AudioClip pacWall;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = pacEating;
    }

    public void PlayEatingClip()
    {
        audioSource.clip = pacEating;
        audioSource.Play();
    }

    public void PlayNormalClip()
    {
        audioSource.clip = pacNormal;
        audioSource.Play();
    }
}
