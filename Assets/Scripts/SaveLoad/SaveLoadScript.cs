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

    void Start()
    {
        GameManager.Instance.saveFilePath = Application.persistentDataPath + "/savefile.json";
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            coins = GameManager.Instance.coins,
            quest = DaySystem.instance.currentQuest,
            day = DaySystem.instance.dayNumber
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

        foreach(var quests in GameManager.Instance.questsInGame)
        {
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

        //save for all quest objects and their data

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

            DaySystem.instance.dayNumber = saveData.day;

            DaySystem.instance.currentQuest = saveData.quest;

            // Clear and refill the petObjects list
            GameManager.Instance.petObjects.Clear();
            GameManager.Instance.questsInGame.Clear();

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

            foreach (var quests in saveData.savedQuests)
            {
                // Find the existing ScriptableObject by petName
                QuestSO existingQuest = Resources.Load<QuestSO>($"Quests/{quests.questName}");

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

                    // Add to GameManager's list
                    GameManager.Instance.questsInGame.Add(existingQuest);
                }
                else
                {
                    Debug.LogWarning($"Pet with name '{quests.questName}' not found in Resources folder.");
                }
            }

            //load quest SOs

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }
}
