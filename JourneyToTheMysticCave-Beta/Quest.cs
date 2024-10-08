namespace JourneyToTheMysticCave_Beta
{
    public class Quest
    {
        public string questName { get; set; }
        public string questDescription { get; set; }
        public int KillCount { get; set; }
        public int KillGoal { get; set; }
        public bool questComplete { get; set; }
        
        public Quest(string questName, string questDescription, int killCount, int killGoal, bool questComplete)
        {
            this.questName = questName;
            this.questDescription = questDescription;
            this.KillCount = killCount;
            this.KillGoal = killGoal;
            this.questComplete = questComplete;
        }
        
        public void Update()
        {
            if (!questComplete)
            {
                if (KillCount >= KillGoal)
                {
                    questComplete = true;
                }
            }
        }
        
        
    }
}