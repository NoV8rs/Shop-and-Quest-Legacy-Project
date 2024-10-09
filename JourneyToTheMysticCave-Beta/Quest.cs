namespace JourneyToTheMysticCave_Beta
{
    public class Quest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int KillCount { get; set; }
        public int KillTarget { get; set; }
        public bool IsComplete { get; set; }
        public string EnemyType { get; set; }

        public Quest(string name, string description, int killTarget, string enemyType)
        {
            Name = name;
            Description = description;
            KillTarget = killTarget;
            KillCount = 0;
            IsComplete = false;
            EnemyType = enemyType;
        }

        public void RegisterKill(string enemyType, int enemyHealth)
        {
            if (!IsComplete && enemyType == EnemyType && enemyHealth <= 0)
            {
                KillCount++;
                if (KillCount >= KillTarget)
                {
                    IsComplete = true;
                }
            }
        }
    }
}