using UnityEngine;

namespace ArcaneRecursion
{
    public interface IUnitStatus
    {
        void ApplyStatus(ref UnitStatusEffect statusEffect);
    }
}