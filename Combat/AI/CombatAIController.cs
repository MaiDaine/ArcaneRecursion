using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace ArcaneRecursion
{
    public class CombatAIController : CombatController
    {
        private List<SimulatedStep> _actionSequence;
        private readonly AIPlanner _planner = new AIPlanner();

        private readonly SkillTag[] PokeRequiredTags = { SkillTag.Damage, SkillTag.Debuff, SkillTag.Heal, SkillTag.Buff };
        private readonly SkillTag[] PokeProhibitedTags = { SkillTag.Control };
        private readonly SkillTag[] BurstRequiredTags = { SkillTag.Damage, SkillTag.Control, SkillTag.Debuff, SkillTag.Heal };
        private readonly SkillTag[] BurstProhibitedTags = { };
        private readonly SkillTag[] DefendRequiredTags = { SkillTag.Control, SkillTag.Debuff, SkillTag.Heal, SkillTag.Def };
        private readonly SkillTag[] DefendProhibitedTags = { };

        public override void UnitTurn(CombatEntity unit)
        {
            base.UnitTurn(unit);
            PlannerWorldState worldState = new PlannerWorldState();
            _planner.RefreshWorldState(ref worldState, unit);
            List<Tile[]> bestPositions = worldState.CurrentUnit.Brain.EvaluateBestPosition(worldState, _grid);
            List<SkillData> availableSkills;
            StepSequence bestSequence = new StepSequence { Score = 0, Steps = new List<SimulatedStep>() { new SimulatedStep() { Skill = null, Targets = bestPositions[0] } } };

            SkillSearch criterias = GenerateCriteria(worldState);
            foreach (Tile[] path in bestPositions)
            {
                //TODO STUB UNIT
                StepSequence currentSequence = new StepSequence { Score = 0, Steps = new List<SimulatedStep>() };
                criterias.MaxAPCost = worldState.CurrentUnit.AvailableAP - ((path.Length - 1) * worldState.CurrentUnit.MoveCost);
                EvaluatePathOpportunities(worldState, path, ref criterias.MinAllyRange, ref criterias.MinEnemyRange);
                availableSkills = worldState.CurrentUnit.Brain.GetSkillsWithCriteria(criterias);
                currentSequence = _planner.FindBestSequence(worldState, path, availableSkills, currentSequence);
                if (currentSequence.Score > bestSequence.Score)
                    bestSequence = currentSequence;
            }
            _actionSequence = bestSequence.Steps;
            ExecuteSequence();
        }

        private SkillSearch GenerateCriteria(PlannerWorldState worldState)
        {
            SkillSearch criterias = new SkillSearch()
            {
                MaxAPCost = 0,
                MaxMPCost = worldState.CurrentUnit.AvailableMP,
                MinEnemyRange = 0,
                MinAllyRange = 0,
            };
            if (worldState.CurrentGoal == TeamGoal.Poke)
            {
                criterias.RequieredTags = PokeRequiredTags;
                criterias.ProhibitedTags = PokeProhibitedTags;
            }
            else if (worldState.CurrentGoal == TeamGoal.Burst)
            {
                criterias.RequieredTags = BurstRequiredTags;
                criterias.ProhibitedTags = BurstProhibitedTags;
            }
            else if (worldState.CurrentGoal == TeamGoal.Defend)
            {
                criterias.RequieredTags = DefendRequiredTags;
                criterias.ProhibitedTags = DefendProhibitedTags;
            }

            return criterias;
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