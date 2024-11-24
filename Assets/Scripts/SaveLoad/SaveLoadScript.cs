using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadScript : MonoBehaviour
{
    [System.Serializable]
    private class SaveData
    {
        public List<SavedPet> petList = new List<SavedPet>();
        public List<SavedQuests> savedQuests = new List<SavedQuests>();
        public List<SavedInventorySlot> inventorySlots = new List<SavedInventorySlot>();
        public int coins;
        public int day;
        public QuestSO quest;
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

    [System.Serializable]
    private class SavedQuests
    {
        public int questID, goal1ID, goal2ID, goal1Max, goal2Max, goal1Progress, goal2Progress;
        public string questName, questRequirements, questGoal1Words, questGoal2Words;
        public string questGiver;
        public bool questFinished, questTurnedIn, questAccepted;
    }

    [System.Serializable]
    private class SavedInventorySlot
    {
        public List<SavedInventoryItem> items = new List<SavedInventoryItem>();
    }

    [System.Serializable]
    private class SavedInventoryItem
    {
        public string itemName; // Used to find the correct ScriptableObject or prefab
        public int itemCount;
    }

    void Start()
    {
        GameManager.Instance.saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            coins = GameManager.Instance.coins,


        };
        if (DaySystem.instance != null)
        {
            saveData.day = DaySystem.instance.dayNumber;
            saveData.quest = DaySystem.instance.currentQuest;
        }
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

        // Save inventory
        // Save inventory
        InventoryManager inventoryManager = GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>();
        foreach (var slot in inventoryManager.inventorySlots)
        {
            SavedInventorySlot savedSlot = new SavedInventorySlot();

            foreach (var inventoryItem in slot.GetComponentsInChildren<InventoryItem>())
            {
                SavedInventoryItem savedItem = new SavedInventoryItem
                {
                    itemName = inventoryItem.item.name, // Assuming InventoryItem has itemName
                    itemCount = inventoryItem.count
                };
                savedSlot.items.Add(savedItem);
            }

            saveData.inventorySlots.Add(savedSlot);
        }


        // Save quests
        if (DaySystem.instance != null)
        {


            for (int i = 0; i < DaySystem.instance.allQuests.Length; i++)
            {
                var quests = DaySystem.instance.allQuests[i];

                saveData.savedQuests.Add(new SavedQuests
                {
                    questID = quests.questID,
                    goal1ID = quests.goal1ID,
                    goal2ID = quests.goal2ID,
                    goal1Max = quests.goal1Max,
                    goal2Max = quests.goal2Max,
                    goal1Progress = quests.goal1Progress,
                    goal2Progress = quests.goal2Progress,
                    questName = quests.questName,
                    questRequirements = quests.questRequirements,
                    questGoal1Words = quests.questGoal1Words,
                    questGoal2Words = quests.questGoal2Words,
                    questGiver = quests.questGiver,
                    questFinished = quests.questFinished,
                    questTurnedIn = quests.questTurnedIn,
                    questAccepted = quests.questAccepted
                });
            }
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

            // Load coins and quest data
            GameManager.Instance.coins = saveData.coins;
            if(DaySystem.instance != null)
            {
                DaySystem.instance.dayNumber = saveData.day;
                DaySystem.instance.currentQuest = saveData.quest;
            }


            // Load pets
            GameManager.Instance.petObjects.Clear();
            foreach (var savedPet in saveData.petList)
            {
                PetObject existingPet = Resources.Load<PetObject>($"Pets/{savedPet.petName}");
                if (existingPet != null)
                {
                    existingPet.happiness = savedPet.happiness;
                    existingPet.maxHapp = savedPet.maxHapp;
                    existingPet.fullness = savedPet.fullness;
                    existingPet.maxFull = savedPet.maxFull;
                    existingPet.level = savedPet.level;

                    GameManager.Instance.petObjects.Add(existingPet);
                }
            }

            // Load inventory
            InventoryManager inventoryManager = GameManager.Instance.inventory.GetComponentInChildren<InventoryManager>();
            inventoryManager.ClearInventory(); // Ensure the inventory is cleared before loading new items

            foreach (var savedSlot in saveData.inventorySlots)
            {
                foreach (var savedItem in savedSlot.items)
                {
                    Item loadedItem = Resources.Load<Item>($"InventoryItems/{savedItem.itemName}");
                    if (loadedItem != null)
                    { 
                        for(int i = 0; i < savedItem.itemCount; i++)
                        inventoryManager.AddItem(loadedItem);
                    }
                    else
                    {
                        Debug.LogWarning($"Item '{savedItem.itemName}' not found in Resources folder.");
                    }
                }
            }


            // Load quests
            for (int i = 0; i < saveData.savedQuests.Count; i++)
            {
                var quests = saveData.savedQuests[i];

                QuestSO existingQuest = Resources.Load<QuestSO>($"Quests/QuestSO/{quests.questName}");
                if (existingQuest != null)
                {
                    existingQuest.questID = quests.questID;
                    existingQuest.goal1ID = quests.goal1ID;
                    existingQuest.goal2ID = quests.goal2ID;
                    existingQuest.goal1Max = quests.goal1Max;
                    existingQuest.goal2Max = quests.goal2Max;
                    existingQuest.goal1Progress = quests.goal1Progress;
                    existingQuest.goal2Progress = quests.goal2Progress;
                    existingQuest.questName = quests.questName;
                    existingQuest.questRequirements = quests.questRequirements;
                    existingQuest.questGoal1Words = quests.questGoal1Words;
                    existingQuest.questGoal2Words = quests.questGoal2Words;
                    existingQuest.questGiver = quests.questGiver;
                    existingQuest.questFinished = quests.questFinished;
                    existingQuest.questTurnedIn = quests.questTurnedIn;
                    existingQuest.questAccepted = quests.questAccepted;
                    if (DaySystem.instance != null)
                    {
                        DaySystem.instance.allQuests[i] = existingQuest;
                    }
                }
                else
                {
                    Debug.LogWarning($"Quest with name '{quests.questName}' not found in Resources folder.");
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
