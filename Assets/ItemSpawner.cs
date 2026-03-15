using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject healthPackPrefab;

    public void SpawnHealthPack(Vector3 position)
    {
        Instantiate(healthPackPrefab, position, Quaternion.identity);
    }
}