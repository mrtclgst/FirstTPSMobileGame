using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    [SerializeField] Transform[] spawnPoints;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public Transform GetSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform;
    }
    internal void Spawn(GameObject players)
    {
        players.transform.position = GetSpawnPoint().position;
        players.transform.rotation = GetSpawnPoint().rotation;
        players.gameObject.SetActive(true);
    }
}
