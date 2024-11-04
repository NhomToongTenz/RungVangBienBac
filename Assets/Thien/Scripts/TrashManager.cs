using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class TrashManager : MonoBehaviour
{
    public GameObject trashPrefab;
    public Transform[] trashSpawnPoints; // Array of predefined spawn points
    public TextMeshProUGUI trashText;

    private int trashCollected = 0;
    private int trashGoal = 5;

    void Start()
    {
        SpawnAllTrash();
        UpdateTrashText();
    }

    void SpawnAllTrash()
    {
        foreach (Transform spawnPoint in trashSpawnPoints)
        {
            StartCoroutine(SpawnTrashAtPoint(spawnPoint));
        }
    }

    private IEnumerator SpawnTrashAtPoint(Transform spawnPoint)
    {
        while (true) // Loop indefinitely
        {
            // Wait for a random time between 12 and 20 seconds
            float waitTime = Random.Range(12f, 20f);
            yield return new WaitForSeconds(waitTime);

            // Spawn the trash
            Instantiate(trashPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void CollectTrash()
    {
        trashCollected++;
        UpdateTrashText();

        if (trashCollected >= trashGoal)
        {
            trashText.text = "Trash Collected: 5/5\nQuest Completed!";
        }
    }

    private void UpdateTrashText()
    {
        trashText.text = $"Trash Collected: {trashCollected}/{trashGoal}";
    }
}
