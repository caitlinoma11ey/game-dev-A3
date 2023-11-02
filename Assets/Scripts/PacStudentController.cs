using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public PacStudentAnimManager animManager; 
    public Tweener tweener;
    public Tilemap tilemap; 

    private float gridSize = 1.28f;
    private float duration = 0.5f;

    KeyCode lastInput;
    KeyCode currentInput; 

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

        Move();
    }

    public void Move()
    {
        if (tweener != null && !tweener.isLerping())
        {
            // Find the direction to move pacstudent
            Vector3 direction = GetDirection(lastInput);

            Vector3 startPos = transform.position;
            Vector3 endPos = transform.position + (direction * gridSize);

            // Check if adjacent grid from last input is walkable
            if (CanWalk(endPos))
            {
                currentInput = lastInput;
                ChangeDirection();
                CheckForPellet(endPos);

                Lerp(startPos, endPos);

            }
            else
            {
                direction = GetDirection(currentInput);
                startPos = transform.position;
                endPos = startPos + (direction * gridSize);

                // Continue moving in current position if it is valid
                if (CanWalk(endPos))
                {
                    CheckForPellet(endPos);
                    Lerp(startPos, endPos);
                }
                else
                {
                    animManager.StopWalking();
                }
            }
        }
    }

    void CheckForPellet(Vector2 newPos)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(newPos);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile.name.Contains("Pellet"))
        {
            
        }
        else if (tile.name.Contains("Empty"))
        {

        }
    }

    void Lerp(Vector2 startPos, Vector3 endPos)
    {
        if (!tweener.TweenExists(transform))
        {
            tweener.AddTween(transform, startPos, endPos, duration);
        }
    }

    void ChangeDirection()
    {
        if (lastInput == KeyCode.W)
            animManager.MoveUp();

        if (lastInput == KeyCode.S)
            animManager.MoveDown();

        if (lastInput == KeyCode.A)
            animManager.MoveLeft();

        if (lastInput == KeyCode.D)
            animManager.MoveRight();
    }

    Vector2 GetDirection(KeyCode input)
    {
        if (input == KeyCode.W)
            return Vector2.up;

        if (input == KeyCode.A)
            return Vector2.left;

        if (input == KeyCode.S)
            return Vector2.down;

        // If lastInput == KeyCode.D
        return Vector2.right;
    }

    bool CanWalk(Vector2 newPos)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(newPos);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (!tile.name.Contains("Wall"))
        {
            return true;
        }

        return false;
    }
}
