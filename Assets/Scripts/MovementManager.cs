using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementManager : MonoBehaviour
{
    private List<PacStudentMovement> pacMovements = new List<PacStudentMovement>();
    private Animator animator;

    int currentPos = 0;
    float speed = 1.5f;
    bool isMoving = false;

    // Movements
    private Vector3 topLeft = new Vector3(-16.13f, 16.6f, 0f);
    private Vector3 topRight = new Vector3(-9.7f, 16.6f, 0f);
    private Vector3 bottomRight = new Vector3(-9.7f, 11.5f, 0f);
    private Vector3 bottomLeft = new Vector3(-16.13f, 11.5f, 0f);

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<AudioSource>().Play();

        pacMovements.Add(new PacStudentMovement(topLeft, topRight));
        pacMovements.Add(new PacStudentMovement(topRight, bottomRight));
        pacMovements.Add(new PacStudentMovement(bottomRight, bottomLeft));
        pacMovements.Add(new PacStudentMovement(bottomLeft, topLeft));
    }

    void Update()
    {
        if (!isMoving && currentPos < pacMovements.Count)
        {
            StartCoroutine(MovePacStudent(pacMovements[currentPos]));
        }

        if (currentPos == 4)
            currentPos = 0;
    }

    IEnumerator MovePacStudent(PacStudentMovement movement)
    {
        isMoving = true;
        float totalTime = 0f;

        while (Vector3.Distance(gameObject.transform.position, movement.endPos) > 0.1f)
        {
            float fractionOfJourney = totalTime * speed / movement.distance;
            gameObject.transform.position = Vector3.Lerp(movement.startPos, movement.endPos, fractionOfJourney);
            totalTime += Time.deltaTime;
            yield return null;
        }

        currentPos++;
        isMoving = false;
    }
}