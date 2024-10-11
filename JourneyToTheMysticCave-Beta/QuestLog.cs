using System;
using System.Collections.Generic;

namespace JourneyToTheMysticCave_Beta
{
    public class QuestLog
    {
        private List<Quest> quests = new List<Quest>();

        public void AddQuest(Quest quest)
        {
            quests.Add(quest);
        }

        public void RegisterKill(string enemyType, int enemyHealth)
        {
            foreach (var quest in quests)
            {
                quest.RegisterKill(enemyType, enemyHealth);
            }
        }

        public void DisplayLog()
        {
            foreach (var quest in quests)
            {
                string status = quest.IsComplete ? "Completed" : $"{quest.KillCount}/{quest.KillTarget} kills";
                Console.WriteLine($"{quest.Name}: {quest.Description} - {status}");
            }
        }
        
        public List<Quest> GetQuests()
        {
            return quests;
        }
    }
}