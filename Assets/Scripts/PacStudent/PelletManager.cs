using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PelletManager : MonoBehaviour
{
    public CherryController cherryController;

    public Tilemap tilemap;
    public TileBase emptyTile;

    public Text pointsTxt; 
    public float points = 0;

    public void RemovePellet(Vector3Int pos)
    {
        tilemap.SetTile(pos, emptyTile);
        Invoke("UpdateScore", 0.3f);
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    gameObject.transform.position = -gameObject.transform.position;
    //}
}
