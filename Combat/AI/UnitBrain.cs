using System.Collections.Generic;
using UnityEngine;
using System;

namespace ArcaneRecursion
{
    public class UnitBrain : MonoBehaviour
    {
        public void OnTurnStart(ref PlannerWorldState worldState, UnitController controller, int teamId)
        {
            worldState.CurrentUnit.TeamId = teamId;
            worldState.CurrentUnit.AvailableAP = controller.CurrentStats.ActionPoints;
            worldState.CurrentUnit.AvailableMP = controller.CurrentStats.ManaPoints;
            worldState.CurrentUnit.MoveCost = controller.CurrentStats.MovementSpeed;
            worldState.CurrentUnit.Brain = this;
            worldState.CurrentUnit.Controller = controller;//TODO Stub
            //((UnitControllerStub)worldState.CurrentUnit.Controller).Init(controller);
        }

        public List<Tile[]> EvaluateBestPosition(PlannerWorldState worldState, CombatGrid grid)
        {
            if (worldState.CurrentUnit.Controller.Status.StatusSummary.IsRoot)
                return new List<Tile[]> { new Tile[1] { worldState.CurrentUnit.Controller.CurrentTile } };
            return worldState.CurrentGoal switch
            {
                TeamGoal.Poke => FindPokePosition(worldState, grid),
                TeamGoal.Burst => FindBurstPosition(worldState, grid),
                TeamGoal.Defend => FindDefendPosition(worldState, grid),
                _ => null,
            };
        }

        public List<SkillData> GetSkillsWithCriteria(SkillSearch criterias)
        {
            UnitController controller = GetComponent<UnitController>();
            List<SkillData> results = new List<SkillData>();

            foreach (List<UnitSkill> skills in controller.Skills.AvailableSkills)
            {
                if (skills != null)
                {
                    foreach (UnitSkill skill in skills)
                    {
                        if (skill.SkillStats.Cooldown == 0 && ValidateCriteria(skill.SkillData, criterias))
                            results.Add(skill.SkillData);
                    }
                }
            }

            return results;
        }

        public SimulatedStep EvaluateAction(PlannerWorldState worldState, Tile fromPosition, SkillData skill, ref int score)
        {
            //TODO
            int distance;
            score = -1;
            foreach (WSUnit unit in worldState.Enemies)
            {
                distance = fromPosition.Coordinates.DistanceTo(worldState.Enemies[worldState.DamageTargetIndex].Position.Coordinates);
                if (distance >= skill.SkillDefinition.SkillStats.MinCastRange && distance <= skill.SkillDefinition.SkillStats.CastRange)
                {
                    score = 100;
                    return new SimulatedStep() { Skill = skill, Targets = new Tile[1] { unit.Position } };
                }
            }
            return new SimulatedStep { Skill = null, Targets = null };
        }

        protected virtual List<Tile[]> FindPokePosition(PlannerWorldState worldState, CombatGrid grid)
        {
            HexCoordinates from = worldState.CurrentUnit.Controller.CurrentTile.Coordinates;
            HexCoordinates to = worldState.Enemies[worldState.DamageTargetIndex].Position.Coordinates;
            Vector2 toTeam;
            Vector2 toTarget;

            toTeam.x = worldState.AlliesAveragePosition.X - from.X;
            toTeam.y = worldState.AlliesAveragePosition.Z - from.Z;

            toTarget.x = to.X - from.X;
            toTarget.y = to.Z - from.Z;

            int distanceToTarget = from.DistanceTo(to);
            if (toTeam.normalized.x == toTarget.normalized.x && toTeam.normalized.y == toTarget.normalized.y
                && distanceToTarget < from.DistanceTo(worldState.AlliesAveragePosition))
                Debug.Log("TODO REPOSITION UNIT");

            //TODO Generate path to team and target with FindPositionCloseTo
            return StubSearch(worldState, grid);
        }

        protected virtual List<Tile[]> FindBurstPosition(PlannerWorldState worldState, CombatGrid grid)
        {
            return StubSearch(worldState, grid);
        }

        protected virtual List<Tile[]> FindDefendPosition(PlannerWorldState worldState, CombatGrid grid)
        {
            return StubSearch(worldState, grid);
        }

        protected bool ValidateCriteria(SkillData skill, SkillSearch criterias)
        {
            //TODO
            return skill.SkillDefinition.SkillStats.CastRange >= criterias.MinEnemyRange;
        }

        private List<Tile[]> FindPositionCloseTo(HexCoordinates target)
        {
            return new List<Tile[]>();
        }

        private List<Tile[]> StubSearch(PlannerWorldState worldState, CombatGrid grid)
        {
            if (worldState.CurrentUnit.Controller.CurrentTile.Coordinates.DistanceTo(worldState.Enemies[0].Position.Coordinates) == 1)
                return new List<Tile[]>() { new Tile[1] { worldState.CurrentUnit.Controller.CurrentTile } };

            Tile[] path = grid.FindPath(worldState.CurrentUnit.Controller.CurrentTile, worldState.Enemies[0].Position, true);
            if (worldState.CurrentUnit.AvailableAP >= (path.Length * worldState.CurrentUnit.MoveCost))
            {
                Tile[] fullPath = new Tile[path.Length + 1];

                fullPath[0] = worldState.CurrentUnit.Controller.CurrentTile;
                Array.Copy(path, 0, fullPath, 1, path.Length);
                return new List<Tile[]>() { fullPath };
            }
            else
            {
                int max = worldState.CurrentUnit.AvailableAP / worldState.CurrentUnit.MoveCost;
                Tile[] shorterPath = new Tile[max];

                Array.Copy(path, shorterPath, max);
                return new List<Tile[]>() { shorterPath };
            }
        }
    }
}