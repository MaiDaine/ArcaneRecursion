using System;

namespace ArcaneRecursion
{
    [Serializable]
    public class UnitStats
    {
        public int CombatSpeed;
        public int MovementSpeed;
        public int ActionPoints;
        public int HealthPoints;
        public int ManaPoints;

        #region Init
        public UnitStats()
        {
            CombatSpeed = 0;
            MovementSpeed = 0;
            ActionPoints = 0;
            HealthPoints = 0;
            ManaPoints = 0;
        }

        public UnitStats(UnitStats other)
        {
            CombatSpeed = other.CombatSpeed;
            MovementSpeed = other.MovementSpeed;
            ActionPoints = other.ActionPoints;
            HealthPoints = other.HealthPoints;
            ManaPoints = other.ManaPoints;
        }

        public static UnitStats DefaultPercentModifier()
        {
            return new UnitStats()
            {
                CombatSpeed = 100,
                MovementSpeed = 100,
                ActionPoints = 100,
                HealthPoints = 100,
                ManaPoints = 100,
            };
        }
        #endregion /* Init */

        public void ApplyModifier(UnitStats other)
        {
            CombatSpeed += other.CombatSpeed;
            MovementSpeed += other.MovementSpeed;
            ActionPoints += other.ActionPoints;
            HealthPoints += other.HealthPoints;
            ManaPoints += other.ManaPoints;
        }
    }
}