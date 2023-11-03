using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PelletManager : MonoBehaviour
{
    public GhostManager ghostManager;

    public Tilemap tilemap;
    public TileBase emptyTile;

    public Text pointsTxt;
    private int currentPoints = 0;
    public int savedPoints = 0;

    public Text timerTxt;
    private float timer = 10;

    private int totalPellets = 222;

    public void RemovePellet(Vector3Int pos)
    {
        tilemap.SetTile(pos, emptyTile);
        UpdateScore();
    }

    void UpdateScore()
    {
        totalPellets--;
        currentPoints += 10;
        savedPoints += 10;
        pointsTxt.text = savedPoints.ToString();
        SavePoints();
    }

    public void EatCherry()
    {
        currentPoints += 100;
        savedPoints += 100;
        pointsTxt.text = savedPoints.ToString();
        SavePoints();
    }

    public void EatPowerPellet()
    {
        totalPellets--;
        StartCoroutine(StartGhostTimer());
        ghostManager.TriggerScaredState();
    }

    public bool PelletsGone()
    {
        if (totalPellets == 0)
            return true;

        return false;
    }

    public IEnumerator StartGhostTimer()
    {
        timer = 10;
        timerTxt.enabled = true;

        while (timer > 0)
        {
            timerTxt.text = timer.ToString();
            yield return new WaitForSeconds(1);
            timer -= 1;

            if (timer == 3)
                ghostManager.TriggerRecoveringState();
        }

        yield return null;

        timer = 0;
        timerTxt.text = "0";
        timerTxt.enabled = false;
        ghostManager.TriggerWalkingState();
    }

    void SavePoints() {
        PlayerPrefs.SetInt("points", savedPoints);
    }

    public void CheckHighScore(float time)
    {
        if (currentPoints > PlayerPrefs.GetInt("points"))
            PlayerPrefs.SetInt("points", currentPoints);

        else if (currentPoints == PlayerPrefs.GetInt("points") && time > PlayerPrefs.GetFloat("elapsedTime"))
            PlayerPrefs.SetInt("points", currentPoints);

    }
}
