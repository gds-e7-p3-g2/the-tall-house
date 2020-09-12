using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public GameObject spawnPoint;

    public void Spawn()
    {
        Instantiate(ghostPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
