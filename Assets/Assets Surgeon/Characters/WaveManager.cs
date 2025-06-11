using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    public GameObject followerPrefab;
    public Transform target;
    public int initialCount = 5;
    public int newWaveCount = 5;
    public float spawnAmplitude = 10f;
    public float terrainX = 20f;
    public float terrainZ = 20f;
    public Vector3 center = Vector3.zero; // Centrage du terrain

    public AudioClip waveStartSound; // 🔊 Son à jouer
    public AudioSource audioSource; // 🎚️ Composant AudioSource

    private int currentCount = 0;
    public List<GameObject> currentFollowers = new();

    private bool waveInProgress = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        StartNewWave(); // Lancer la premi�re vague automatiquement
    }

    void Update()
    {
        if (waveInProgress && currentFollowers.Count == 0)
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        if (waveStartSound != null)
        {
            audioSource.PlayOneShot(waveStartSound);
        }

        currentCount = (currentCount == 0) ? initialCount : currentCount + newWaveCount;

        for (int i = 0; i < currentCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            GameObject follower = Instantiate(followerPrefab, spawnPos, Quaternion.identity);
            follower.GetComponent<Follower>().target = target;
            currentFollowers.Add(follower);

            follower.GetComponent<SurgeonDeathController>().SetOnDeathCallback(() =>
            {
                currentFollowers.Remove(follower);
                Destroy(follower);
            });
        }

        waveInProgress = true;
    }

    Vector3 GetRandomSpawnPosition()
    {
        for (int i = 0; i < 10; i++) // 10 tentatives max
        {
            float x = 0f;
            float z = 0f;

            if (Random.value > 0.5f)
            {
                float xMin = terrainX;
                float xMax = terrainX + spawnAmplitude;
                x = (Random.value > 0.5f) ? Random.Range(xMin, xMax) : Random.Range(-xMax, -xMin);
                z = Random.Range(-terrainZ - spawnAmplitude, terrainZ + spawnAmplitude);
            }
            else
            {
                float zMin = terrainZ;
                float zMax = terrainZ + spawnAmplitude;
                z = (Random.value > 0.5f) ? Random.Range(zMin, zMax) : Random.Range(-zMax, -zMin);
                x = Random.Range(-terrainX - spawnAmplitude, terrainX + spawnAmplitude);
            }

            Vector3 candidate = new Vector3(x, 5f, z); // y=5 pour �viter d��tre dans le sol

            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }

        Debug.LogWarning("No valid NavMesh spawn position found!");
        return center; // fallback pour �viter crash
    }
}
