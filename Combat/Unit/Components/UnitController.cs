using System;
using UnityEngine;

namespace ArcaneRecursion
{
    public class UnitController : MonoBehaviour
    {
        public CombatEntity CombatEntity { get; private set; }
        public UnitMovement Movement { get; private set; }
        public UnitRessources Ressources { get; private set; }
        public UnitSkills Skills { get; private set; }
        public UnitStatus Status { get; private set; }
        public UnitStats CurrentStats { get { return Ressources.UnitStats; } }
        public Tile CurrentTile { get { return Movement.CurrentTile; } }

        #region Init
        public void Init(CombatUnit unit, CombatEntity entity, Tile tile)
        {
            CombatEntity = entity;
            Movement.Init(tile, entity);
            Ressources.LoadStats(entity.UnitStats);
            Skills = new UnitSkills(unit.Build, this);
        }
        #endregion /* Init */

        #region UnitTurn Cycle
        public void StartTurn()
        {
            CurrentStats.ActionPoint = CombatEntity.UnitStats.ActionPoint;
            Skills.OnStartTurn();
            Status.OnStartTurn();
        }

        public void EndTurn()
        {
            Skills.ClearSelectedSkill();
            Status.OnEndTurn();
        }
        #endregion /* UnitTurn Cycle */

        public bool CanMoveTo(Tile[] path) { return CurrentStats.ActionPoint >= CalculateMoveCost(path.Length); }

        public bool CanMoveTo(Tile[] path, ref int moveCost)
        {
            moveCost = CalculateMoveCost(path.Length);
            return Ressources.UnitStats.ActionPoint >= moveCost;
        }

        public string Move(Action callback, Tile[] path)
        {
            int moveCost = CalculateMoveCost(path.Length);

            if (CanMoveTo(path, ref moveCost))
            {
                CurrentStats.ActionPoint -= moveCost;
                Movement.MoveTo(callback, path);
                return null;
            }
            return "Not enought AP";
        }

        public void OnDeath()
        {
            Debug.Log("TODO UNIT DEATH"); //TODO 
            CombatTurnController.Instance.RemoveUnit(CombatEntity);
        }

        #region MonoBehavior LifeCycle
        private void Awake()
        {
            Movement = GetComponent<UnitMovement>();
            Ressources = new UnitRessources(this);
            Status = new UnitStatus(this);
        }
        #endregion /* MonoBehavior LifeCycle */

        private int CalculateMoveCost(int pathLength)
        {
            return pathLength * CurrentStats.MovementSpeed;//TODO MOVESPEED
        }
    }
}