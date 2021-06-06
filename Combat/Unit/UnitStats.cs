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
        public int[] Defences;

        public UnitStats()
        {
            CombatSpeed = 0;
            MovementSpeed = 0;
            ActionPoints = 0;
            HealthPoints = 0;
            ManaPoints = 0;
            Defences = new int[6] { 0, 0, 0, 0, 0, 0 };
        }

        public UnitStats(UnitStats other)
        {
            CombatSpeed = other.CombatSpeed;
            MovementSpeed = other.MovementSpeed;
            ActionPoints = other.ActionPoints;
            HealthPoints = other.HealthPoints;
            ManaPoints = other.ManaPoints;
            Defences = new int[6];
            Array.Copy(other.Defences, Defences, 6);
        }
    }
}