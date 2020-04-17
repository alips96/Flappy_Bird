using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ColumnSpawner : MonoBehaviour
{
    [SerializeField] private GameObject columnPrefab;
    private float yPos;
    private float nextCheck;
    private float checkRate = 2;
    private float deductionRate;

    private void OnEnable()
    {
        yPos = transform.position.y;
        PhotonNetwork.NetworkingClient.EventReceived += SpawnColumns;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= SpawnColumns;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextCheck)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, Random.Range(yPos - 1, yPos + 2.8f));
                PhotonNetwork.RaiseEvent(1, spawnPos, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = false });
            }
            nextCheck = Time.time + checkRate;
        }
    }

    private void SpawnColumns(EventData obj)
    {
        if (obj.Code != 1)
            return;

        Vector3 spawnPos = (Vector3) obj.CustomData;
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
