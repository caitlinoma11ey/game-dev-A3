using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public Tweener tweener;
    public Tilemap tilemap; 

    public enum direction
    {

    }

    private bool isMoving = false;
    private float gridSize = 1.28f;
    private float duration = 0.5f;

    KeyCode lastInput;
    Transform currentInput; 

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            lastInput = KeyCode.W;
            Move();
        }


        if (Input.GetKey(KeyCode.A))
        {
            lastInput = KeyCode.A;
            Move();
        }

        if (Input.GetKey(KeyCode.S))
        {
            lastInput = KeyCode.S;
            Move();
        }

        if (Input.GetKey(KeyCode.D))
        {
            lastInput = KeyCode.D;
            Move();
        }
    }

    public void MoveUp()
    {

    }
    
    public void MoveDown()
    {

    }

    public void MoveLeft()
    {

    }

    public void Move()
    {
        if (tweener != null && !tweener.isLerping())
        {
            // Find the direction to move pacstudent in
            Vector2 direction = GetDirection();
            

            Vector2 startPos = transform.position;
            Vector2 endPos = startPos + (direction * gridSize);


            if (!tweener.TweenExists(transform))
            {
                tweener.AddTween(transform, startPos, endPos, duration);
            }

        }
    }

    private Vector2 GetDirection ()
    {
        if (lastInput == KeyCode.W)
            return Vector2.up;

        if (lastInput == KeyCode.A)
            return Vector2.left;

        if (lastInput == KeyCode.S)
            return Vector2.down;

        // If lastInput == KeyCode.D
        return Vector2.right;
    }
}
