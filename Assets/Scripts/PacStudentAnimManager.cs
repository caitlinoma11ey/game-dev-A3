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
        if (gameObject.transform.position == new Vector3(-16.13f, 16.6f, 0f))
            animator.SetTrigger("Walk_Right");

        if (gameObject.transform.position == new Vector3(-9.7f, 16.6f, 0f))
            animator.SetTrigger("Walk_Down");

        if (gameObject.transform.position == new Vector3(-9.7f, 11.5f, 0f))
            animator.SetTrigger("Walk_Left");

        if (gameObject.transform.position == new Vector3(-16.13f, 11.5f, 0f))
            animator.SetTrigger("Walk_Up");
    }
}