using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct DefModifier
    {
        public StatusModifierVariable Def;
        public StatusModifierVariable Res;
        public StatusModifierVariable Earth;
        public StatusModifierVariable Fire;
        public StatusModifierVariable Water;
        public StatusModifierVariable Wind;

        public void Reset()
        {
            Def.FlatValue = 0;
            Def.PercentValue = 100;
            Res.FlatValue = 0;
            Res.PercentValue = 100;
            Earth.FlatValue = 0;
            Earth.PercentValue = 100;
            Fire.FlatValue = 0;
            Fire.PercentValue = 100;
            Water.FlatValue = 0;
            Water.PercentValue = 100;
            Wind.FlatValue = 0;
            Wind.PercentValue = 100;
        }
    }

    public interface IDefEnhancement
    {
        void ApplyEnhancement(ref DefModifier defEnhancement);
    }
}
