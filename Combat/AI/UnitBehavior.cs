using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ArcaneRecursion
{
    public enum FormationPosition { Front, MidFront, MidBack, Back }

    [Serializable]
    public class SkillRole
    {
        public int Tank;
        public int Heal;
        public int Support;
        public int DPS;

        public void Add(SkillRole other)
        {
            Tank += other.Tank;
            Heal += other.Heal;
            Support += other.Support;
            DPS += other.DPS;
        }

        public void Normalise(int partition)
        {
            Tank /= partition;
            Heal /= partition;
            Support /= partition;
            DPS /= partition;
        }

        public override string ToString()
        {
            return "Tank<" + Tank + "> || Heal<" + Heal + "> || Support<" + Support + "> || DPS<" + DPS + ">";
        }
    }

    public class UnitBehavior : ScriptableObject
    {
        public SkillRole combatGoal;
        public FormationPosition preferedPosition;
    }
}