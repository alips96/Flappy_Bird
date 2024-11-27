using Unity.Netcode;
using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    private readonly float yPos;
    private float nextCheck;
    [SerializeField] private float SpawnRate = 2;
    [SerializeField] private float shrinkRate = 0.02f;
    [SerializeField] private float minDistanceBetweenColumns = 3.9f;
    private float columnsOffset;

    void Update()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            if (Time.time > nextCheck)
            {
                Vector3 spawnPos = new Vector2(transform.position.x, Random.Range(yPos - 1, yPos + 2.8f));
                SpawnColumns(spawnPos);

                nextCheck = Time.time + SpawnRate;
            }
        }
    }

    private void SpawnColumns(Vector3 spawnPos)
    {
        GameObject column = Instantiate(columnPrefab, spawnPos, Quaternion.identity);
        SetColumnSize(column); // To adjust difficulty level.

        column.GetComponent<NetworkObject>().Spawn(true);

        //Destroy(column, 3.75f);
    }

    private void SetColumnSize(GameObject column)
    {
        Transform[] transforms = column.GetComponentsInChildren<Transform>();

        foreach (Transform item in transforms)
        {
            if (item.position.y > 0)
            {
                item.position = new Vector3(item.position.x, item.position.y - columnsOffset, item.position.z);
            }
            else
            {
                item.position = new Vector3(item.position.x, item.position.y + columnsOffset, item.position.z);
            }
        }

        if (Mathf.Abs(transforms[0].position.y - transforms[1].position.y) > minDistanceBetweenColumns) // stop the game from getting too hard.
        {
            columnsOffset += shrinkRate;
        }
    }
}
