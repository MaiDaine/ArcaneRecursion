using UnityEngine;

namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Combat/Skill")]
    public class CombatSkillObject : ScriptableObject
    {
        public SelectionCursor Cursor;
        public bool IsPassive;
        public int CastRange;
        public int CastTime;
        public int Cooldown;
        public int APCost;
        public int MPCost;
        public NodeDefinition NodeDefinition;
        public CombatSkillReference Skill;
    }
}
