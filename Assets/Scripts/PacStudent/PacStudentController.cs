using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    public PacStudentAnimManager animManager;
    public PacStudentAudioManager audioManager;
    public ParticleSystem dust; 


    public Tweener tweener;
    public Tilemap tilemap;

    private float gridSize = 1.28f;
    private float duration = 0.5f;

    private Transform recentTween = null;

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
        if (tweener != null && !tweener.isLerping(recentTween))
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
                    Lerp(startPos, endPos);
                }
                else
                {
                    StopMovement(endPos);
                }
            }
        }
    }

    public void StopMovement(Vector3 endPos)
    {
        HideDust();
        animManager.StopWalking();
        CheckForPellet(endPos);
    }

    void CheckForPellet(Vector3 newPos)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(newPos);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile.name.Contains("Pellet"))
        {
            audioManager.PlayEatingClip();
        }
        else
        {
            audioManager.PlayNormalClip();
        }
    }

    void Lerp(Vector2 startPos, Vector3 endPos)
    {
        ShowDust();

        if (!tweener.TweenExists(transform))
        {
            tweener.AddTween(transform, startPos, endPos, duration);
        }

        CheckForPellet(endPos);

        recentTween = transform; // Allows us to see if tween has completed 
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

    bool CanWalk(Vector3 newPos)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(newPos);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (!tile.name.Contains("Wall"))
        {
            return true;
        }

        return false;
    }

    void ShowDust()
    {
        dust.Play();
    }

    void HideDust()
    {
        dust.Stop();
    }
}
