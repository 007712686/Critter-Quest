using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadScript : MonoBehaviour
{
    [System.Serializable]
    private class SaveData
    {
        public List<SavedPet> petList = new List<SavedPet>();
        public int coins;
    }

    [System.Serializable]
    private class SavedPet
    {
        public string petName; // Used to find the correct ScriptableObject
        public float happiness;
        public float maxHapp;
        public float fullness;
        public float maxFull;
        public float level;
    }

    void Start()
    {
        GameManager.Instance.saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            coins = GameManager.Instance.coins
        };

        // Save all pet objects and their data
        foreach (var pet in GameManager.Instance.petObjects)
        {
            saveData.petList.Add(new SavedPet
            {
                petName = pet.petName,
                happiness = pet.happiness,
                maxHapp = pet.maxHapp,
                fullness = pet.fullness,
                maxFull = pet.maxFull,
                level = pet.level
            });
        }

        // Serialize to JSON and write to file
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(GameManager.Instance.saveFilePath, json);
        Debug.Log("Game Saved to " + GameManager.Instance.saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(GameManager.Instance.saveFilePath))
        {
            string json = File.ReadAllText(GameManager.Instance.saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // Load coins
            GameManager.Instance.coins = saveData.coins;

            // Clear and refill the petObjects list
            GameManager.Instance.petObjects.Clear();

            foreach (var savedPet in saveData.petList)
            {
                // Find the existing ScriptableObject by petName
                PetObject existingPet = Resources.Load<PetObject>($"Pets/{savedPet.petName}");

                if (existingPet != null)
                {
                    // Update the ScriptableObject's data
                    existingPet.happiness = savedPet.happiness;
                    existingPet.maxHapp = savedPet.maxHapp;
                    existingPet.fullness = savedPet.fullness;
                    existingPet.maxFull = savedPet.maxFull;
                    existingPet.level = savedPet.level;

                    // Add to GameManager's list
                    GameManager.Instance.petObjects.Add(existingPet);
                }
                else
                {
                    Debug.LogWarning($"Pet with name '{savedPet.petName}' not found in Resources folder.");
                }
            }

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }
}
