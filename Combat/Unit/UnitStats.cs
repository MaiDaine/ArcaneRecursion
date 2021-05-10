using System;

namespace ArcaneRecursion
{
    [Serializable]
    public class UnitStats
    {
        public int CombatSpeed;
        public int MovementSpeed;
        public int ActionPoint;
        public int HealthPoint;
        public int ManaPoint;

        #region Init
        public UnitStats()
        {
            CombatSpeed = 0;
            MovementSpeed = 0;
            ActionPoint = 0;
            HealthPoint = 0;
            ManaPoint = 0;
        }

        public UnitStats(UnitStats other)
        {
            CombatSpeed = other.CombatSpeed;
            MovementSpeed = other.MovementSpeed;
            ActionPoint = other.ActionPoint;
            HealthPoint = other.HealthPoint;
            ManaPoint = other.ManaPoint;
        }

        public static UnitStats DefaultPercentModifier()
        {
            return new UnitStats()
            {
                CombatSpeed = 100,
                MovementSpeed = 100,
                ActionPoint = 100,
                HealthPoint = 100,
                ManaPoint = 100,
            };
        }
        #endregion /* Init */

        public void ApplyModifier(UnitStats other)
        {
            CombatSpeed += other.CombatSpeed;
            MovementSpeed += other.MovementSpeed;
            ActionPoint += other.ActionPoint;
            HealthPoint += other.HealthPoint;
            ManaPoint += other.ManaPoint;
        }
    }
}