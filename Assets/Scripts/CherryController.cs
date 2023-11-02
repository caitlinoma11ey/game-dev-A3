using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public Tweener tweener;
    public GameObject cherryPrefab;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("PlaceBonusCherry", 1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void PlaceBonusCherry()
    {
        Vector3 position = GetRandomPosition();
        GameObject cherry = Instantiate(cherryPrefab, position, Quaternion.identity);

        if (!tweener.TweenExists(cherry.transform))
        {
            Vector3 endPos = new Vector3(-position.x, -position.y, 0);

            tweener.AddTween(cherry.transform, position, endPos, 5f);
        }
    }

    Vector3 GetRandomPosition()
    {
        float height = cam.orthographicSize * cam.aspect + 1;
        float width = cam.orthographicSize + 1;

        float x = 0;
        float y = 0;

        int randomNum = Random.Range(0, 4);

        switch (randomNum)
        {
            case 0:
                x = Random.Range(width, width + 2);
                y = height + 2f;
                break;

            case 1:
                x = Random.Range(width, width + 2);
                y = -height - 2f;
                break;

            case 2:
                x = height + 2f;
                y = Random.Range(height, height + 2);
                break;

            case 3:
                x = -height - 2f;
                y = Random.Range(-height, height - 2);
                break;
        }
        return new Vector3(x, y, 0);
    }
}
