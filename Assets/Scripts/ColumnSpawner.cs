using UnityEngine;

public class ColumnSpawner : MonoBehaviour
{
    public GameObject columnPrefab;
    private float yPos;
    private float nextCheck;
    private float checkRate = 2;

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
            Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(yPos - 1, yPos + 2.8f));
            GameObject column = Instantiate(columnPrefab, spawnPos, Quaternion.identity);
            Destroy(column, 3.75f);
            nextCheck = Time.time + checkRate;
        }

    }
}
