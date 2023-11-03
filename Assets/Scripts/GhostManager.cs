using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public List<Animator> ghostAnimators = new List<Animator>(4);
    public AudioManager ghostAudio;

    public void TriggerScaredState()
    {
        ghostAudio.StartCoroutine(ghostAudio.playScaredAudio());

        for (int i = 0; i < ghostAnimators.Count; i++)
        {
            ghostAnimators[i].SetTrigger("isScared");
        }
    }

    public void TriggerRecoveringState()
    {
        for (int i = 0; i < ghostAnimators.Count; i++)
        {
            ghostAnimators[i].SetTrigger("isRecovering");
        }
    }

    public void TriggerWalkingState()
    {
        ghostAudio.StartCoroutine(ghostAudio.playNormalAudio());

        for (int i = 0; i < ghostAnimators.Count; i++)
        {
            ghostAnimators[i].SetTrigger("walkRight"); 
        }
    }
}
