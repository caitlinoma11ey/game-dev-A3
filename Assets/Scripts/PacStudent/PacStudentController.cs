using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class PacStudentController : MonoBehaviour
{
    public PacStudentAnimManager animManager;
    public PacStudentAudioManager audioManager;
    public PelletManager pelletManager;
    public GhostManager ghostManager;

    public CherryController cherryController;
    public ParticleSystem dust;
    public ParticleSystem wallHitPS;

    public Tweener tweener;
    public Tilemap tilemap;

    private float gridSize = 1.28f;
    private float duration = 0.5f;

    private Transform recentTween = null;

    KeyCode lastInput;
    KeyCode currentInput;

    public Text countdownTxt;
    private bool countdownActive = false;

    public Text timerText;
    private float time;

    public Text gameOver; 

    void Start()
    {
        HideDust();
        time = PlayerPrefs.GetFloat("elapsedTime", 0f);
        CreateTimer();

        int points = pelletManager.savedPoints = PlayerPrefs.GetInt("points", 0);
        pelletManager.pointsTxt.text = points.ToString();

        StartCoroutine(CountDown());

        wallHitPS.Stop();
    }

    IEnumerator CountDown()
    {
        countdownActive = true;
        countdownTxt.text = "3";
        yield return new WaitForSeconds(1);

        countdownTxt.text = "2";
        yield return new WaitForSeconds(1);

        countdownTxt.text = "1";
        yield return new WaitForSeconds(1);

        countdownTxt.text = "GO!";
        yield return new WaitForSeconds(1);

        countdownTxt.enabled = false;
        countdownActive = false;

        EnableGameObjects();
    }

    void EnableGameObjects ()
    {
        animManager.animator.enabled = true;

        for (int i = 0; i < ghostManager.ghostAnimators.Count; i++)
        {
            ghostManager.ghostAnimators[i].enabled = true;
        }

        ShowDust();
        cherryController.InvokeRepeating("PlaceBonusCherry", 0f, 10f);
    }

    void Update()
    {
        if (countdownActive == false)
        {
            if (pelletManager.PelletsGone())
            {
                GameOver();
            }
            else
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

                CreateTimer();
                SaveTimer();
            }
        }
    }

    void CreateTimer()
    {
        time += Time.deltaTime;

        int min = (int)(time/ 60);
        int sec = (int)(time % 60);
        int miliSec = Mathf.FloorToInt((time * 1000) % 1000);

        //Round milisec to two digits
        miliSec = Mathf.FloorToInt(miliSec / 10);

        timerText.text = min.ToString("00") + ":" + sec.ToString("00") + ":" + miliSec.ToString("00");
    }

    void SaveTimer()
    {
        PlayerPrefs.SetFloat("elapsedTime", time);
    }

    void Move()
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

                StopWallParticles();
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
                    StopWallParticles();
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
        CheckForWall(endPos);
        animManager.StopWalking();
    }

    public void CheckForWall(Vector3 newPos)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(newPos);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return;

        if (tile.name.Contains("Wall"))
        {
            StartHitWallParticles();
            audioManager.PlayWallHitClip();
        }
}
    void CheckForPellet(Vector3 newPos)
    {
        Vector3Int gridPosition = tilemap.WorldToCell(newPos);
        TileBase tile = tilemap.GetTile(gridPosition);

        if (tile == null)
            return;

        if (tile.name.Contains("Pellet"))
        {
            audioManager.PlayEatingClip();
            pelletManager.RemovePellet(gridPosition);
            if (pelletManager.PelletsGone())
            {
                GameOver();
            }
        }
        else if (!tile.name.Contains("Wall"))
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

        CheckForPellet(startPos);

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

        if (tile == null)
            return false;

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

    void StartHitWallParticles()
    {
        wallHitPS.Play();
    }

    void StopWallParticles()
    {
        wallHitPS.Stop();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Teleporter")
        {
            StartCoroutine(Teleport());
        }
    }

    private IEnumerator Teleport()
    {
        // -0.3f to ensure pacstudent remains in middle of the map.
        Vector3 newPos = new Vector3(-transform.position.x-0.3f, transform.position.y, 0);
        transform.position = newPos;

        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cherry")
            HandleCherry(other);

        if (other.tag == "PowerPellet")
            HandlePowerPellet(other);
    }

    void HandleCherry(Collider other)
    {
        tweener.RemoveTween(other.transform);
        cherryController.EatCherry(other.gameObject);
        pelletManager.EatCherry();

        if (pelletManager.PelletsGone())
        {
            GameOver();
        }
    }

    void HandlePowerPellet(Collider other)
    {
        Destroy(other.gameObject);
        pelletManager.EatPowerPellet();
        pelletManager.PelletsGone();
    }

    void GameOver()
    {
        gameOver.enabled = true;
        animManager.animator.enabled = false;
        HideDust();
        cherryController.gameIsPlaying = false;

        ghostManager.DisableState();
        pelletManager.CheckHighScore(time);
    }
}