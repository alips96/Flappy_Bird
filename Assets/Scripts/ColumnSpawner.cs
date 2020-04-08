using System;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public GameObject columnPrefab;
    private float yPos;
    private float nextCheck;
    private float checkRate = 2;
    private float deductionRate;

    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnColumns();
    }

    void SpawnColumns()
    {
        if(Time.time > nextCheck)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, UnityEngine.Random.Range(yPos - 1, yPos + 2.8f));
            GameObject column = Instantiate(columnPrefab, spawnPos, Quaternion.identity);
            SetColumnSize(column); // To adjust difficulty level.
            Destroy(column, 3.75f);
            nextCheck = Time.time + checkRate;
        }

    }

    private void SetColumnSize(GameObject column)
    {
        Transform[] transforms = column.GetComponentsInChildren<Transform>();

        foreach (Transform item in transforms)
        {
            if (item.position.y > 0)
            {
                item.position = new Vector3(item.position.x, item.position.y - deductionRate, item.position.z);
            }
            else
            {
                item.position = new Vector3(item.position.x, item.position.y + deductionRate, item.position.z);
            }
        }

        if (Mathf.Abs(transforms[0].position.y - transforms[1].position.y) > 3.9f) // stop the game from getting to hard.
        {
            deductionRate += .02f;
        }
    }
}
