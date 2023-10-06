using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementManager : MonoBehaviour
{
    private List<PacStudentMovement> pacMovements = new List<PacStudentMovement>();
    private Animator animator;

    bool isMoving = false;
    int currentPos = 0;

    // Movements
    private Vector3 topLeft = new Vector3(-16.13f, 16.6f, 0f);
    private Vector3 topRight = new Vector3(-9.7f, 16.6f, 0f);
    private Vector3 bottomRight = new Vector3(-9.7f, 11.5f, 0f);
    private Vector3 bottomLeft = new Vector3(-16.13f, 11.5f, 0f);

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();

        pacMovements.Add(new PacStudentMovement(topLeft, topRight, "Walk_Right"));
        pacMovements.Add(new PacStudentMovement(topRight, bottomRight, "Walk_Down"));
        pacMovements.Add(new PacStudentMovement(bottomRight, bottomLeft, "Walk_Left"));
        pacMovements.Add(new PacStudentMovement(bottomLeft, topLeft, "Walk_Up"));
    }

    void Update()
    {
        if (!isMoving && currentPos < pacMovements.Count)
        {
            StartCoroutine(MovePacStudent(pacMovements[currentPos]));
        }
    }

    IEnumerator MovePacStudent(PacStudentMovement movement)
    {
        isMoving = true;
        float totalTime = 0f;

        animator.SetTrigger(movement.animation);

        while (Vector3.Distance(gameObject.transform.position, movement.endPos) > 0.1f)
        {
            float fractionOfJourney = totalTime / movement.distance;
            gameObject.transform.position = Vector3.Lerp(movement.startPos, movement.endPos, fractionOfJourney);
            totalTime += Time.deltaTime;
            yield return null;
        }

        currentPos++;
        isMoving = false;
    }
}