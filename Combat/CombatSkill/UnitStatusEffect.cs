using UnityEngine;

namespace ArcaneRecursion
{
    public struct UnitStatusEffect
    {
        public bool IsCripple;
        public bool IsRoot;

        public UnitStatusEffect(UnitStatusEffect other)
        {
            IsCripple = other.IsCripple;
            IsRoot = other.IsRoot;
        }

        public void Reset()
        {
            IsCripple = false;
            IsRoot = false;
        }
    }
}
