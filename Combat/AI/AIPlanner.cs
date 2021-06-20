using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ArcaneRecursion
{
    public class AIPlanner
    {
        public void RefreshWorldState(ref PlannerWorldState worldState, CombatEntity unit)
        {
            UnitController currentUnit = null;
            int xAverageTeamPosition = 0;
            int zAverageTeamPosition = 0;
            int lowestAllyHealthPercent = 100;
            int lowestEnemyHealthPercent = 100;

            ResetWorldState(ref worldState);
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

            worldState.AlliesAveragePosition = new HexCoordinates(xAverageTeamPosition / worldState.Allies.Count, zAverageTeamPosition / worldState.Allies.Count);
            worldState.EnemiesAveragePosition = new HexCoordinates(xAverageTeamPosition / worldState.Enemies.Count, zAverageTeamPosition / worldState.Enemies.Count);
            unit.GameObject.GetComponent<UnitBrain>()?.OnTurnStart(ref worldState, currentUnit, unit.Team);
            UpdateTeamPlanning(ref worldState, lowestAllyHealthPercent, lowestEnemyHealthPercent);
        }

        private void ResetWorldState(ref PlannerWorldState worldState)
        {
            worldState.Allies = new List<WSUnit>();
            worldState.Enemies = new List<WSUnit>();
            worldState.LowestAllyIndex = -1;
            worldState.DamageTargetIndex = -1;
            worldState.CCTargetIndex = -1;
        }

        private void UpdateTeamPlanning(ref PlannerWorldState worldState, int lowestAllyHealthPercent, int lowestEnemyHealthPercent)
        {
            // Should update with turn order
            if (lowestAllyHealthPercent < 25)
            {
                worldState.CurrentGoal = TeamGoal.Defend;
                worldState.CCTargetIndex = FindIndexOfClosestUnitFrom(worldState.Allies[worldState.LowestAllyIndex].Position.Coordinates, worldState.Enemies);
            }
            else if (lowestEnemyHealthPercent < 75)
            {
                worldState.CurrentGoal = TeamGoal.Burst;
                worldState.CCTargetIndex = FindIndexOfClosestUnitFrom(worldState.Enemies[worldState.DamageTargetIndex].Position.Coordinates, worldState.Enemies);
            }
            else
                worldState.CurrentGoal = TeamGoal.Poke;

            Debug.Log("GOAL STATE <" + worldState.CurrentGoal + ">");
            Debug.Log("DMG_TARGET <" + worldState.Enemies[worldState.DamageTargetIndex].Position.Coordinates + ">");
            if (worldState.CCTargetIndex != -1)
                Debug.Log("CC_TARGET <" + worldState.Enemies[worldState.CCTargetIndex].Position.Coordinates + ">");
            Debug.Log("HEAL_TARGET <" + worldState.Allies[worldState.LowestAllyIndex].Position.Coordinates + ">");
        }

        private int FindIndexOfClosestUnitFrom(HexCoordinates position, List<WSUnit> targetTeam)
        {
            int index = 0;
            int distance = position.DistanceTo(targetTeam[0].Position.Coordinates);
            int tmpDistance;

            for (int i = 0; i < targetTeam.Count; i++)
            {
                tmpDistance = position.DistanceTo(targetTeam[1].Position.Coordinates);
                if (tmpDistance < distance)
                {
                    distance = tmpDistance;
                    index = i;
                }
            }

            return index;
        }

        public StepSequence FindBestSequence(PlannerWorldState worldState, Tile[] path, List<SkillData> availableSkills, StepSequence currentSequence)
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
    }
}