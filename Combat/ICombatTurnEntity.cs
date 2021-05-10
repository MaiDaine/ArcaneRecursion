using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ArcaneRecursion
{
    public interface ICombatTurnEntity
    {
        int CombatSpeed { get; }
        int Team { get; }
        int Id { get; }
        Sprite Icone { get; }
    }

    public struct CombatTurnEntityStore
    {
        public int Team { get; }
        public int Id { get; }
        public Sprite Icone { get; }
        public FormationPosition FormationPosition;

        public CombatTurnEntityStore(int team, int id, Sprite icon, FormationPosition position = FormationPosition.Front)
        {
            Team = team;
            Id = id;
            Icone = icon;
            FormationPosition = position;
        }
    }
}