using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace ArcaneRecursion
{
    public class CombatAIController : CombatController
    {
        private List<SimulatedStep> _actionSequence;

        public override void UnitTurn(CombatEntity unit)
        {
            base.UnitTurn(unit);
            PlannerWorldState worldState = new PlannerWorldState();
            RefreshWorldState(ref worldState, unit);
            List<Tile[]> bestPositions = worldState.CurrentUnit.Brain.EvaluateBestPosition(worldState, _grid);
            List<SkillData> availableSkills;
            StepSequence bestSequence = new StepSequence { Score = 0, Steps = new List<SimulatedStep>() { new SimulatedStep() { Skill = null, Targets = bestPositions[0] } } };

            SkillSearch criterias = new SkillSearch()
            {
                MaxAPCost = 0,
                MaxMPCost = worldState.CurrentUnit.AvailableMP,
                q
                MinEnemyRange = 0,
                MinAllyRange = 0,
            };
            if (worldState.CurrentGoal == TeamGoal.Poke)
            {
                criterias.RequieredTags = new SkillTag[] { SkillTag.Damage, SkillTag.Debuff, SkillTag.Heal, SkillTag.Buff };
                criterias.ProhibitedTags = new SkillTag[] { SkillTag.Control };
            }
            else if (worldState.CurrentGoal == TeamGoal.Burst)
            {
                criterias.RequieredTags = new SkillTag[] { SkillTag.Damage, SkillTag.Control, SkillTag.Debuff, SkillTag.Heal };
                criterias.ProhibitedTags = new SkillTag[] { };
            }
            else if (worldState.CurrentGoal == TeamGoal.Defend)
            {
                criterias.RequieredTags = new SkillTag[] { SkillTag.Control, SkillTag.Debuff, SkillTag.Heal, SkillTag.Def };
                criterias.ProhibitedTags = new SkillTag[] { SkillTag.Damage, SkillTag.AOE };
            }
            foreach (Tile[] path in bestPositions)
            {
                //TODO STUB UNIT
                StepSequence currentSequence = new StepSequence { Score = 0, Steps = new List<SimulatedStep>() };
                criterias.MaxAPCost = worldState.CurrentUnit.AvailableAP - ((path.Length - 1) * worldState.CurrentUnit.MoveCost);
                EvaluatePathOpportunities(worldState, path, ref criterias.MinAllyRange, ref criterias.MinEnemyRange);
                availableSkills = worldState.CurrentUnit.Brain.GetSkillsWithCriteria(criterias);
                currentSequence = FindBestSequence(worldState, path, availableSkills, currentSequence);
                if (currentSequence.Score > bestSequence.Score)
                    bestSequence = currentSequence;
            }
            _actionSequence = bestSequence.Steps;
            ExecuteSequence();
        }

        private void RefreshWorldState(ref PlannerWorldState worldState, CombatEntity unit)
        {
            UnitController currentUnit = null;
            worldState.Allies = new List<WSUnit>();
            worldState.Enemies = new List<WSUnit>();
            worldState.LowestAllyIndex = -1;
            int xAverageTeamPosition = 0;
            int zAverageTeamPosition = 0;
            int lowestAllyHealthPercent = 100;
            int lowestEnemyHealthPercent = 100;

            foreach (CombatEntity entity in CombatTurnController.Instance.Entities)
                if (entity.Team != 0)
                {
                    WSUnit wsUnit;
                    UnitController unitController = entity.GameObject.GetComponent<UnitController>();
                    int healthPercent = unitController.Ressources.UnitStats.HealthPoints / unitController.Ressources.UnitStatsMax.HealthPoints * 100;

                    if (entity.Team == unit.Team)
                    {
                        if (entity.Id == unit.Id)
                            currentUnit = unitController;
                        else
                        {
                            wsUnit = new WSUnit() { HealthPoint = unitController.CurrentStats.HealthPoints, Position = unitController.CurrentTile, Orientation = unitController.Movement.Orientation };
                            xAverageTeamPosition += wsUnit.Position.Coordinates.X;
                            zAverageTeamPosition += wsUnit.Position.Coordinates.Z;
                            if (worldState.LowestAllyIndex == -1 || worldState.Allies[worldState.LowestAllyIndex].HealthPoint > wsUnit.HealthPoint)
                            {
                                worldState.LowestAllyIndex = worldState.Allies.Count;
                                lowestAllyHealthPercent = healthPercent;
                            }
                            worldState.Allies.Add(wsUnit);
                        }
                    }
                    else
                    {
                        wsUnit = new WSUnit() { HealthPoint = unitController.CurrentStats.HealthPoints, Position = unitController.CurrentTile, Orientation = unitController.Movement.Orientation };
                        if (worldState.DamageTargetIndex == -1 || wsUnit.HealthPoint < worldState.Enemies[worldState.DamageTargetIndex].HealthPoint)
                        {
                            worldState.DamageTargetIndex = worldState.Enemies.Count;
                            lowestEnemyHealthPercent = healthPercent;
                        }
                        worldState.Enemies.Add(wsUnit);
                    }
                }

            worldState.TeamAveragePosition = new HexCoordinates(xAverageTeamPosition / worldState.Allies.Count, zAverageTeamPosition / worldState.Allies.Count);
            unit.GameObject.GetComponent<UnitBrain>()?.OnTurnStart(ref worldState, currentUnit, unit.Team);
            UpdateTeamPlanning(ref worldState, lowestAllyHealthPercent, lowestEnemyHealthPercent);
        }

        private void UpdateTeamPlanning(ref PlannerWorldState worldState, int lowestAllyHealthPercent, int lowestEnemyHealthPercent)
        {
            //TODO set targets;

            if (lowestAllyHealthPercent < 25)
                worldState.CurrentGoal = TeamGoal.Defend;
            else if (lowestEnemyHealthPercent < 75)
                worldState.CurrentGoal = TeamGoal.Burst;
            else
                worldState.CurrentGoal = TeamGoal.Poke;

            worldState.DamageTargetIndex = 0;
            worldState.CCTargetIndex = -1;
        }

        private void EvaluatePathOpportunities(PlannerWorldState worldState, Tile[] path, ref int minAllyDistance, ref int minEnemyDistance)
        {
            minAllyDistance = int.MaxValue;
            minEnemyDistance = int.MaxValue;
            int tmpDistance;

            for (int i = 0; i < path.Length; i++)
            {
                foreach (WSUnit unit in worldState.Allies)
                {
                    tmpDistance = path[i].Coordinates.DistanceTo(unit.Position.Coordinates);
                    if (minAllyDistance > tmpDistance)
                        minAllyDistance = tmpDistance;
                }
                foreach (WSUnit unit in worldState.Enemies)
                {
                    tmpDistance = path[i].Coordinates.DistanceTo(unit.Position.Coordinates);
                    if (minEnemyDistance > tmpDistance)
                        minEnemyDistance = tmpDistance;
                }
            }
        }

        private StepSequence FindBestSequence(PlannerWorldState worldState, Tile[] path, List<SkillData> availableSkills, StepSequence currentSequence)
        {
            int bestScore = -1;
            int currentScore = 0;
            int savedPosition = 0;
            int availableAP;
            SimulatedStep bestStep = new SimulatedStep();
            SimulatedStep currentStep;
            foreach (SkillData skill in availableSkills)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    availableAP = worldState.CurrentUnit.AvailableAP - (worldState.CurrentUnit.MoveCost * i);
                    if (availableAP >= skill.SkillDefinition.SkillStats.APCost)
                    {
                        currentStep = worldState.CurrentUnit.Brain.EvaluateAction(worldState, path[i], skill, ref currentScore);
                        if (currentScore > bestScore)
                        {
                            bestScore = currentScore;
                            bestStep = currentStep;
                            savedPosition = i;
                        }
                    }
                }
            }
            if (bestScore == -1)
                return currentSequence;

            if (savedPosition != 0)
            {
                Tile[] partialPath = new Tile[savedPosition + 1];
                Array.Copy(path, partialPath, savedPosition + 1);
                if (savedPosition + 1 == path.Length)
                    path = new Tile[1] { path[path.Length - 1] };
                else
                {
                    Tile[] updatedPath = new Tile[path.Length - savedPosition];
                    Array.Copy(path, savedPosition, updatedPath, 0, path.Length - savedPosition);//TODO USE STUB UNIT
                    path = updatedPath;
                }
                currentSequence.Steps.Add(new SimulatedStep() { Skill = null, Targets = partialPath });
            }

            currentSequence.Score += bestScore;
            currentSequence.Steps.Add(bestStep);
            UpdateLocalWorldState(ref worldState, bestStep);
            availableSkills.RemoveAll(x => x.SkillDefinition.SkillStats.APCost > worldState.CurrentUnit.AvailableAP);
            if (availableSkills.Count == 0)
                return currentSequence;
            return FindBestSequence(worldState, path, availableSkills, currentSequence);
        }

        private void UpdateLocalWorldState(ref PlannerWorldState worldState, SimulatedStep step)
        {
            worldState.CurrentUnit.AvailableAP -= step.Skill.SkillDefinition.SkillStats.APCost;
        }

        private void ExecuteSequence()
        {
            Debug.Log(string.Format("Action:{0} || UNIT AP: {1}", _actionSequence.Count, _currentUnit.CurrentStats.ActionPoints));
            if (_actionSequence.Count == 0)
            {
                base.UnitActionEnd();
                return;
            }
            SimulatedStep step = _actionSequence[0];
            _actionSequence.RemoveAt(0);
            if (step.Skill == null)
            {
                Debug.Log("Move To: " + step.Targets[step.Targets.Length - 1].Coordinates.ToString());
                _currentUnit.Move(ExecuteSequence, step.Targets.Skip(1).ToArray());
                return;
            }
            _currentUnit.CurrentStats.ActionPoints -= step.Skill.SkillDefinition.SkillStats.APCost;
            ExecuteSequence();
        }
    }
}