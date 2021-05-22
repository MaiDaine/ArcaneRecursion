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
        public DefModifier Defences { get { return Status.DefModifier; } }
        public Tile CurrentTile { get { return Movement.CurrentTile; } }

        #region Init
        public void Init(CombatUnit unit, CombatEntity entity, Tile tile)
        {
            CombatEntity = entity;
            Movement.Init(tile, entity);
            Ressources.LoadStats(entity.UnitStats);
            Skills = new UnitSkills(SkillLibrary.GenerateInnateSkills(unit.InnateSkills), unit.Build, this);
        }
        #endregion /* Init */

        #region UnitTurn Cycle
        public void StartTurn()
        {
            CurrentStats.ActionPoints = CombatEntity.UnitStats.ActionPoints;
            Skills.OnStartTurn();
            Status.OnStartTurn();
        }

        public void EndTurn()
        {
            Skills.ClearSelectedSkill();
            Status.OnEndTurn();
        }
        #endregion /* UnitTurn Cycle */

        public bool CanMoveTo(Tile[] path) { return CurrentStats.ActionPoints >= CalculateMoveCost(path.Length); }

        public bool CanMoveTo(Tile[] path, ref int moveCost)
        {
            moveCost = CalculateMoveCost(path.Length);
            return Ressources.UnitStats.ActionPoints >= moveCost;
        }

        public string Move(Action callback, Tile[] path)
        {
            int moveCost = CalculateMoveCost(path.Length);

            if (CanMoveTo(path, ref moveCost))
            {
                CurrentStats.ActionPoints -= moveCost;
                Movement.MoveTo(callback, path);
                return null;
            }
            return "Not enought AP";
        }

        public void OnKnockBack(HexCoordinates direction)
        {
            CombatGrid grid = CombatGrid.Instance;
            Tile toTile = null;
            int toX = CurrentTile.Coordinates.X + direction.X;
            int toZ = CurrentTile.Coordinates.Z + direction.Z;

            if (toX >= 0 && toX <= grid.Width && toZ >= 0 && toZ <= grid.Height)
            {
                toTile = grid.GetTilefromCoordinate(toX, toZ);
                if (toTile.State == TileState.Empty)
                {
                    //TODO Travel trigger + animation
                    Movement.Teleport(toTile);
                    return;
                }
            }
            Ressources.OnKnockbackDamage();
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