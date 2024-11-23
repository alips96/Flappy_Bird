using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    private float yPos;
    private float nextCheck;
    private float checkRate = 2;
    private float deductionRate;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextCheck)
        {
            Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(yPos - 1, yPos + 2.8f));
            SpawnColumns(spawnPos);

            nextCheck = Time.time + checkRate;
        }
    }

    private void SpawnColumns(Vector3 spawnPos)
    {
        GameObject column = Instantiate(columnPrefab, spawnPos, Quaternion.identity);
        SetColumnSize(column); // To adjust difficulty level.
        Destroy(column, 3.75f);
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
