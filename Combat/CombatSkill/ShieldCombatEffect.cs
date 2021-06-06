using UnityEngine;

namespace ArcaneRecursion
{
    public class ShieldCombatEffect : CombatEffect
    {
        public bool OnDamageTaken(ref int amount, DamageTypes damageType = DamageTypes.Magical)
        {
            Potency -= amount;

            if (Potency > 0)
            {
                amount = 0;
                return false;
            }

            amount = -Potency;
            return true;
        }
    }
}