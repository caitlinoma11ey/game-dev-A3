using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentMovement
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float distance;
    public string animation;

    public PacStudentMovement(Vector3 startPos, Vector3 endPos, string animation)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.distance = Vector3.Distance(startPos, endPos);
        this.animation = animation;
    }
}
