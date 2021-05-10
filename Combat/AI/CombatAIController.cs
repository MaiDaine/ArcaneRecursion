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
            UpdateTeamPlanning(ref worldState);
            List<Tile[]> bestPositions = worldState.CurrentUnit.Brain.EvaluateBestPosition(worldState, _grid);
            List<CombatSkillObject> availableSkills;
            StepSequence bestSequence = new StepSequence { Score = 0, Steps = new List<SimulatedStep>() { new SimulatedStep() { Skill = null, Targets = bestPositions[0] } } };

            if (worldState.CurrentGoal == TeamGoal.Poke)
            {
                SkillSearch criterias = new SkillSearch()
                {
                    MaxAPCost = 0,
                    MaxMPCost = worldState.CurrentUnit.AvailableMP,
                    MinEnemyRange = 0,
                    MinAllyRange = 0,
                    RequieredTags = new SkillTag[] { SkillTag.DPS, SkillTag.Debuff, SkillTag.Heal, SkillTag.Buff },
                    ProhibitedTags = new SkillTag[] { SkillTag.CC }
                };
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
            }
            //else if (worldState.currentGoal == TeamGoal.Burst) { }
            //else if (worldState.currentGoal == TeamGoal.Defend) { }
            _actionSequence = bestSequence.Steps;
            ExecuteSequence();
        }

        private void RefreshWorldState(ref PlannerWorldState worldState, CombatEntity unit)
        {
            worldState.CurrentUnit.Index = 0;
            worldState.Allies = new List<WSUnit>();
            worldState.Enemies = new List<WSUnit>();
            foreach (CombatEntity entity in CombatTurnController.Instance.Entities)
            {
                if (entity.Team != 0)
                {
                    UnitController unitController = entity.GameObject.GetComponent<UnitController>();
                    if (entity.Team == unit.Team)
                    {
                        if (entity.Id == unit.Id)
                            unit.GameObject.GetComponent<UnitBrain>()?.OnTurnStart(ref worldState, unitController, unit.Team);
                        worldState.Allies.Add(new WSUnit() { HealthPoint = unitController.CurrentStats.HealthPoint, Position = unitController.CurrentTile, Orientation = unitController.Movement.Orientation });
                    }
                    else
                    {
                        worldState.Enemies.Add(new WSUnit() { HealthPoint = unitController.CurrentStats.HealthPoint, Position = unitController.CurrentTile, Orientation = unitController.Movement.Orientation });
                    }
                }
            }
        }

        private void UpdateTeamPlanning(ref PlannerWorldState worldState) { worldState.CurrentGoal = TeamGoal.Poke; worldState.DamageTargetIndex = 0; worldState.CCTargetIndex = -1; }

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

        private StepSequence FindBestSequence(PlannerWorldState worldState, Tile[] path, List<CombatSkillObject> availableSkills, StepSequence currentSequence)
        {
            int bestScore = -1;
            int currentScore = 0;
            int savedPosition = 0;
            int availableAP;
            SimulatedStep bestStep = new SimulatedStep();
            SimulatedStep currentStep;
            foreach (CombatSkillObject skill in availableSkills)
            {
                for (int i = 0; i < path.Length; i++)
                {
                    availableAP = worldState.CurrentUnit.AvailableAP - (worldState.CurrentUnit.MoveCost * i);
                    if (availableAP >= skill.APCost)
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
                    Array.Copy(path, savedPosition, updatedPath, 0, savedPosition);//TODO USE STUB UNIT
                    path = updatedPath;
                }
                currentSequence.Steps.Add(new SimulatedStep() { Skill = null, Targets = partialPath });
            }

            currentSequence.Score += bestScore;
            currentSequence.Steps.Add(bestStep);
            UpdateLocalWorldState(ref worldState, bestStep);
            availableSkills.RemoveAll(x => x.APCost > worldState.CurrentUnit.AvailableAP);
            if (availableSkills.Count == 0)
                return currentSequence;
            return FindBestSequence(worldState, path, availableSkills, currentSequence);
        }

        private void UpdateLocalWorldState(ref PlannerWorldState worldState, SimulatedStep step)
        {
            worldState.CurrentUnit.AvailableAP -= step.Skill.APCost;
        }

        private void ExecuteSequence()
        {
            Debug.Log(string.Format("Action:{0} || UNIT AP: {1}", _actionSequence.Count, _currentUnit.CurrentStats.ActionPoint));
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
            Debug.Log(string.Format("STEP: {0} => {1}", step.Skill.Skill, step.Targets[0]));
            _currentUnit.CurrentStats.ActionPoint -= step.Skill.APCost;
            ExecuteSequence();
        }
    }
}