using System;
using UnityEngine;

namespace ArcaneRecursion
{
    [CreateAssetMenu(menuName = "ArcaneRecursion/Classes/ClasseDefinition")]
    public class ClassDefinition : ScriptableObject
    {
        public string Name;
        public string Description;
        public ClassSpell Spells;
        public ClassTalent Talents;
    }

    [Serializable]
    public struct ClassSpell
    {
        public NodeDefinition Passive;
        public NodeDefinition Spell1;
        public NodeDefinition Spell2;
        public NodeDefinition Spell3;
        public NodeDefinition Spell4;
        public NodeDefinition Ultimate;
    }

    [Serializable]
    public struct ClassTalent
    {
        public NodeDefinition[] Tier1;
        public NodeDefinition[] Tier2;
        public NodeDefinition[] Tier3;
        public NodeDefinition[] Tier4;
    }
}
