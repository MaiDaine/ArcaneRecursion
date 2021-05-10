using System.Collections.Generic;
using UnityEngine;
using System;

namespace ArcaneRecursion
{
    [Serializable]
    public class CombatEntity : ICombatTurnEntity
    {
        public GameObject GameObject { get; }
        public int Team { get; }
        public int Id { get; }
        public Sprite Icone { get; }
        public int CombatSpeed { get { return UnitStats.CombatSpeed; } }
        public UnitStats UnitStats { get; }
        public List<ClassBuild> Build { get; }

        public CombatEntity(GameObject gameObject, CombatUnit combatUnit, CombatTurnEntityStore store)
        {
            GameObject = gameObject;
            Team = store.Team;
            Id = store.Id;
            Icone = store.Icone;
            UnitStats = combatUnit.Stats;//TODO
            Build = combatUnit.Build;
        }
    }

    public class AICombatEntity : CombatEntity
    {
        public FormationPosition FormationPosition;

        public AICombatEntity(GameObject gameObject, CombatUnit combatUnit, CombatTurnEntityStore store) : base(gameObject, combatUnit, store)
        {
            FormationPosition = store.FormationPosition;
        }
    }
}
