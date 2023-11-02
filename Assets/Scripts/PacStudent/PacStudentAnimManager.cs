using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentAnimManager : MonoBehaviour
{
    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
    }

    public void MoveUp ()
    {
        animator.enabled = true;
        animator.SetTrigger("Walk_Up");
    }

    public void MoveDown()
    {
        animator.enabled = true;
        animator.SetTrigger("Walk_Down");
    }

    public void MoveRight()
    {
        animator.enabled = true;
        animator.SetTrigger("Walk_Right");
    }

    public void MoveLeft()
    {
        animator.enabled = true;
        animator.SetTrigger("Walk_Left");
    }

    public void StopWalking()
    {
        animator.enabled = false;
    }
}