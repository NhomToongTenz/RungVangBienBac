using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject wavePrefab; // The wave prefab to spawn
    public Transform spawnPoint; // Starting point for the waves
    public float spawnInterval = 10f; // Time between each wave spawn
    public float waveSpeed = 5f; // Speed at which the wave moves

    private void Start()
    {
        InvokeRepeating(nameof(SpawnWave), 0f, spawnInterval);
    }

    private void SpawnWave()
    {
        GameObject wave = Instantiate(wavePrefab, spawnPoint.position, Quaternion.identity);
        wave.GetComponent<Wave>().SetSpeed(waveSpeed);
    }
}
