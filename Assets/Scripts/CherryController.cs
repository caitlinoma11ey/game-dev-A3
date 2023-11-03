using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public Tweener tweener;
    public GameObject cherryPrefab;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("PlaceBonusCherry", 0f, 10f);
    }

    void PlaceBonusCherry()
    {
        Vector3 position = GetRandomPosition();
        GameObject cherry = Instantiate(cherryPrefab, position, Quaternion.identity);

        if (!tweener.TweenExists(cherry.transform))
            tweener.AddTween(cherry.transform, position, -position, 20f);

        if (!tweener.TweenExists(cherry.transform))
            StartCoroutine(DestroyCherry(cherry));
    }


    IEnumerator DestroyCherry(GameObject cherry)
    {
        while (tweener.isLerping(cherry.transform))
        {
            // Do nothing
            yield return null;
        }

        Destroy(cherry);
    }

    public void EatCherry(GameObject cherry)
    {
        Destroy(cherry);
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
                x = Random.Range(-width, width);
                y = height + 2f;
                break;

            case 1:
                x = Random.Range(-width, width);
                y = -height - 2f;
                break;

            case 2:
                x = height + 2f;
                y = Random.Range(-height, height);
                break;

            case 3:
                x = -height - 2f;
                y = Random.Range(-height, height);
                break;
        }
        return new Vector3(x, y, 0);
    }
}
