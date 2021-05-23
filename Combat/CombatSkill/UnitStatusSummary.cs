using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcaneRecursion
{
    public struct UnitStatusSummary
    {
        public bool IsRoot;

        public UnitStatusSummary(UnitStatusSummary other)
        {
            IsRoot = other.IsRoot;
        }
    }
}
