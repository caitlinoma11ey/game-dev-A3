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
    private float points = 0;

    public Text timerTxt;
    private float timer = 10;
    private bool ghostTimerActive = false;

    public void RemovePellet(Vector3Int pos)
    {
        tilemap.SetTile(pos, emptyTile);
        Invoke("UpdateScore", 0.1f);
    }

    void UpdateScore ()
    {
        points += 10;

        pointsTxt.text = points.ToString();
    }

    public void EatCherry()
    {
        points += 100;
        pointsTxt.text = points.ToString();
    }

    public void EatPowerPellet()
    {
        if (!ghostTimerActive)
        {
            StartCoroutine(StartGhostTimer());
            ghostManager.TriggerScaredState();
        }
    }

    public IEnumerator StartGhostTimer()
    {
        ghostTimerActive = true;

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
        ghostTimerActive = false;

    }
}
