using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct SkillModifier
    {
        public int APFlat;
        public float APPercent;
        public int MPFlat;
        public float MPPercent;
    }

    public interface ISkillEnhancement
    {
        void ApplyEnhancement(ref SkillModifier castEnhancement);
    }
}
