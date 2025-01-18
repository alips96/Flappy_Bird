using System.Linq;
using UniRx;
using Unity.Netcode;
using UnityEngine;
using Observable = UniRx.Observable;

public class ColumnSpawner : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float shrinkRate = 0.02f;
    [SerializeField] private float minDistanceBetweenColumns = 3.9f;

    private float columnsOffset;
    private readonly float yPos;

    private void Start()
    {
        if (!NetworkManager.Singleton.IsServer) return;

        Observable.Interval(System.TimeSpan.FromSeconds(spawnRate))
            .Subscribe(_ => SpawnColumn())
            .AddTo(this);
    }

    private void SpawnColumn()
    {
        Vector3 spawnPos = new Vector2(transform.position.x, Random.Range(yPos - 1, yPos + 2.8f));
        GameObject column = Instantiate(columnPrefab, spawnPos, Quaternion.identity);
        SetColumnSize(column);
        column.GetComponent<NetworkObject>().Spawn(true);
    }

    private void SetColumnSize(GameObject column)
    {
        var transforms = column.GetComponentsInChildren<Transform>();

        foreach (var item in transforms)
        {
            item.position = new Vector3(item.position.x,
                item.position.y + (item.position.y > 0 ? -columnsOffset : columnsOffset),
                item.position.z);
        }

        if (Mathf.Abs(transforms.First().position.y - transforms.Last().position.y) > minDistanceBetweenColumns)
        {
            columnsOffset += shrinkRate;
        }
    }
}
