using System;
using System.IO;
using System.Text.Json;

namespace JourneyToTheMysticCave_Beta
{
    internal class GameStats
    {
        LevelManager levelManager;
        Random random = new Random();
        Map map;
        Item item;

        #region PlayerStat Declarations
        public string PlayerName { get; set; }
        public char PlayerCharacter { get; set; }
        public int PlayerDamage { get; set; }
        public int PlayerHealth { get; set; }
        public Point2D PlayerPos { get; set; }

        #endregion

        #region RangerStat Declarations
        public int RangerCount { get; set; }
        public char RangedCharacter { get; set; }
        public string RangerName { get; set; }
        public int RangerDamage { get; set; }
        private int rangerMaxHp;
        private int rangerMinHp;
        public string RangerAttack { get; set; }
        #endregion

        #region MageStat Declarations
        public int MageCount { get; set; }
        public char MageCharacter { get; set; }
        public string MageName { get; set; }
        public int MageDamage { get; set; }
        private int mageMaxHp;
        private int mageMinHp;
        public string MageAttack { get; set; }
        #endregion

        #region MeleeStat Declarations
        public int MeleeCount { get; set; }
        public char MeleeCharacter { get; set; }
        public string MeleeName { get; set; }
        public int MeleeDamage { get; set; }
        private int meleeMaxHp;
        private int meleeMinHp;
        public string MeleeAttack { get; set; }
        #endregion

        #region BossStat Declarations
        public int BossCount { get; set; }
        public char BossCharacter { get; set; }
        public string BossName { get; set; }
        public int BossDamage { get; set; }
        public int BossHealth { get; set; }
        public string BossAttack { get; set; }
        #endregion

        #region MoneyStat Declarations
        public int MoneyCount { get; set; }
        public char MoneyCharacter { get; set; }
        public string MoneyName { get; set; }
        
        public int MoneyValue { get; set; }
        #endregion

        #region PotionStat Declarations
        public int PotionCount { get; set; }
        public char PotionCharacter { get; set; }
        public string PotionName { get; set; }
        public int PotionHeal { get; set; }
        #endregion

        #region TrapStat Declarations
        public int TrapCount { get; set; }
        public char TrapCharacter { get; set; }
        public string TrapName { get; set; }
        public int TrapDamage { get; set; }
        #endregion

        #region SwordStat Declarations
        public int SwordCount { get; set; }
        public char SwordCharacter { get; set; }
        public string SwordName { get; set; }
        public int SwordMultiplier { get; set; }
        #endregion
        
        #region ShopStat Declarations
        public int ShopCount { get; set; }
        public char ShopCharacter { get; set; }
        public string ShopName { get; set; }
        
        #endregion

        public int PoisonDamage;

        public void Init(LevelManager levelManager, Map map, Item item)
        {
            this.levelManager = levelManager;
            this.map = map;
            this.item = item;

            GameConfig();

            try
            {
                string jsonString = File.ReadAllText("Data/GameData.json");
                GameStats gameStats = JsonSerializer.Deserialize<GameStats>(jsonString);
                if (gameStats != null)
                {
                    PlayerName = gameStats.PlayerName;
                    PlayerCharacter = gameStats.PlayerCharacter;
                    PlayerDamage = gameStats.PlayerDamage;
                    PlayerHealth = gameStats.PlayerHealth;
                    RangerAttack = gameStats.RangerAttack;
                    RangerCount = gameStats.RangerCount;
                    RangedCharacter = gameStats.RangedCharacter;
                    RangerName = gameStats.RangerName;
                    RangerDamage = gameStats.RangerDamage;
                    rangerMaxHp = gameStats.rangerMaxHp;
                    rangerMinHp = gameStats.rangerMinHp;
                    MageAttack = gameStats.MageAttack;
                    MageCount = gameStats.MageCount;
                    MageCharacter = gameStats.MageCharacter;
                    MageName = gameStats.MageName;
                    MageDamage = gameStats.MageDamage;
                    mageMaxHp = gameStats.mageMaxHp;
                    mageMinHp = gameStats.mageMinHp;
                    MeleeAttack = gameStats.MeleeAttack;
                    MeleeCount = gameStats.MeleeCount;
                    MeleeCharacter = gameStats.MeleeCharacter;
                    MeleeName = gameStats.MeleeName;
                    MeleeDamage = gameStats.MeleeDamage;
                    meleeMaxHp = gameStats.meleeMaxHp;
                    meleeMinHp = gameStats.meleeMinHp;
                    BossAttack = gameStats.BossAttack;
                    BossCount = gameStats.BossCount;
                    BossCharacter = gameStats.BossCharacter;
                    BossName = gameStats.BossName;
                    BossDamage = gameStats.BossDamage;
                    BossHealth = gameStats.BossHealth;
                    MoneyCount = gameStats.MoneyCount;
                    MoneyCharacter = gameStats.MoneyCharacter;
                    MoneyName = gameStats.MoneyName;
                    MoneyValue = gameStats.MoneyValue;
                    PotionCount = gameStats.PotionCount;
                    PotionCharacter = gameStats.PotionCharacter;
                    PotionName = gameStats.PotionName;
                    PotionHeal = gameStats.PotionHeal;
                    TrapCount = gameStats.TrapCount;
                    TrapCharacter = gameStats.TrapCharacter;
                    TrapName = gameStats.TrapName;
                    TrapDamage = gameStats.TrapDamage;
                    SwordCount = gameStats.SwordCount;
                    SwordCharacter = gameStats.SwordCharacter;
                    SwordName = gameStats.SwordName;
                    SwordMultiplier = gameStats.SwordMultiplier;
                    ShopCount = gameStats.ShopCount;
                    ShopCharacter = gameStats.ShopCharacter;
                    ShopName = gameStats.ShopName;
                    PoisonDamage = gameStats.PoisonDamage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading JSON file: {ex.Message}");
            }
        }

        public void GameConfig()
        {
            PlayerPos = new Point2D { x = 2, y = 5 };
        }

        public int GiveHealth(Random random, string type)
        {
            int health;
            switch (type)
            {
                case "Ranger":
                    health = random.Next(rangerMinHp, rangerMaxHp);
                    return (health);
                case "Mage":
                    health = random.Next(mageMinHp, mageMaxHp);
                    return (health);
                case "Melee":
                    health = (random.Next(meleeMinHp, meleeMaxHp));
                    return (health);
                default: return 0;
            }
        }

        public Point2D PlaceCharacters(int levelNumber, Random random)
        {
            int x, y;

            do
            {
                x = random.Next(0, levelManager.AllMapContents[levelNumber].GetLength(1));
                y = random.Next(0, levelManager.AllMapContents[levelNumber].GetLength(0));
            } while (!CheckInitialPlacement(x, y, levelNumber));

            return new Point2D { x = x, y = y };
        }

        private bool CheckInitialPlacement(int x, int y, int levelNumber)
        {
            return levelManager.InitialBoundaries(x, y, levelNumber) && map.EmptySpace(x,y, levelNumber);
        }
    }
}