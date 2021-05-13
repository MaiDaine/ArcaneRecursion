using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Classes/SkillDefinition")]
    public class SkillDefinition : NodeDefinition
    {
        public SelectionCursor Cursor;
        public SkillStats SkillStats;
        public List<SkillTag> SkillTags;
    }

    public struct SkillStats
    {
        public int CastRange;
        public int CastTime;
        public int Cooldown;
        public int APCost;
        public int MPCost;
        public int Potency;
    }
}