using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    public GameObject followerPrefab;
    public Transform spawnCenter;
    public Transform target;

    public float terrainLength = 10f; // X axis size
    public float terrainWidth = 10f;  // Z axis size
    public float spawnAmplitude = 5f; // Area around the terrain
    public int initialCount = 5;

    private int currentCount;
    private List<GameObject> currentFollowers = new List<GameObject>();

    void Start()
    {
        currentCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        // Clear previous followers
        foreach (var follower in currentFollowers)
        {
            if (follower != null)
                Destroy(follower);
        }
        currentFollowers.Clear();

        // Double count (except first wave)
        if (currentCount == 0)
            currentCount = initialCount;
        else
            currentCount *= 2;

        for (int i = 0; i < currentCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            GameObject newFollower = Instantiate(followerPrefab, spawnPos, Quaternion.identity);

            // Assigner la cible si le script existe
            Follower followerScript = newFollower.GetComponent<Follower>();
            if (followerScript != null)
            {
                followerScript.target = target;
            }

            currentFollowers.Add(newFollower);
        }

        Debug.Log($"Vague lancée : {currentCount} followers");
    }

    Vector3 GetRandomSpawnPosition()
    {
        float halfLength = terrainLength / 2f;
        float halfWidth = terrainWidth / 2f;

        // Choisir un côté au hasard : 0=haut, 1=bas, 2=gauche, 3=droite
        int side = Random.Range(0, 4);
        float x = 0f, z = 0f;

        switch (side)
        {
            case 0: // Haut (devant le terrain)
                x = Random.Range(-halfLength - spawnAmplitude, halfLength + spawnAmplitude);
                z = halfWidth + Random.Range(0, spawnAmplitude);
                break;
            case 1: // Bas
                x = Random.Range(-halfLength - spawnAmplitude, halfLength + spawnAmplitude);
                z = -halfWidth - Random.Range(0, spawnAmplitude);
                break;
            case 2: // Gauche
                x = -halfLength - Random.Range(0, spawnAmplitude);
                z = Random.Range(-halfWidth, halfWidth);
                break;
            case 3: // Droite
                x = halfLength + Random.Range(0, spawnAmplitude);
                z = Random.Range(-halfWidth, halfWidth);
                break;
        }

        Vector3 spawnPos = spawnCenter.position + new Vector3(x, 0f, z);
        return spawnPos;
    }
}
