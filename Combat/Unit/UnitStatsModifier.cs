using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct UnitStatsModifier
    {
        public StatusModifierVariable CombatSpeed;
        public StatusModifierVariable MovementSpeed;
        public StatusModifierVariable ActionPoints;
        public StatusModifierVariable HealthPoints;
        public StatusModifierVariable ManaPoints;
        public StatusModifierVariable[] Defences;

        public void Reset()
        {
            CombatSpeed.Reset();
            MovementSpeed.Reset();
            ActionPoints.Reset();
            HealthPoints.Reset();
            ManaPoints.Reset();
            Defences = new StatusModifierVariable[6]
            {
                new StatusModifierVariable { FlatValue = 0, PercentValue = 100 },
                new StatusModifierVariable { FlatValue = 0, PercentValue = 100 },
                new StatusModifierVariable { FlatValue = 0, PercentValue = 100 },
                new StatusModifierVariable { FlatValue = 0, PercentValue = 100 },
                new StatusModifierVariable { FlatValue = 0, PercentValue = 100 },
                new StatusModifierVariable { FlatValue = 0, PercentValue = 100 }
            };
        }
    }
}