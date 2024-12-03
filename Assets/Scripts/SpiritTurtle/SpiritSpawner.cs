using UnityEngine;
using System.Collections.Generic;

public class SpiritSpawner : MonoBehaviour
{
    public GameObject spiritPrefab; // Reference to the Spirit prefab
    public GameObject emptySpacePrefab; // Reference to the EmptySpace prefab
    public List<Transform> spawnPoints; // List of spawn points

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        if (spawnPoints.Count < 5)
        {
            Debug.LogError("Ensure there are exactly 5 spawn points in the list.");
            return;
        }

        // Create a list of indices for the spawn points
        List<int> indices = new List<int> { 0, 1, 2, 3, 4 };
        
        // Shuffle the indices to randomize the spawn locations
        ShuffleList(indices);

        // Place 3 spirits at random locations
        for (int i = 0; i < 3; i++)
        {
            Instantiate(spiritPrefab, spawnPoints[indices[i]].position, Quaternion.identity);
        }

        // Place 2 empty spaces at the remaining locations
        for (int i = 3; i < indices.Count; i++)
        {
            Instantiate(emptySpacePrefab, spawnPoints[indices[i]].position, Quaternion.identity);
        }
    }

    private void ShuffleList(List<int> list)
    {
        // Fisher-Yates Shuffle
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
