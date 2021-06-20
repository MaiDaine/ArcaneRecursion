using System;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitControllerStub : UnitController
    {
        public void Init(UnitController other)
        {
            CombatEntity = other.CombatEntity;
            Movement = null;
            Ressources.LoadStats(other.CombatEntity.UnitStats);
            Skills = new UnitSkillsStub(other.Skills);
        }

        public override bool Move(Action callback, Tile[] path)
        {
            int moveCost = CalculateMoveCost(path);

            if (CanMoveTo(path, ref moveCost))
            {
                CurrentStats.ActionPoints -= moveCost;
                return true;
            }
            return false;
        }

        public override void OnDeath() { }
    }
}