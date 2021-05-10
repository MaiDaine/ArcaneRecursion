using System.Collections.Generic;
using UnityEngine;
using System;

namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Combat/CombatEntityLoader")]
    public class CombatLoaderData : ScriptableObject
    {
        public List<CombatUnit> PlayerUnits;
        public List<AICombatUnit> EnemyUnits;
        public List<ICombatTurnEntity> MapEntities;
    }

    [Serializable]
    public class AICombatUnit : CombatUnit
    {
        public FormationPosition FormationPosition;
    }
}