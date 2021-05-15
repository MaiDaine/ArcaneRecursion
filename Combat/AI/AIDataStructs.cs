using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct PlannerWorldState
    {
        public TeamGoal CurrentGoal;
        public WSCurrentUnit CurrentUnit;
        public int DamageTargetIndex;
        public int CCTargetIndex;
        public List<WSUnit> Allies;
        public List<WSUnit> Enemies;
    }

    public struct WSCurrentUnit
    {
        public int Index;
        public int TeamId;
        public int AvailableAP;
        public int AvailableMP;
        public int MoveCost;
        public UnitBrain Brain;
        public UnitController Controller;//should stub unit to account for buff
    }

    public struct WSUnit
    {
        public int HealthPoint;
        public Tile Position;
        public HexCoordinates Orientation;
    }

    public struct SkillSearch
    {
        public int MaxAPCost;
        public int MaxMPCost;
        public int MinAllyRange;
        public int MinEnemyRange;
        public SkillTag[] RequieredTags;
        public SkillTag[] ProhibitedTags;
    }

    public struct SimulatedStep
    {
        public SkillData Skill;
        public Tile[] Targets;
    }

    public struct StepSequence
    {
        public int Score;
        public List<SimulatedStep> Steps;
    }
}