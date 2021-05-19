using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct SkillModifier
    {
        public StatusModifierVariable AP;
        public StatusModifierVariable MP;

        public void Reset()
        {
            AP.FlatValue = 0;
            AP.PercentValue = 100;
            MP.FlatValue = 0;
            MP.PercentValue = 100;
        }
    }

    public interface ISkillEnhancement
    {
        void ApplyEnhancement(ref SkillModifier castEnhancement);
    }
}
