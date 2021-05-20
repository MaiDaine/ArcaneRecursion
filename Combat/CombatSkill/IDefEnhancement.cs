using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct DefModifier
    {
        public StatusModifierVariable Magical;
        public StatusModifierVariable Earth;
        public StatusModifierVariable Fire;
        public StatusModifierVariable Water;
        public StatusModifierVariable Wind;
        public StatusModifierVariable Physical;

        public void Reset()
        {
            Physical.FlatValue = 0;
            Physical.PercentValue = 100;
            Magical.FlatValue = 0;
            Magical.PercentValue = 100;
            Earth.FlatValue = 0;
            Earth.PercentValue = 100;
            Fire.FlatValue = 0;
            Fire.PercentValue = 100;
            Water.FlatValue = 0;
            Water.PercentValue = 100;
            Wind.FlatValue = 0;
            Wind.PercentValue = 100;
        }

        public StatusModifierVariable GetModifier(DamageTypes type)
        {
            return type switch
            {
                DamageTypes.Magical => Magical,
                DamageTypes.Earth => Earth,
                DamageTypes.Fire => Fire,
                DamageTypes.Water => Water,
                DamageTypes.Wind => Wind,
                DamageTypes.Physical => Physical,
                _ => Magical
            }
        }
    }

    public interface IDefEnhancement
    {
        void ApplyEnhancement(ref DefModifier defEnhancement);
    }
}
